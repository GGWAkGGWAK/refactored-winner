using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�
using System.Collections;

public class NPC : MonoBehaviour
{
    public TutorialManager tutorialManager;
    public PlayerInfo playerInfo;
    Ingredient_Manager ingredient_Manager;
    Storage sTorage;
    NewsManager newsManager;
    Train_Manager train_Manager;
    Special_Manager special_Manager;
    Slot slot;

    void Start()
    {
        if (playerInfo == null)
        {
            playerInfo = FindObjectOfType<PlayerInfo>();
            if (playerInfo == null) Debug.LogError("PlayerInfo component not found!");
        }
        if (tutorialManager == null)
        {
            tutorialManager = FindObjectOfType<TutorialManager>();
            if (tutorialManager == null)
            {
                Debug.LogError("TutorialManager component not found!");
                return; // tutorialManager�� null�� ��� ���� �ڵ� ������ ����
            }
        }
        ingredient_Manager = GameObject.Find("iingredient_Pop")?.GetComponent<Ingredient_Manager>();
        if (ingredient_Manager == null) Debug.LogError("Ingredient_Manager component not found on iingredient_Pop!");

        sTorage = GameObject.Find("Storage")?.GetComponent<Storage>();
        if (sTorage == null) Debug.LogError("Storage component not found on Storage!");

        newsManager = GameObject.Find("NewsPopUp")?.GetComponent<NewsManager>();
        if (newsManager == null) Debug.LogError("NewsManager component not found on NEWS_PPOP!");

        train_Manager = GameObject.Find("TTrain_PopUp")?.GetComponent<Train_Manager>();
        if (train_Manager == null) Debug.LogError("Train_Manager component not found on TTrain_PopUp!");

        special_Manager = GameObject.Find("SPECIAL_STORE")?.GetComponent<Special_Manager>();
        if (special_Manager == null) Debug.LogError("Special_Manager component not found on SPECIAL_STORE!");

        // InputField�� Text ������Ʈ �ʱ�ȭ

        SayHello();
    }

    void SayHello()
    {
        if (tutorialManager == null)
        {
            Debug.LogError("TutorialManager�� �������� �ʾҽ��ϴ�.");
            return;
        }

        tutorialManager.steps = new List<TutorialStep>
        {
            new TutorialStep
            {
                instructionText = "�ȳ��ϼ���!\n �ɽŻ翡 ���� ���� ȯ���մϴ�.",
                hidePanel = false
            },
            new TutorialStep
            {
                instructionText = "��� ��ư�� ����������!",
                highlightButton = GameObject.Find("Build")?.GetComponent<Button>(),
                hidePanel = true
            },
            new TutorialStep
            {
                instructionText = "���ϼ̾��\n ���⼭ ��Ḧ ���� �� �־��!.",
                hidePanel = false
            },
            new TutorialStep
            {
                instructionText = "10000���� �帱�״�\n õ�� �����غ��ðھ��?",
                highlightButton = ingredient_Manager?.cloth_Button,
                hidePanel = true,
                onStepComplete = AddGoldAndProceed,
            },
            new TutorialStep
            {
                onStepComplete = DisableOtherButtons,
                hidePanel = true,
            },
            new TutorialStep
            {
                highlightButton = ingredient_Manager?.sell_ingredient_Button,
                hidePanel = true,

            },
            new TutorialStep
            {
                onStepComplete = AbleButton,
                instructionText = "���ϼ̾��!\n �̷��� ����ǰ�� ����˴ϴ�.",
                hidePanel = false
            },
            new TutorialStep
            {
                instructionText = "��ٸ��ٺ���\n â�� ���� �������� �ֽ��ϴ�!.",
                highlightButton = GameObject.Find("Storge_Button")?.GetComponent<Button>(),
                hidePanel = true
            },
            new TutorialStep
            {
                highlightButton = sTorage?.sTorage_cancle,
                hidePanel = true
            },
            new TutorialStep
            {
                instructionText = "�� ��ư�� ���� �ý����ε�\n ���� ������ ������ �����ſ�!",
                hidePanel = false
            },
            new TutorialStep
            {
                highlightButton = GameObject.Find("News")?.GetComponent<Button>(),
                hidePanel = true
            },
            new TutorialStep
            {
                highlightButton = newsManager?.NewsCancle,
                hidePanel = true
            },
            new TutorialStep
            {
                instructionText = "�� ��ư�� Ư�� ����\n Ư�� �������� �����ؿ�!",
                hidePanel = false
            },
            new TutorialStep
            {
                highlightButton = GameObject.Find("Special_Store")?.GetComponent<Button>(),
                hidePanel = true
            },
            new TutorialStep
            {
                highlightButton = special_Manager?.special,
                hidePanel = true
            },
            new TutorialStep
            {
                instructionText = "���������� ���� ��������\n Ʈ������ ���� �� �־��",
                hidePanel = false
            },
            new TutorialStep
            {
                highlightButton = GameObject.Find("Train")?.GetComponent<Button>(),
                hidePanel = true
            },
            new TutorialStep
            {
                highlightButton = train_Manager?.train_Cancle,
                hidePanel = true
            },
            new TutorialStep
            {
                instructionText = "���� �����ص帱 �� �ִ°�\n �����������",
                hidePanel = false
            },
            new TutorialStep
            {
                //other
                instructionText = "������ ������ �������\n ������ �������� ��������!",
                hidePanel = false
            },
        };

        tutorialManager.StartTutorial();
    }

    void DisableOtherButtons()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (var button in allButtons)
        {
            if (button != ingredient_Manager.sell_ingredient_Button)
            {
                button.interactable = false;
            }
        }
    }
    void AbleButton()
    {
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (var button in allButtons)
        {
            if (button != ingredient_Manager.sell_ingredient_Button)
            {
                button.interactable = true;
            }
        }
    }
    public void TenOverClothCheck()
    {
        if (sTorage.first_cloth >= 10)
        {
            tutorialManager.OnButtonClick();
        }
        else
        {
            tutorialManager.tutorialCheck = false;
            Debug.Log("��ᰡ ������� �ʽ��ϴ�. �˾�â�� ���ϴ�.");
            tutorialManager.SShowManager();
        }
    }



    void AddGoldAndProceed()
    {
        playerInfo.player_gold += 10000;
        Debug.Log("�÷��̾�� 10000���� ���޵Ǿ����ϴ�.");
        tutorialManager.OnButtonClick();
    }

    public void OnSuccessfulPurchase()
    {
        tutorialManager.OnButtonClick();
        Debug.Log("���� �� ���� Ʃ�丮�� �ܰ�� �����մϴ�.");
    }
}
