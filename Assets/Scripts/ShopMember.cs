using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMember : MonoBehaviour
{
    protected ShopManage shopManager;

    public void SetupShopManager(ShopManage shopManager)
    {
        this.shopManager = shopManager;
    }
}
