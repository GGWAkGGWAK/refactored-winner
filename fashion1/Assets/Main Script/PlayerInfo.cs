using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public string player_name; //플레이어 이름
    public int player_level;   //플레이어 레벨
    public string player_id;   //플레이어 아이디

    public int player_gold;     //보유 골드량
    public int player_crystal;  //보유 유료재화량
    public int coupon_shop_change; //보유 보부상 쿠폰
    public int coupon_fever;       //보유 피버 쿠폰

    public int player_accumulate_gold; //누적 판매 골드
    public int item_accumulate_selse_rate; //아이템 누적 판매량
    public int item_accumulate_output;     //아이템 누적 생산량
    public int part_accumulate_output;     //재료 누적 생산량
    public int item_possession_amount;     //총 보유 아이템 개수
    public int part_possession_amount;     //총 보유 재료 개수

    public float item_rail_production_speed; // 아이템 레일 생산 속도
    public float item_rail_output;           // 아이템 레일 생산량
    public int item_rail_recipe;             // 아이템 레일 레시피 획득 확률

    public float part_rail_production_speed; // 재료 레일 생산속도
    public float part_rail_output;           // 재료 레일 생산량

    public float time_shop_refresh_time;    //보부상 시간
    public float time_shop_main_time;       //업그레이드 할 보부상 시간
    public float time_shop_discount;

    public float train_reward_multiple;     //기차 골드 획득 배수
    public float train_reward_time1;        //기차1 시간
    public float train_reward_time2;        //기차2 시간
    public float train_reward_time3;        //기차3 시간
    public float train_main_reward_time1;   //업그레이드 할 기차1 시간
    public float train_main_reward_time2;   //업그레이드 할 기차2 시간
    public float train_main_reward_time3;   //업그레이드 할 기차3 시간
    public bool train1Started;  // 기차 1 출발 여부
    public bool train2Started;  // 기차 2 출발 여부
    public bool train3Started;  // 기차 3 출발 여부

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player_gold = 100000;
        player_crystal = 10;

        item_rail_production_speed = 1;

        
        time_shop_main_time = 60f;
        time_shop_refresh_time = time_shop_main_time;
        time_shop_discount = 0.75f;

        train_reward_multiple = 2.0f;
        train_main_reward_time1 = 60f;
        train_main_reward_time2 = 60f;
        train_main_reward_time3 = 60f;
        train_reward_time1 = train_main_reward_time1;
        train_reward_time2 = train_main_reward_time2;
        train_reward_time3 = train_main_reward_time3;
        train1Started = false;
        train2Started = false;
        train3Started = false;


    }

}