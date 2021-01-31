using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class InteractPopup : MonoBehaviour
{
    [Header("Popup")] 
    public float TravelTime = 0.5f;
    public float TravelDistanceY = 10;

    private RectTransform rectTransform;
    private float yValueInitial;

    private LTDescr currentTween;

    public void PopUp() {
        if(currentTween != null)
            LeanTween.cancel(gameObject);
        
        currentTween = LeanTween.value(gameObject, rectTransform.anchoredPosition.y, yValueInitial + TravelDistanceY, TravelTime);
        currentTween.setOnUpdate(UpdatePosition);
    }

    public void PopDown() {
        if(currentTween != null)
            LeanTween.cancel(gameObject);
        
        currentTween = LeanTween.value(gameObject, rectTransform.anchoredPosition.y, yValueInitial, TravelTime);
        currentTween.setOnUpdate(UpdatePosition);
    }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();

        //Save initial Y position
        yValueInitial = rectTransform.anchoredPosition.y;
        Debug.Log($"Initial value: {yValueInitial}");
    }

    private void UpdatePosition(float y) {
        Vector3 pos = rectTransform.anchoredPosition;
        pos.y = y;
        rectTransform.anchoredPosition = pos;
    }
}