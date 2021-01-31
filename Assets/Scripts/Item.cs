using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item")] 
    public string ShortName;
    [TextArea(1, 3)] public string Description;
    
    [Space]
    public int Price;
    public Category Category;

    public virtual void Buy(Bank bank) {
        bank.TryWithdraw(Price);
    }

    public virtual bool CanBuy(int bankAmount) {
        return bankAmount >= Price;
    }
}

public enum Category
{
    Armor01,
    Armor02,
    Armor03,
    Armor04,
    Armor05,
    Armor06
}