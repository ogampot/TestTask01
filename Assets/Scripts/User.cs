using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour
{
    [SerializeField] private int ticketsCount = 100;
    public int TicketsCount { get { return ticketsCount; } }

    [SerializeField] private int levelCount = 1;
    public int LevelCount { get { return levelCount; } }

    private DateTime bonusTime = DateTime.MinValue;
    public DateTime BonusTime { get { return bonusTime; } }

    [HideInInspector] public GlobalEventManager eventManager;

    private UserData userData = null;
    private SaveSystem saveSystem = new SaveSystem();

    private void Start()
    {
        userData = saveSystem.Load();

        if(userData == null)
        {
            userData = new UserData();

            userData.SetUserLevel(levelCount);
            userData.SetUserTickets(ticketsCount);

            saveSystem.Save(userData);
        }
        else
        {
            //userData.SetUserTickets(100);
            //userData.SetUserLevel(1);
            //saveSystem.Save(userData);

            levelCount = userData.level;
            ticketsCount = userData.tickets;
        }

        eventManager.OnTicketsCountAdd += AddTickets;
        eventManager.OnTicketsCountRemove += RemoveTickets;

        eventManager.OnLevelButtonPressed += SetLevel;

        eventManager.OnNeedCheckUserTicketsCount += SendTicketsCount;
        eventManager.OnNeedCheckUserLevelCount += SendLevelCount;

        eventManager.OnUserBonusTimeUpdate += UpdateBonusTime;

        SendTicketsCount();
        SendLevelCount();
    }

    private void Update()
    {
        if(bonusTime < DateTime.Now)
        {
            eventManager.SendUserBonusTimeIsCame();
        }
    }

    private void UpdateBonusTime(DateTime time)
    {
        bonusTime = time;
    }

    private void AddTickets(int count)
    {
        ticketsCount += count;

        userData.SetUserTickets(ticketsCount);
        saveSystem.Save(userData);

        SendTicketsCount();
    }

    private void RemoveTickets(int count)
    {
        ticketsCount -= count;

        userData.SetUserTickets(ticketsCount);
        saveSystem.Save(userData);

        SendTicketsCount();
    }

    private void SendTicketsCount()
    {
        eventManager.SendUserTicketsCount(ticketsCount);
    }

    private void SetLevel(int level)
    {
        if (level > levelCount)
        {
            levelCount = level;

            userData.SetUserLevel(levelCount);
            saveSystem.Save(userData);
        }
        SendLevelCount();
    }

    private void SendLevelCount()
    {
        eventManager.SendUserLevelCount(levelCount);
    }
}
