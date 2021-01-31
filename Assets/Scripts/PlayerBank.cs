using System.Globalization;
using TMPro;
using UnityEngine;

public class PlayerBank : Bank
{
    [Header("Player Bank")] 
    public float DepositeLerpTime = 2;
    public float WithdrawLerpTime = 2;
    
    [Space]
    public TMP_Text MoneyText;
    public TMP_Text ShopMoneyText;

    protected override void OnDepositeMoney(int previousAmount, int currentAmount) {
        base.OnDepositeMoney(previousAmount, currentAmount);

        StartLerp(previousAmount, currentAmount, DepositeLerpTime);
        PlayerInfoManager.SetCoins(currentAmount);
    }

    protected override void OnWithdrawMoney(int previousAmount, int currentAmount) {
        base.OnWithdrawMoney(previousAmount, currentAmount);
        
        StartLerp(previousAmount, currentAmount, WithdrawLerpTime);
        PlayerInfoManager.SetCoins(currentAmount);
    }

    private void StartLerp(int previousAmount, int currentAmount, float time) {
        LTDescr desc = LeanTween.value(gameObject, previousAmount, currentAmount, time);
        desc.setEase(LeanTweenType.easeOutExpo);
        desc.setOnUpdate((float f) => SetTexts(f));
    }

    private void SetTexts(float value) {
        MoneyText.text = value.ToString("0");
        ShopMoneyText.text = value.ToString("0");
    }
}
