using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가
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
                return; // tutorialManager가 null인 경우 이후 코드 실행을 중지
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

        // InputField와 Text 컴포넌트 초기화

        SayHello();
    }

    void SayHello()
    {
        if (tutorialManager == null)
        {
            Debug.LogError("TutorialManager가 설정되지 않았습니다.");
            return;
        }

        tutorialManager.steps = new List<TutorialStep>
        {
            new TutorialStep
            {
                instructionText = "안녕하세요!\n 냥신사에 오신 것을 환영합니다.",
                hidePanel = false
            },
            new TutorialStep
            {
                instructionText = "재료 버튼을 눌러보세요!",
                highlightButton = GameObject.Find("Build")?.GetComponent<Button>(),
                hidePanel = true
            },
            new TutorialStep
            {
                instructionText = "잘하셨어요\n 여기서 재료를 만들 수 있어요!.",
                hidePanel = false
            },
            new TutorialStep
            {
                instructionText = "10000원을 드릴테니\n 천을 구매해보시겠어요?",
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
                instructionText = "잘하셨어요!\n 이렇게 생산품이 생산됩니다.",
                hidePanel = false
            },
            new TutorialStep
            {
                instructionText = "기다리다보면\n 창고에 만든 아이템이 있습니다!.",
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
                instructionText = "이 버튼은 뉴스 시스템인데\n 매일 아이템 가격이 변동돼요!",
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
                instructionText = "이 버튼은 특별 상점\n 특별 아이템이 등장해요!",
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
                instructionText = "마지막으로 만든 아이템을\n 트럭으로 보낼 수 있어요",
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
                instructionText = "제가 설명해드릴 수 있는건\n 여기까지에요",
                hidePanel = false
            },
            new TutorialStep
            {
                //other
                instructionText = "열심히 공장을 성장시켜\n 나만의 공장으로 만들어보세요!",
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
            Debug.Log("재료가 충분하지 않습니다. 팝업창을 띄웁니다.");
            tutorialManager.SShowManager();
        }
    }



    void AddGoldAndProceed()
    {
        playerInfo.player_gold += 10000;
        Debug.Log("플레이어에게 10000원이 지급되었습니다.");
        tutorialManager.OnButtonClick();
    }

    public void OnSuccessfulPurchase()
    {
        tutorialManager.OnButtonClick();
        Debug.Log("구매 후 다음 튜토리얼 단계로 진행합니다.");
    }
}
