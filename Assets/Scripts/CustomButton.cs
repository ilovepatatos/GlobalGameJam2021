using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : UnityEngine.UI.Button
{
    public UnityEvent OnEnter = new UnityEvent();
    public UnityEvent OnDown = new UnityEvent();
    public UnityEvent OnUp = new UnityEvent();
    public UnityEvent OnExit = new UnityEvent();

    public void Unselect() => FindObjectOfType<EventSystem>().SetSelectedGameObject(null);

    public override void OnPointerEnter(PointerEventData eventData) {
        if (!IsInteractable())
            return;
        OnEnter?.Invoke();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;
        OnDown?.Invoke();
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;
        OnUp?.Invoke();
        base.OnPointerUp(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!IsInteractable())
            return;
        OnExit?.Invoke();
        base.OnPointerExit(eventData);
    }
}
