using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5;
    [HideInInspector] public bool IsPlayerMoving;

    private Player player;
    private Rigidbody2D rb;

    public Action OnPlayerStartMoving, OnPlayerStopMoving;

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

    private void Awake() {
        player = GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();

        player.OnInteractionBegin += obj => { rb.centerOfMass = transform.position + transform.up * 5; };
    }

    private void FixedUpdate() {
        Vector2 dir = player.Input.MovementDirection;
        UpdateMovement(dir, Time.fixedDeltaTime);
        UpdateRotation(dir);
        UpdateAnimator(dir);
    }

    private void UpdateAnimator(Vector2 dir) {
        player.Animator.SetFloat("MovementSpeed", Math.Max(Math.Abs(dir.x), Math.Abs(dir.y)));
    }

    private void UpdateMovement(Vector2 dir, float delta) {
        ResolveMovement(dir);
        rb.MovePosition((Vector2) transform.position + dir.normalized * (delta * Speed));
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
        if (zAxisRotationPresets.ContainsKey(dir))
            rb.SetRotation(ResolveRotation(dir));
    }

    private float ResolveRotation(Vector2 dir) {
        return zAxisRotationPresets[dir];
    }
}