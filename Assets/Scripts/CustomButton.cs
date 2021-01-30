using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CustomButton : UnityEngine.UI.Button
{
    public UnityEvent OnEnter = new UnityEvent();
    public UnityEvent OnDown = new UnityEvent();
    public UnityEvent OnUp = new UnityEvent();
    public UnityEvent OnExit = new UnityEvent();
    

    public override void OnPointerEnter(PointerEventData eventData)
    {
        OnEnter?.Invoke();
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        OnDown?.Invoke();
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        OnUp?.Invoke();
        base.OnPointerUp(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        OnExit?.Invoke();
        base.OnPointerExit(eventData);
    }
}
