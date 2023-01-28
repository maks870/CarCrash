using System;
using YG;

public static class EarningManager
{
    public static Action OnLackCoins;
    public static Action OnLackGems;
    public static Action OnChangeEarnings;


    public static void AddLootbox()
    {
        YandexGame.savesData.lootboxes += 1;
        OnChangeEarnings.Invoke();
    }

    public static bool SpendLootbox()
    {
        if (YandexGame.savesData.lootboxes <= 0)
            return false;

        YandexGame.savesData.lootboxes -= 1;
        OnChangeEarnings.Invoke();
        return true;
    }

    public static void AddCoin(int count)
    {
        YandexGame.savesData.coins += count;
        OnChangeEarnings.Invoke();
    }

    public static bool SpendCoin(int count)
    {
        int balance = YandexGame.savesData.coins - count;

        if (balance < 0)
        {
            OnLackCoins.Invoke();
            return false;
        }

        YandexGame.savesData.coins = balance;
        OnChangeEarnings.Invoke();
        return true;
    }

    public static void AddGem(int count)
    {
        YandexGame.savesData.gems += count;
        OnChangeEarnings.Invoke();
    }

    public static bool SpendGem(int count)
    {
        int balance = YandexGame.savesData.gems - count;

        if (balance < 0)
        {
            OnLackGems.Invoke();
            return false;
        }

        YandexGame.savesData.gems = balance;
        OnChangeEarnings.Invoke();
        return true;
    }
}
