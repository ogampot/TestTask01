using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : ShopMember
{
    [SerializeField] private string itemName = "Default";
    [SerializeField] private TextMeshProUGUI itemNameLabel;

    [SerializeField] private Sprite itemImageSprite;
    [SerializeField] private Image itemImage;

    [SerializeField] private GameObject lockImage;

    [SerializeField] private int levelCount = 1;
    [SerializeField] private TextMeshProUGUI levelCountLabel;

    [SerializeField] private int itemCost = 500;
    [SerializeField] private TextMeshProUGUI itemCostLabel;

    [SerializeField] private GameObject ticketImage;

    [SerializeField] private Button itemBuyButton;

    [SerializeField] private GameObject purchasedCheck;

    [SerializeField] private bool isPurchased = false;
    public bool IsPurchased { get { return isPurchased; } }

    private void Start()
    {
        itemNameLabel.text = itemName;
        itemImage.sprite = itemImageSprite;
        levelCountLabel.text = "LV. " + levelCount.ToString();
        itemCostLabel.text = itemCost.ToString();

        itemBuyButton.onClick.AddListener(TryToBuyItem);

        ticketImage.SetActive(true);

        lockImage.SetActive(false);
        purchasedCheck.SetActive(false);

        Checks();
    }

    public void Checks()
    {
        CheckItemPurchased();
        CheckItemAvailability();
    }

    private void CheckItemAvailability()
    {
        if (shopManager.UserLevel < levelCount)
        {
            lockImage.SetActive(true);

            levelCountLabel.enabled = true;

            itemImage.enabled = false;
            itemBuyButton.enabled = false;
        }
        else
        {
            lockImage.SetActive(false);

            levelCountLabel.enabled = false;

            itemImage.enabled = true;
            itemBuyButton.enabled = true;
        }

        if(shopManager.UserTickets < itemCost) itemBuyButton.enabled = false;
        else itemBuyButton.enabled = true;
    }

    private void TryToBuyItem()
    {
        if (shopManager.UserTickets >= itemCost)
        {
            isPurchased = true;
            shopManager.RemoveUserTicketsByPurchase(itemCost);
            CheckItemPurchased();
        }
    }

    private void CheckItemPurchased()
    {
        if (isPurchased)
        {
            itemCostLabel.enabled = false;
            itemBuyButton.enabled = false;

            ticketImage.SetActive(false);

            purchasedCheck.SetActive(true);
        }
        else
        {
            itemCostLabel.enabled = true;
            itemBuyButton.enabled = true;

            ticketImage.SetActive(true);

            purchasedCheck.SetActive(false);
        }
    }
}
