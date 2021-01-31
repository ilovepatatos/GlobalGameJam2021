using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Shop")] public GameObject ShopCanvas;

    [Space] public List<ShopItem> Items;

    //
    private Bank currentClientBank;
    private ShopItem currentSelectItem;

    //Event call by button
    public void StartShopping(Bank bank) {
        currentClientBank = bank;

        foreach (ShopItem item in Items) {
            item.OnShopOpen(this, bank);
        }

        ShopCanvas.SetActive(true);
    }

    //Event call by button
    public void StopShopping() {
        currentClientBank = null;
        UnselectCurrentItem();
        ShopCanvas.SetActive(false);
    }

    public void Buy(ShopItem item) {
        item.Buy(currentClientBank);
    }

    public void SelectItem(ShopItem item) {
        UnselectCurrentItem();
        currentSelectItem = item;
    }

    public void UnselectCurrentItem() {
        if (currentSelectItem != null)
            currentSelectItem.Unselect(currentClientBank);
    }

    public void OnBuyButtonPressed() {
        if (currentSelectItem != null)
            Buy(currentSelectItem);
    }
}