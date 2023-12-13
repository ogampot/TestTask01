using System;
using UnityEngine;

public class GlobalEventManager
{
    public Action<int> OnUserTicketsCountSent;
    public void SendUserTicketsCount(int count)
    {
        OnUserTicketsCountSent?.Invoke(count);
    }

    public Action OnNeedCheckUserTicketsCount;
    public void SendNeedCheckUserTicketsCount()
    {
        OnNeedCheckUserTicketsCount?.Invoke();
    }

    public Action<int> OnTicketsCountAdd;
    public void SendRewardActivated(int count)
    {
        OnTicketsCountAdd?.Invoke(count);
    }

    public void SendTicketsAddByChest(int count)
    {
        OnTicketsCountAdd?.Invoke(count);
    }

    public Action OnRewardButtonPressed;
    public void SendRewardButtonPressed()
    {
        OnRewardButtonPressed?.Invoke();
    }

    public Action<int> OnUserLevelCountSent;
    public void SendUserLevelCount(int count)
    {
        OnUserLevelCountSent?.Invoke(count);
    }

    public Action OnNeedCheckUserLevelCount;
    public void SendNeedCheckUserLevelCount()
    {
        OnNeedCheckUserLevelCount?.Invoke();
    }

    public Action<int> OnLevelButtonPressed;
    public void SendLevelButtonPressed(int level)
    {
        OnLevelButtonPressed?.Invoke(level);
    }

    public Action<int> OnTicketsCountRemove;
    public void SendRemoveUserTicketsCountByPurchase(int count)
    {
        OnTicketsCountRemove?.Invoke(count);
    }

    public Action OnUserBonusTimeIsCame;
    public void SendUserBonusTimeIsCame()
    {
        OnUserBonusTimeIsCame?.Invoke();
    }

    public Action<DateTime> OnUserBonusTimeUpdate;
    public void SendUpdateUserBonusTime(DateTime time)
    {
        OnUserBonusTimeUpdate?.Invoke(time);
    }
}
