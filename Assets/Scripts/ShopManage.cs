using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManage : MonoBehaviour
{
    [SerializeField] private GameObject shopViewBack;
    [SerializeField] private Transform shopContent;

    [HideInInspector] public GlobalEventManager eventManager;

    private List<ShopMember> members = new List<ShopMember>();

    private int userTickets = 0;
    public int UserTickets { get { return userTickets; } }

    private int userLevel = 0;
    public int UserLevel { get {  return userLevel; } }

    private void Start()
    {
        members = shopContent.GetComponentsInChildren<ShopMember>().ToList();
        
        if(members.Count > 0 )
        {
            foreach(ShopMember member in members )
            {
                member.SetupShopManager(this);
            }
        }

        eventManager.OnUserTicketsCountSent += GetUserTickets;
        eventManager.OnUserLevelCountSent += GetUserLevel;

        SendNeedUserTicketsAndLevel();
    }

    public void OpenShopView()
    {
        SendNeedUserTicketsAndLevel();

        ResetItemsAvailability();

        shopViewBack.SetActive(true);
    }

    private void ResetItemsAvailability()
    {
        if (members.Count > 0)
        {
            foreach (ShopMember member in members)
            {
                if (member.gameObject.TryGetComponent(out ShopItem shopItem))
                {
                    shopItem.Checks();
                }
            }
        }
    }

    public void CloseShopView()
    {
        shopViewBack.SetActive(false);
    }

    public void SendNeedUserTicketsAndLevel()
    {
        eventManager.SendNeedCheckUserTicketsCount();
        eventManager.SendNeedCheckUserLevelCount();
    }

    private void GetUserTickets(int userTickets)
    {
        this.userTickets = userTickets;
    }

    private void GetUserLevel(int userLevel)
    {
        this.userLevel = userLevel;
    }

    public void RemoveUserTicketsByPurchase(int itemCost)
    {
        userTickets -= itemCost;
        ResetItemsAvailability();

        eventManager.SendRemoveUserTicketsCountByPurchase(itemCost);
    }

    public void AddUserTicketsByChest(int ticketsCount)
    {
        userTickets += ticketsCount;
        ResetItemsAvailability();

        eventManager.SendTicketsAddByChest(ticketsCount);
    }
}
