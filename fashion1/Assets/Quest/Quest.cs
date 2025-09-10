using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int quest_Code;      //����Ʈ �ڵ�
    public string quest_Name;   //����Ʈ ����
    public QuestType quest_Type; //����Ʈ Ÿ��

    public int quest_item_code;  //�䱸 ������
    public int quest_gold;   //�䱸 ���

    public int quest_reward; //���� ���

    public enum QuestType
    {
        ItemProduction,  //������ ����
        ItemSale,       // ������ �Ǹ�
        MoneyEarning,   // �� ����
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
