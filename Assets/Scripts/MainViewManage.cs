using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainViewManage : MonoBehaviour
{
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject mainViewBack;

    [SerializeField] private ShopManage shopManager;

    [SerializeField] private TextMeshProUGUI userTicketsCountLabel;

    [SerializeField] private GameObject submenuHolder;

    [SerializeField] private DailyBonusManage dailyBonusViewManager;
    [SerializeField] private GameObject settingsView;

    [HideInInspector] public GlobalEventManager eventManager;

    private void Start()
    {
        eventManager.OnUserTicketsCountSent += UpdateUserTicketCounter;

        eventManager.SendNeedCheckUserTicketsCount();

        OpenMainView();
        CloseSubmenu();
    }

    public void OpenMainView()
    {
        mainViewBack.SetActive(true);

        hud.SetActive(false);

        eventManager.SendNeedCheckUserTicketsCount();
    }

    public void CloseMainView()
    {
        mainViewBack.SetActive(false);

        hud.SetActive(true);
    }

    public void OpenShopView()
    {
        shopManager.OpenShopView();
    }

    public void CloseShopView()
    {
        shopManager.CloseShopView();

        eventManager.SendNeedCheckUserTicketsCount();
    }

    private void OpenSubmenu(GameObject submenuObject)
    {
        submenuHolder.SetActive(true);

        submenuObject.SetActive(true);
    }

    public void OpenBonuses()
    {
        OpenSubmenu(dailyBonusViewManager.gameObject);
        dailyBonusViewManager.OpenBonusView();
    }

    public void OpenSettings()
    {
        OpenSubmenu(settingsView);
    }

    public void CloseSubmenu()
    {
        submenuHolder.SetActive(false);

        dailyBonusViewManager.CLoseBonusView();
        settingsView.SetActive(false);
    }

    public void UpdateUserTicketCounter(int count)
    {
        userTicketsCountLabel.text = count.ToString();
    }
}
