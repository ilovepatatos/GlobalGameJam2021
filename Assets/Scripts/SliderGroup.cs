using System.Collections.Generic;
using UnityEngine;

public class SliderGroup : MonoBehaviour
{
    public List<SetVolume> Volumes = new List<SetVolume>();

    public void SetSliders()
    {
        foreach (var volume in Volumes)
            volume.SetSliderValue();
    }
}
