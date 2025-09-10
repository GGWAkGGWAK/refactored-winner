using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.NetworkInformation;

public class MainUpgrade : MonoBehaviour
{
    public int buyingRailSpeedGold;
    public int buyingRailRecipeGold;
    public int buyingRailSpecailGold;

    public int buyingtrainTimeGold;
    public int buyingtrainRecipeGold;
    public int buyingtrainGoldGold;

    public int buyingtimeShopDiscountGold;
    public int buyingtimeShopRecipeGold;
    public int buyingtimeShopSpecailGold;


    public TextMeshProUGUI railSpeedText;
    public TextMeshProUGUI railRecipeText;
    public TextMeshProUGUI railSpecialItemText;

    public TextMeshProUGUI trainTimeText;
    public TextMeshProUGUI trainSpecialItemText;
    public TextMeshProUGUI trainGoldText;

    public TextMeshProUGUI timeShopDiscountText;
    public TextMeshProUGUI timeShopRecipeText;
    public TextMeshProUGUI timeShopSpecialItemText;

    public GameObject gold_Danger;

    public GameObject pInfoObject;
    PlayerInfo pInfo;
    int railSpeedCount;
    int railRecipeCount;
    int railSpecialCount;
    int trainTimeCount;
    int trainGoldCount;
    int trainSpecialCount;
    int timeDiscountCount;
    int timeRecipeCount;
    int timeSpecialCount;
    void Start()
    {
        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();
        railSpeedCount = 1;
        railRecipeCount = 1;
        railSpecialCount = 1;
        trainTimeCount = 1;
        trainGoldCount = 1;
        trainSpecialCount = 1;
        timeDiscountCount = 1;
        timeRecipeCount = 1;
        timeSpecialCount = 1;

        buyingRailSpeedGold = 10000;
        buyingRailRecipeGold = 10000;
        buyingRailSpecailGold = 10000;
        buyingtrainTimeGold = 10000;
        buyingtrainRecipeGold = 10000;
        buyingtrainGoldGold = 10000;
        buyingtimeShopDiscountGold = 10000;
        buyingtimeShopRecipeGold = 10000;
        buyingtimeShopSpecailGold = 10000;
    }
    void Update()
    {
        railSpeedText.text = railSpeedCount.ToString() + "/24";
        railRecipeText.text = railRecipeCount.ToString() + "/24";
        railSpecialItemText.text = railSpecialCount.ToString() + "/24";

        trainTimeText.text = trainTimeCount.ToString() + "/24";
        trainGoldText.text = trainGoldCount.ToString() + "/24";
        trainSpecialItemText.text = trainSpecialCount.ToString() + "/24";

        timeShopDiscountText.text = timeDiscountCount.ToString() + "/24"; 
        timeShopRecipeText.text = timeRecipeCount.ToString() + "/24";
        timeShopSpecialItemText .text = timeSpecialCount.ToString() + "/24";
    }
    public void RailSpeedUp()
    {
        if(railSpeedCount <= 24 && pInfo.player_gold >= buyingRailSpeedGold)
        {
            pInfo.player_gold -= buyingRailSpeedGold;
            railSpeedCount++;
            pInfo.item_rail_production_speed += 0.05f;
            buyingRailSpeedGold *= 10;
        }
        else if(railSpeedCount > 25)
        {
            Debug.Log("최대 강화치 입니다!");
        }
        else if (pInfo.player_gold <  buyingRailSpeedGold)
        {
            Debug.Log("보유 골드가 부족합니다!");
            gold_Danger.SetActive(true);
        }
    }

    public void TrainTimeDown()
    {
        if(trainTimeCount <= 24 && pInfo.player_gold >= buyingtrainTimeGold)
        {
            pInfo.player_gold -= buyingtrainTimeGold;
            trainTimeCount++;
            pInfo.train_main_reward_time1 -= 1;
            pInfo.train_main_reward_time2 -= 1;
            pInfo.train_main_reward_time3 -= 1;
            buyingtrainTimeGold *= 10;
        }
        else if(trainTimeCount > 25)
        {
            Debug.Log("최대 강화치 입니다!");
        }
        else if (pInfo.player_gold < buyingtrainTimeGold)
        {
            Debug.Log("보유 골드가 부족합니다!");
            gold_Danger.SetActive(true);
        }
    }

    public void TrainGoldUp()
    {
        if(trainGoldCount <= 24 && pInfo.player_gold >= buyingtrainGoldGold)
        {
            pInfo.player_gold -= buyingtrainGoldGold;
            trainGoldCount++;
            pInfo.train_reward_multiple += 0.1f;
            buyingtrainGoldGold *= 10;
        }
        else if(trainGoldCount > 25)
        {
            Debug.Log("최대 강화치 입니다!");
        }
        else if (pInfo.player_gold < buyingtrainGoldGold)
        {
            Debug.Log("보유 골드가 부족합니다!");
            gold_Danger.SetActive(true);
        }
    }

    public void TimeShopDiscount()
    {
        if(timeDiscountCount <= 24 && pInfo.player_gold >= buyingtimeShopDiscountGold)
        {
            pInfo.player_gold -= buyingtimeShopDiscountGold;
            timeDiscountCount++;
            pInfo.time_shop_discount -= 0.02f;
            buyingtimeShopDiscountGold *= 10;
        }
        else if(timeDiscountCount > 25)
        {
            Debug.Log("최대 강화치 입니다!");
        }
        else if (pInfo.player_gold < buyingtimeShopDiscountGold)
        {
            Debug.Log("보유 골드가 부족합니다!");
            gold_Danger.SetActive(true);
        }
    }
}
