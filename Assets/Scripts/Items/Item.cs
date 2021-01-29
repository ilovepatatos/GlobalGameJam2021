using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item")]
    public string ShortName;
    public ItemCategory Category;
    
    [Space]
    [TextArea] public string Description;
    
    
}

public enum ItemCategory
{
    Default,
    Ring,
    HermitHouse
}
