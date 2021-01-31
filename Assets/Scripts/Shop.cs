using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Shop")] public Dialog WinDialog;

    [Space] public GameObject ShopCanvas;
    public TMP_Text MoneyText;
    public GameObject BuyCanvas, SellCanvas;

    [Space] public List<ShopItem> Items;

    //
    [HideInInspector] public Interacter currentInteracter;
    private Bank currentClientBank;
    private ShopItem currentSelectItem;
    [HideInInspector] public ShopItem CurrentEquippedItem;

    //Event call by button
    public void StartShopping(Interacter interacter, Bank bank, bool openSellFirst) {
        currentInteracter = interacter;
        currentClientBank = bank;

        RefreshItems();

        ShopCanvas.SetActive(true);
        BuyCanvas.SetActive(!openSellFirst);
        SellCanvas.SetActive(openSellFirst);
        MoneyText.text = bank.MoneyAmount.ToString();
    }

    //Event call by button
    public void StopShopping() {
        UnselectCurrentItem();
        ShopCanvas.SetActive(false);
        currentClientBank = null;
    }

    public void Buy(ShopItem item) {
        item.Buy(currentClientBank);
        RefreshItems();
    }

    public void SelectItem(ShopItem item) {
        UnselectCurrentItem();
        currentSelectItem = item;
        item.Select();
    }

    public void UnselectCurrentItem() {
        if (currentSelectItem == null)
            return;
        currentSelectItem.Unselect(currentClientBank);
        currentSelectItem = null;
    }

    public void RefreshItems() {
        foreach (ShopItem item in Items) {
            item.OnShopOpen(this, currentClientBank);
        }
    }

    public void OnBuyButtonPressed() {
        if (!currentSelectItem)
            return;

        Buy(currentSelectItem);
        if (AmountItemsMissing() <= 0) {
            WinDialog.Start();
            StopShopping();
            currentInteracter.TryTerminateInteraction();
        }
    }

    public void OnEquipmentEquipped(ShopItem item) {
        if (!(currentInteracter is Player player))
            return;
        if (CurrentEquippedItem)
            CurrentEquippedItem.UnEquip();

        player.Armory.Equip(item.Category);
        CurrentEquippedItem = item;
    }

    private int AmountItemsMissing() {
        int amount = 0;
        foreach (ShopItem item in Items) {
            if (!item.HasBeenBought)
                amount++;
        }
        return amount;
    }
}