using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class PurchasePopupButton : MonoBehaviour
{
    enum PurchaseType
    {
        Weekly,
        Monthly,
        Restore
    }

    [SerializeField] PurchaseType purchaseType;


    public void OnPurchaseSuccess(Product product)
    {
        if (purchaseType == PurchaseType.Weekly)
        {
            
        }
        else if(purchaseType == PurchaseType.Monthly)
        {

        }
        else if(purchaseType == PurchaseType.Restore)
        {

        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
    {

    }


    public void OnProductFetchedEvent(Product product)
    {
        Debug.Log("  OnProductFetchedEvent  ");
    }
}
