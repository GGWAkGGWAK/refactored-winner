using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    PlayerInfo playerinfo;
    SystemInfo systeminfo;
    ItemBook itemBook;
    QuestUI questUI;

    List<Quest> quest = new List<Quest>();
    Quest nowQuest;

    public Sprite gold_sprite;

    public int quest_Code;

    public int gold_targetAmount;

    bool quest_Completed;


    void Start()
    {
        playerinfo = GameObject.Find("Playerinfo").GetComponent<PlayerInfo>();
        systeminfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        itemBook = GameObject.Find("Item_Book").GetComponent<ItemBook>();
        questUI = GameObject.Find("QuestUI").GetComponent<QuestUI>();

        quest_Code = 0;

        QuestListAdd();
        nowQuest = quest[0];
    }

    
    void Update()
    {
        quest_Completed = NowQuestUpdate(nowQuest);
        QuestUIUpdate(nowQuest);
    }

    bool NowQuestUpdate(Quest quest)
    {
        if(quest.quest_Type == Quest.QuestType.MoneyEarning) //µ·¹ú±â
        {
            if(quest.quest_gold <= gold_targetAmount)
            {
                gold_targetAmount = quest.quest_gold;
                return true;
            }
        }
        else if(quest.quest_Type == Quest.QuestType.ItemSale) //¾ÆÀÌÅÛ ÆÇ¸Å
        {
            return false;
        }
        else if(quest.quest_Type == Quest.QuestType.ItemProduction) //¾ÆÀÌÅÛ »ý»ê
        {
            return false;
        }
        return false;
    }

    void QuestUIUpdate(Quest quest)
    {
        if (quest.quest_Type == Quest.QuestType.MoneyEarning) //µ·¹ú±â
        {
            questUI.quest_Name.text = quest.quest_Name;
            questUI.quest_TragetAmount.text = gold_targetAmount.ToString() + "/" + quest.quest_gold.ToString();
            questUI.quest_Reward.sprite = gold_sprite;
            questUI.quest_Reward_Amount.text = quest.quest_reward.ToString();
        }
        else if (quest.quest_Type == Quest.QuestType.ItemSale) //¾ÆÀÌÅÛ ÆÇ¸Å
        {
            
        }
        else if (quest.quest_Type == Quest.QuestType.ItemProduction) //¾ÆÀÌÅÛ »ý»ê
        {
            
        }
    }

    public void QuestCompleted()
    {
        if (quest_Completed)
        {
            playerinfo.player_gold += nowQuest.quest_reward;
            gold_targetAmount = 0;
            quest_Code++;
            nowQuest = quest[quest_Code];
        }
    }
    void QuestListAdd()
    {
        quest.Add(QuestCode0);
        quest.Add(QuestCode1);
        quest.Add(QuestCode2);
        quest.Add(QuestCode3);
        quest.Add(QuestCode4);
        quest.Add(QuestCode5);
    }

    Quest QuestCode0 = new Quest("100¿ø ¹ú±â", 1, 100, 100, 0, Quest.QuestType.MoneyEarning);
    Quest QuestCode1 = new Quest("1000¿ø ¹ú±â", 1, 1000, 1000, 0, Quest.QuestType.MoneyEarning);
    Quest QuestCode2 = new Quest("10000¿ø ¹ú±â", 2, 10000, 10000, 0, Quest.QuestType.MoneyEarning);
    Quest QuestCode3 = new Quest("100000¿ø ¹ú±â", 3, 100000, 100000, 0, Quest.QuestType.MoneyEarning);
    Quest QuestCode4 = new Quest("1000000¿ø ¹ú±â", 4, 1000000, 1000000, 0, Quest.QuestType.MoneyEarning);
    Quest QuestCode5 = new Quest("10000000¿ø ¹ú±â", 5, 10000000, 10000000, 0, Quest.QuestType.MoneyEarning);
}
