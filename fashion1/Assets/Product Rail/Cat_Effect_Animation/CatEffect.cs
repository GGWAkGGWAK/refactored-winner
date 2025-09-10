using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEffect : MonoBehaviour
{
    ClothesProductRail clothesProductRail;
    private void Start()
    {
        clothesProductRail = transform.parent.parent.GetComponent<ClothesProductRail>();
    }
    public void Product_Result_Item()
    {
        clothesProductRail.Product_Result_Item();
    }
}
