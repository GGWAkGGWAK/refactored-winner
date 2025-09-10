using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class QuestUI : MonoBehaviour, IPointerClickHandler
{
    QuestManager questManager;

    public TextMeshProUGUI quest_Name;
    public TextMeshProUGUI quest_TragetAmount;
    public Image quest_Reward;
    public TextMeshProUGUI quest_Reward_Amount;

    public void Start()
    {
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        questManager.QuestCompleted();
    }
}
