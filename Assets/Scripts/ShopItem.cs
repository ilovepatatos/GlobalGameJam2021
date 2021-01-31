using TMPro;
using UnityEngine;

[RequireComponent(typeof(CustomButton))]
public class ShopItem : Item
{
    [Header("UI")] public bool UnlockOnStart = false;
    public TMP_Text PriceText;

    [Space] [SerializeField] private Color boughtColor;
    [SerializeField] private Color canAffordColor;
    [SerializeField] private Color cantAffordColor;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color unselectedColor;

    [Space] public SoundSettings OnClickSound;
    public SoundSettings OnHoverSound;

    [HideInInspector] public bool HasBeenBought;

    private CustomButton button;
    public Shop myShop;

    private void Awake() {
        button = GetComponent<CustomButton>();
        PriceText.text = Price.ToString();
    }

    private void Start() {
        if (UnlockOnStart) {
            HasBeenBought = true;
            PriceText.text = "Equipped";
            SetButtonEnable(false);
            SetButtonColor(boughtColor);
            myShop.CurrentEquippedItem = this;
        }
    }

    public void OnButtonClick() {
        SoundManager.PlayOneShot(OnClickSound);

        Debug.Log($"OnClick {ShortName}");
        if (HasBeenBought) {
            Debug.Log("In here");
            Equip();
            button.Unselect();
            return;
        }

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
        Unlock();
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

    private void Unlock() {
        HasBeenBought = true;
        SetButtonColor(boughtColor);
        Equip();
    }

    private void SetButtonColor(Color color) {
        button.image.color = color;
    }

    private void Equip() {
        PriceText.text = "Equipped";
        SetButtonEnable(false);
        myShop.OnEquipmentEquipped(this);
    }

    public void UnEquip() {
        PriceText.text = "Equip";
        SetButtonEnable(true);
    }
}