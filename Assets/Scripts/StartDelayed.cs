using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class StartDelayed : MonoBehaviour
{
    [Header("Delay")] 
    public float Min;
    public float Max;
    
    private void Start() {
        //GetComponent<AudioSource>().PlayDelayed(Random.Range(Min, Max));
    }
}
