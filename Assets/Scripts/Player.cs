using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerBank))]
public class Player : Interacter
{
    [Header("Player")] public Transform Body;
    public FixedJoint2D Joint;

    [Space] public Animator PlayerAnimator;
    public Animator LeftClawAnimator, RightClawAnimator;

    [HideInInspector] public PlayerBank Bank;
    public PlayerMovement playerMovement;

    public PlayerInputPck Input = new PlayerInputPck();

    public bool IsCarryingObject => CurrentInteractableSelected is LostObject;
    public LostObject ObjectCarrying => CurrentInteractableSelected as LostObject;

    protected override void Awake() {
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
        Bank = GetComponent<PlayerBank>();

        OnInteractableEnterRange += obj => { UIManager.SetInteractPopupActive(true); };
        OnInteractableLeaveRange += obj => { UIManager.SetInteractPopupActive(false); };
    }

    protected override void Update() {
        base.Update();
        Input.Update();

        if (Input.Interact)
            ResolveInteraction();
        if (DialogManager.IsDialogPlaying())
            if (Input.Mouse0 || Input.Space)
                DialogManager.CurrentDialog.Next();
    }

    protected override void OnInteraction(Interactable interactable) {
        base.OnInteraction(interactable);
        UIManager.SetInteractPopupActive(false);

        if (CurrentInteractableSelected is LostObject obj) {
            RightClawAnimator.SetBool("IsCarryingObject", true);
            SetPivot(obj.CarryDistance);
            playerMovement.OnPickupObject();
        }
    }

    protected override void OnTerminateInteraction(Interactable interactable) {
        base.OnTerminateInteraction(interactable);
        Joint.connectedBody = playerMovement.Rb;

        if (CurrentInteractableSelected is LostObject obj) {
            RightClawAnimator.SetBool("IsCarryingObject", false);
            SetPivot(-obj.CarryDistance);
        }
    }

    private void ResolveInteraction() {
        if (IsInteracting)
            TryTerminateInteraction();
        else
            TryInteract();
    }

    private void SetPivot(float distance) {
        Transform t = transform;
        Vector3 dir = t.up * distance;
        t.position += dir;
        Body.position -= dir;
    }
}