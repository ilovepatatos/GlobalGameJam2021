using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create LeanTweenConfig", fileName = "LeanTweenConfig", order = 0)]
public class LeanTweenConfig : ScriptableObject
{
    public LeanTweenType TweenType;
    public AnimationCurve Curve;
    public bool PingPong;
    public float Time;
    public DataType DataType;
    public TransformType TransformType;
    public Vector3 from;
    public Vector3 to;

    private Vector3 initialValue;
    
    public LTDescr Create(GameObject gameObject)
    {
        LTDescr ltDescr;
        switch (DataType)
        {
            case DataType.Float:
                ltDescr = LeanTween.value(gameObject, from.x, to.x, Time);
                break;
            case DataType.Vector2:
                ltDescr = LeanTween.value(gameObject, (Vector2)from,(Vector2)to, Time);
                break;
            case DataType.Vector3:
                ltDescr = LeanTween.value(gameObject, from, to, Time);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        
        switch (TransformType)
        {
            case TransformType.Position:

                if (gameObject.TryGetComponent<RectTransform>(out var component))
                {
                    initialValue = component.anchoredPosition;
                    ltDescr.setOnUpdate(f => component.anchoredPosition = initialValue + f);
                }
                else
                {
                    initialValue = gameObject.transform.localPosition;
                    ltDescr.setOnUpdate(f => gameObject.transform.localPosition = initialValue + f); 
                }
                break;
            case TransformType.Rotation:
                initialValue = gameObject.transform.rotation.eulerAngles;
                ltDescr.setOnUpdate(f => gameObject.transform.rotation = Quaternion.Euler(initialValue + f));
                break;
            case TransformType.Scale:
                initialValue = gameObject.transform.localScale;
                ltDescr.setOnUpdate(f => gameObject.transform.localScale = initialValue + f);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if(TweenType == LeanTweenType.animationCurve)
            ltDescr.setEase(Curve);
        else
            ltDescr.setEase(TweenType);

        if (PingPong)
            ltDescr.setLoopPingPong();
        
        return ltDescr;
    }
}

public enum DataType { Float, Vector2, Vector3}
public enum TransformType { Position, Rotation, Scale }