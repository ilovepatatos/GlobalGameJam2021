using UnityEngine;

public class Bank : MonoBehaviour
{
    private int currentMoneyAmount;

    public int MoneyAmount => currentMoneyAmount;
    
    public void Deposite(int amount) {
        if (amount <= 0) 
            return;
        
        int previousAmount = currentMoneyAmount;
        currentMoneyAmount += amount;
        OnDepositeMoney(previousAmount, currentMoneyAmount);
    }

    public bool TryWithdraw(int amount) {
        if (!HasAmount(amount)) 
            return false;
        Withdraw(amount);
        return true;
    }

    private void Withdraw(int amount) {
        int previousAmount = currentMoneyAmount;
        currentMoneyAmount -= amount;
        OnWithdrawMoney(previousAmount, currentMoneyAmount);
    }

    public bool HasAmount(int amount) {
        return currentMoneyAmount - amount >= 0;
    }

    protected virtual void OnDepositeMoney(int previousAmount, int currentAmount) { }
    protected virtual void OnWithdrawMoney(int previousAmount, int currentAmount) { }
}
