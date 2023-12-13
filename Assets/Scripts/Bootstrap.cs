using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private User user;
    [SerializeField] private MainViewManage mainViewManager;
    [SerializeField] private DailyBonusManage dailyBonusViewManager;
    [SerializeField] private LevelManage levelManager;
    [SerializeField] private ShopManage shopManager;

    private GlobalEventManager eventManager;

    private void Awake()
    {
        eventManager = new GlobalEventManager();

        user.eventManager = eventManager;
        mainViewManager.eventManager = eventManager;
        dailyBonusViewManager.eventManager = eventManager;
        levelManager.eventManager = eventManager;
        shopManager.eventManager = eventManager;
    }
}
