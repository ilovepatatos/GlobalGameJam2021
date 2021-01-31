using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    [HideInInspector] public bool IsPlayerMoving;
    [HideInInspector] public bool EnableMultiDirectionMovement;
    private bool IsMovementEnable = true;

    private Player player;
    [HideInInspector] public Rigidbody2D Rb;
    private float onInteractionInitialRotation;

    public event Action OnPlayerStartMoving, OnPlayerStopMoving;

    private static Dictionary<Vector2, float> zAxisRotationPresets = new Dictionary<Vector2, float>()
    {
        {Vector2.up, 0},
        {new Vector2(-1, 1), 45},
        {Vector2.left, 90},
        {new Vector2(-1, -1), 135},
        {Vector2.down, 180},
        {new Vector2(1, -1), 225},
        {Vector2.right, 270},
        {new Vector2(1, 1), 315},
    };

    private static Dictionary<Vector2, float> zAxisRotationPresetsInvert = new Dictionary<Vector2, float>()
    {
        {Vector2.up, 180},
        {new Vector2(-1, 1), 225},
        {Vector2.left, 270},
        {new Vector2(-1, -1), 315},
        {Vector2.down, 0},
        {new Vector2(1, -1), 45},
        {Vector2.right, 90},
        {new Vector2(1, 1), 135},
    };

    public void OnPickupObject() {
        onInteractionInitialRotation = To180Angle(Rb.rotation);
    }

    public void SetEnableMovement(bool enable) {
        IsMovementEnable = false;
    }

    private void Awake() {
        player = GetComponent<Player>();
        Rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (!IsMovementEnable)
            return;
        Vector2 dir = player.Input.MovementDirection;

        if (player.IsCarryingObject)
            if (!CanMoveTowardDirection(dir))
                dir = Vector2.zero;

        UpdateRotation(dir);
        UpdateMovement(dir, Time.fixedDeltaTime);
        UpdateAnimator(dir);
    }

    private void UpdateAnimator(Vector2 dir) {
        float speed = Math.Max(Math.Abs(dir.x), Math.Abs(dir.y));
        player.PlayerAnimator.SetFloat("Speed", speed);
        player.RightClawAnimator.SetFloat("Speed", speed);
        player.LeftClawAnimator.SetFloat("Speed", speed);
    }

    private void UpdateMovement(Vector2 dir, float delta) {
        ResolveMovement(dir);
        float weight = player.IsCarryingObject ? player.ObjectCarrying.Weight : 1;
        Rb.MovePosition((Vector2) transform.position + dir.normalized * (delta * (Speed / weight)));
    }

    private void ResolveMovement(Vector2 dir) {
        if (ShouldPlayerStartMoving(dir)) {
            IsPlayerMoving = true;
            OnPlayerStartMoving?.Invoke();
        }
        else if (ShouldPlayerStopMoving(dir)) {
            IsPlayerMoving = false;
            OnPlayerStopMoving?.Invoke();
        }
    }

    private bool ShouldPlayerStartMoving(Vector2 dir) {
        if (IsPlayerMoving) return false;
        return Math.Abs(dir.x) > 0 || Math.Abs(dir.y) > 0;
    }

    private bool ShouldPlayerStopMoving(Vector2 dir) {
        if (!IsPlayerMoving) return false;
        return Math.Abs(dir.x) <= 0 && Math.Abs(dir.y) <= 0;
    }

    private void UpdateRotation(Vector2 dir) {
        if (!zAxisRotationPresets.ContainsKey(dir)) {
            if (player.IsCarryingObject)
                FallbackToInitialRotation();
        }
        else
            Rb.SetRotation(ResolveRotation(dir));
    }

    private float ResolveRotation(Vector2 dir) {
        if (player.IsCarryingObject)
            return zAxisRotationPresetsInvert[dir];
        return zAxisRotationPresets[dir];
    }

    private void FallbackToInitialRotation() {
        Rb.SetRotation(onInteractionInitialRotation);
    }

    private bool CanMoveTowardDirection(Vector2 dir) {
        if (!zAxisRotationPresetsInvert.ContainsKey(dir))
            return false;
        return IsRotationWithinRange(zAxisRotationPresetsInvert[dir], -45, 45);
    }

    private bool IsRotationWithinRange(float rotation, float min, float max) {
        float angle = To180Angle(rotation) - onInteractionInitialRotation;
        if (angle <= -315 && angle > -360) return true; //Its a game jam ok! I was close, don't judge... -_-
        return angle >= min && angle <= max;
    }

    // Returns an angle between [-180, 180]
    private float To180Angle(float angle) {
        angle %= 360;
        if (angle > 180) return angle - 360;
        if (angle < -180) return angle + 360;
        return angle;
    }
}