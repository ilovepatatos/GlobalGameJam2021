using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [Header("Shop")] public GameObject ShopCanvas;
    public TMP_Text MoneyText;
    public GameObject BuyCanvas, SellCanvas;

    [Space] public List<ShopItem> Items;

    //
    private Bank currentClientBank;
    private ShopItem currentSelectItem;

    //Event call by button
    public void StartShopping(Bank bank, bool openSellFirst) {
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
        if (currentSelectItem != null)
            Buy(currentSelectItem);
    }
}