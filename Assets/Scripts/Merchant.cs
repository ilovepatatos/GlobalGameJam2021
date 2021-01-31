using System.Collections.Generic;
using UnityEngine;

public class Merchant : Interactable
{
    [Header("Merchant")] public Collider2D SellingZone;

    [Space] public Dialog Dialog;
    public Dialog NothingToSell;

    [Space] public Shop Shop;

    public void OnSellButtonPressed() {
        if (Interacter is Player player) {
            RetrieveObjectInSellingZone(out List<LostObject> objectsInSellingZone);
            SellObjects(player.Bank, objectsInSellingZone);
        }
    }

    //TODO Should be in player
    public void OnExitShop() {
        if (Interacter)
            Interacter.TryTerminateInteraction();
    }

    public override void OnInteractionStart(Interacter interacter) {
        base.OnInteractionStart(interacter);

        if (!(interacter is Player player)) {
            interacter.TryTerminateInteraction();
            return;
        }

        player.playerMovement.SetEnableMovement(false);
        UIManager.SetPauseButtonActive(false);
        UIManager.SetInGameCoinActive(false);
        Shop.StartShopping(interacter, player.Bank, !IsSellZoneEmpty());
    }

    public override void OnInteractionStop() {
        if (Interacter is Player player) {
            player.playerMovement.SetEnableMovement(true);
            UIManager.SetPauseButtonActive(true);
            UIManager.SetInGameCoinActive(true);
        }

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
            if (hit.TryGetComponent(out LostObject obj))
                objects.Add(obj);
        }
    }

    private bool IsSellZoneEmpty() {
        List<Collider2D> hits = new List<Collider2D>();
        
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = LayerMask.GetMask("Interactable");
        filter.useTriggers = true;

        SellingZone.OverlapCollider(filter, hits);
        return hits.Count <= 2;
    }
}