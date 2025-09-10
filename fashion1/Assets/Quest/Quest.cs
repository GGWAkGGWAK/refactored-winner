using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int quest_Code;      //퀘스트 코드
    public string quest_Name;   //퀘스트 네임
    public QuestType quest_Type; //퀘스트 타입

    public int quest_item_code;  //요구 아이템
    public int quest_gold;   //요구 골드

    public int quest_reward; //보상 골드

    public enum QuestType
    {
        ItemProduction,  //아이템 생산
        ItemSale,       // 아이템 판매
        MoneyEarning,   // 돈 벌기
    }

    public Quest(string quest_Name, int quest_Code, int quest_gold,int quest_reward, int quest_item_code, QuestType quest_Type)
    {
        this.quest_Name = quest_Name;
        this.quest_Code = quest_Code;
        this.quest_Type = quest_Type;
        this.quest_item_code = quest_item_code;
        this.quest_gold = quest_gold;
        this.quest_reward = quest_reward;
    }
}
