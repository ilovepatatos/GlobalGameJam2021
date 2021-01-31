using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item")] 
    public string ShortName;
    [TextArea(1, 3)] public string Description;
    public int Price;

    public virtual void Buy(Bank bank) {
        bank.TryWithdraw(Price);
    }

    public virtual bool CanBuy(int bankAmount) {
        return bankAmount >= Price;
    }
}