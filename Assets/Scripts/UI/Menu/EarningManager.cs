using System;
using YG;

public static class EarningManager
{
    private static EarningManagerUI earningManagerUI;

    public static Action lackCoins;
    public static Action lackGems;
    public static Action<int, int> changeEarnings;

    private static void UpdateEarningsUI()
    {
        if (earningManagerUI != null)
            changeEarnings.Invoke(YandexGame.savesData.coins, YandexGame.savesData.gems);
    }

    public static void AddCoin(int count)
    {
        YandexGame.savesData.coins += count;
        UpdateEarningsUI();
    }

    public static bool SpendCoin(int count)
    {
        int balance = YandexGame.savesData.coins - count;

        if (balance < 0)
        {
            lackCoins.Invoke();
            return false;
        }

        YandexGame.savesData.coins = balance;
        UpdateEarningsUI();
        return true;
    }

    public static void AddGem(int count)
    {
        YandexGame.savesData.gems += count;
        UpdateEarningsUI();
    }

    public static bool SpendGem(int count)
    {
        int balance = YandexGame.savesData.gems - count;

        if (balance < 0)
        {
            lackGems.Invoke();
            return false;
        }

        YandexGame.savesData.coins += balance;
        UpdateEarningsUI();
        return true;
    }
}
