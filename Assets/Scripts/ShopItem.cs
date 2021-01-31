using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : Item
{
    [Header("UI")] public TMP_Text PriceText;

    [Space] [SerializeField] private Color boughtColor;
    [SerializeField] private Color canAffordColor;
    [SerializeField] private Color cantAffordColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;

    [Space] public SoundSettings OnClickSound;
    public SoundSettings OnHoverSound;

    [HideInInspector] public bool HasBeenBought;

    private Button button;
    private Shop myShop;

    private void Awake() {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
        PriceText.text = Price.ToString();
    }

    public void OnButtonClick() {
        SoundManager.PlayOneShot(OnClickSound);

        if (!myShop) {
            Debug.LogWarning("Missing shop exception!");
            return;
        }

        myShop.SelectItem(this);
    }

    public void OnHoverButton() {
        SoundManager.PlayOneShot(OnHoverSound);
    }

    public override void Buy(Bank bank) {
        base.Buy(bank);
        HasBeenBought = true;
        SetButtonColor(boughtColor);
        SetButtonEnable(false);
    }

    public void OnShopOpen(Shop shop, Bank bank) {
        myShop = shop;

        if (HasBeenBought)
            return;

        bool canAfford = CanBuy(bank.MoneyAmount);
        SetButtonColor(canAfford ? canAffordColor : cantAffordColor);
        SetButtonEnable(canAfford);
    }

    public void SetButtonEnable(bool enable) {
        button.interactable = enable;
    }

    public void Select() {
        SetButtonColor(selectedColor);
    }

    public void Unselect(Bank bank) {
        if (!HasBeenBought)
            SetButtonColor(bank.HasAmount(Price) ? canAffordColor : cantAffordColor);
    }

    private void SetButtonColor(Color color) {
        button.image.color = color;
    }
}