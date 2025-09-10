using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyScript : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public int total_count = 0;

    public GameObject storageObject;
    Storage storage;

    public Item ingredient;
    public int buyGold;
    public int total_Gold_Count;

    public GameObject pInfoObject;
    PlayerInfo pInfo;

    public TextMeshProUGUI textValue; // TextMeshProUGUI로 변경
    public Slider slider;
    public TextMeshProUGUI goldText;

    public GameObject gold_Danger;
    public GameObject storage_Danger;

    public NPC npcScript;

    void Start()
    {
        storageObject = GameObject.Find("Storage");
        storage = storageObject.GetComponent<Storage>();
        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();


        UpdateCountText();
        npcScript = FindObjectOfType<NPC>();  // NPC 스크립트 참조
        // 슬라이더의 최소값과 최대값을 설정
        slider.minValue = 0;
        slider.maxValue = 500;

        // 슬라이더의 기본값을 0으로 설정
        slider.value = 0;

        // 슬라이더 값이 변경될 때 호출될 메서드 등록
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        // 텍스트 업데이트
        UpdateTextValue(slider.value);

    }
    void Update()
    {
        goldText.text = (Mathf.RoundToInt(slider.value) * ingredient.item_price).ToString();
    }
    public void OnSliderValueChanged()
    {
        // 텍스트 업데이트
        UpdateTextValue(slider.value);
    }

    // 텍스트 업데이트 메서드
    void UpdateTextValue(float value)
    {
        // 슬라이더 값을 정수로 변환
        int intValue = Mathf.RoundToInt(value);

        // 텍스트 업데이트
        textValue.text = intValue.ToString() + "개";
    }

    public void Buy()
    {
        // 슬라이더 값으로 총 수량과 총 골드 계산
        int count = Mathf.RoundToInt(slider.value);
        int goldRequired = count * buyGold;

        // 구매 처리
        if (pInfo.player_gold >= goldRequired)
        {
            if (storage.Storage_Space_Finding(ingredient, count))
            {
                pInfo.player_gold -= goldRequired;
                storage.Storage_Add(ingredient, count);
                total_count += count;
                total_Gold_Count += goldRequired;
                UpdateCountText();
                Debug.Log("구매 성공!");

                if (npcScript != null)
                {
                    npcScript.OnSuccessfulPurchase();  // NPC 스크립트의 OnSuccessfulPurchase 메서드 호출
                }
                //gameObject.SetActive(false);
                //gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                storage_Danger.SetActive(true);
                Debug.Log("창고에 자리가 없습니다!");
            }
        }
        else
        {
            gold_Danger.SetActive(true);
            Debug.Log("보유 골드가 부족합니다!");
        }
    }

    public void Refresh()
    {
        total_count = 0;
        total_Gold_Count = 0;
        slider.value = 0;
        UpdateCountText();
        
    }

    private void UpdateCountText()
    {
        if (counterText != null)
        {
            counterText.text = total_count + "개";
        }
        else
        {
            Debug.LogError("counterText가 할당되지 않았습니다.");
        }
    }

}
