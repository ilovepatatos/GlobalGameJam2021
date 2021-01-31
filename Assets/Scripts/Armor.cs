using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Armor : Item
{
    [Header("Armor")] 
    public SoundSettings EquipSound;
    [HideInInspector] public Animator Animator;

    private void Awake() {
        Animator = GetComponent<Animator>();
    }

    public void Equip(Player player) {
        SoundManager.PlayOneShot(EquipSound);
        
        //Deactivate old armor
        player.ArmorAnimator.gameObject.SetActive(false);
        //Set new armor animator
        gameObject.SetActive(true);
        player.ArmorAnimator = Animator;
    }
}