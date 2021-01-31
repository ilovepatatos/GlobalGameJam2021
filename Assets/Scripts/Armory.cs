using System.Collections.Generic;
using UnityEngine;

public class Armory : MonoBehaviour
{
    [Header("Armory")]
    public List<Armor> Armors;
    
    [HideInInspector] public Player player;
    
    private Dictionary<Category, Armor> armorsDictionary = new Dictionary<Category, Armor>();

    private void Awake() {
        foreach (Armor armor in Armors) {
            armorsDictionary.Add(armor.Category, armor);
        }
    }

    public void Equip(Category category) {
        if (!armorsDictionary.ContainsKey(category))
            return;
        armorsDictionary[category].Equip(player);
    }
}