using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicketsChest : ShopMember
{
    [SerializeField] private string chestName = "Default";
    [SerializeField] private TextMeshProUGUI chestNameLabel;

    [SerializeField] private int chestTicketsCount = 100;
    [SerializeField] private TextMeshProUGUI chestTicketsCountLabel;

    [SerializeField] private float chestCost = 1.99f;
    [SerializeField] private TextMeshProUGUI chestCostLabel;

    [SerializeField] private Button chestBuyButton;

    private void Start()
    {
        chestNameLabel.text = chestName + " chest";
        chestTicketsCountLabel.text = "x" + chestTicketsCount.ToString();
        chestCostLabel.text = "$" + chestCost.ToString();

        chestBuyButton.onClick.AddListener(SendTicketsAdd);
    }

    private void SendTicketsAdd()
    {
        shopManager.AddUserTicketsByChest(chestTicketsCount);
    }
}
