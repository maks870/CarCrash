using System;
using YG;

public static class EarningManager
{
    private static EarningUIController earningManagerUI;

    public static Action lackCoins;
    public static Action lackGems;
    public static Action changeEarnings;




    public static void AddCoin(int count)
    {
        YandexGame.savesData.coins += count;
        changeEarnings.Invoke();
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
        changeEarnings.Invoke();
        return true;
    }

    public static void AddGem(int count)
    {
        YandexGame.savesData.gems += count;
        changeEarnings.Invoke();
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
        changeEarnings.Invoke();
        return true;
    }
}
