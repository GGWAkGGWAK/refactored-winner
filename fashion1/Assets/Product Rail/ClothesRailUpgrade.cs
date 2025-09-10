using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClothesRailUpgrade : MonoBehaviour
{
    PlayerInfo playerinfo;
    SystemInfo systeminfo;
    ClothesProductRail clothes_rail;
    RecipeBook recipebook;

    public Image product_itme_sprite;

    [Header ("Upgrade")]
    public int product_speed_level;
    public float product_speed_price;
    public float product_speed;

    [Space(10f)]
    public int product_amount_level;
    public float product_amount_price;
    public int product_amount;

    [Space(10f)]
    public int product_ingredient_decrease_level;
    public float product_ingredient_decrease_price;
    public float product_ingredient_decrease;


    void Start()
    {
        playerinfo = GameObject.Find("Playerinfo").GetComponent<PlayerInfo>();
        systeminfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        recipebook = GameObject.Find("Recipe_Book").GetComponent<RecipeBook>();
        clothes_rail = GetComponent<ClothesProductRail>();

        product_speed_level = 0;
        product_speed_price = 50f;
        product_speed = 0.1f;

        product_amount_level = 0;
        product_amount_price = 50f;
        product_amount = 1;

        product_ingredient_decrease_level = 0;
        product_ingredient_decrease_price = 500f;
        product_ingredient_decrease = 1f;

}

    void Update()
    {
        //product_itme_sprite.sprite = clothes_rail.product_item_sprite.sprite;
    }

    public void Product_Speed_Upgrade()
    {
        product_speed_price = Mathf.Pow(10, product_speed_level) * 50; //비용 계산

        if(playerinfo.player_gold >=  product_speed_price)
        {
            clothes_rail.rail_product_speed += product_speed; //한 레일 생산 속도 + 생산 속도 증가량
            product_speed_level++; //레벨 증가
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void Product_Amount_Upgrade()
    {
        product_amount_price = Mathf.Pow(10, product_amount_level) * 50; //비용 계산

        if (playerinfo.player_gold >= product_amount_price)
        {
            //생산량 증가
            product_amount_level++; //레벨 증가
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void Product_Ingredient_Decrease_Upgrade()
    {
        product_ingredient_decrease_price = Mathf.Pow(10, product_ingredient_decrease_level) * 50; //비용 계산

        if (playerinfo.player_gold >= product_ingredient_decrease_price)
        {
            // 재료 감소
            product_ingredient_decrease_level++; //레벨 증가
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }
}
