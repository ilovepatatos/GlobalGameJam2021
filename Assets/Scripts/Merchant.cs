using System.Collections.Generic;
using UnityEngine;

public class Merchant : Interactable
{
    [Header("Merchant")] 
    public Collider2D SellingZone;
    
    [Space]
    public Dialog Dialog;
    public Dialog NothingToSell;
    
    public override void OnInteractionStart(Interacter interacter) {
        base.OnInteractionStart(interacter);
        
        //Sell objects in sell zone
        if (interacter is Player player) {
            Dialog.OnDialogStart += () => player.playerMovement.SetEnableMovement(false);
            Dialog.OnDialogStop += () => player.playerMovement.SetEnableMovement(true);
            Dialog.Start();
            
            RetrieveObjectInSellingZone(out List<LostObject> objectsInSellingZone);
            if(objectsInSellingZone.Count > 0)
                SellObjects(player.Bank, objectsInSellingZone);
            else
                OnSellEmtpySellZone();
        }

        interacter.TryTerminateInteraction();
    }

    public override void OnInteractionStop() {
        base.OnInteractionStop();
    }

    protected virtual void OnSellEmtpySellZone() {
        NothingToSell.Start();
    }

    private void SellObjects(Bank bank, List<LostObject> objects) {
        int amountMoney = 0;
        foreach (LostObject obj in objects) {
            amountMoney += obj.Price;
            obj.Sell();
        }
        bank.Deposite(amountMoney);
    }

    private void RetrieveObjectInSellingZone(out List<LostObject> objects) {
        objects = new List<LostObject>();
        List<Collider2D> hits = new List<Collider2D>();
        
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Interactable");
        filter.useTriggers = true;
        
        if (SellingZone.OverlapCollider(filter, hits) <= 0)
            return;

        foreach (Collider2D hit in hits) {
            if(hit.TryGetComponent(out LostObject obj))
                objects.Add(obj);
        }
    }
}
