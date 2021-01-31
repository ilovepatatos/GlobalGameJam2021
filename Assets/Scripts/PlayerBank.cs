using TMPro;
using UnityEngine;

public class PlayerBank : Bank
{
    [Header("Player Bank")] 
    public float DepositeLerpTime = 2;
    public float WithdrawLerpTime = 2;
    
    [Space]
    public TMP_Text MoneyText;

    protected override void OnDepositeMoney(int previousAmount, int currentAmount) {
        base.OnDepositeMoney(previousAmount, currentAmount);

        LTDescr desc = LeanTween.value(gameObject, previousAmount, currentAmount, DepositeLerpTime);
        desc.setEase(LeanTweenType.easeOutExpo);
        desc.setOnUpdate((float f) => { MoneyText.text = $"{f:0}"; });

        PlayerInfoManager.SetCoins(currentAmount);
    }

    protected override void OnWithdrawMoney(int previousAmount, int currentAmount) {
        base.OnWithdrawMoney(previousAmount, currentAmount);
        
        LTDescr desc = LeanTween.value(gameObject, previousAmount, currentAmount, WithdrawLerpTime);
        desc.setEase(LeanTweenType.easeOutExpo);
        desc.setOnUpdate((float f) => { MoneyText.text = $"{f:0}"; });
        
        PlayerInfoManager.SetCoins(currentAmount);
    }
}
