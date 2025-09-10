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

    public TextMeshProUGUI textValue; // TextMeshProUGUI�� ����
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
        npcScript = FindObjectOfType<NPC>();  // NPC ��ũ��Ʈ ����
        // �����̴��� �ּҰ��� �ִ밪�� ����
        slider.minValue = 0;
        slider.maxValue = 500;

        // �����̴��� �⺻���� 0���� ����
        slider.value = 0;

        // �����̴� ���� ����� �� ȣ��� �޼��� ���
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        // �ؽ�Ʈ ������Ʈ
        UpdateTextValue(slider.value);

    }
    void Update()
    {
        goldText.text = (Mathf.RoundToInt(slider.value) * ingredient.item_price).ToString();
    }
    public void OnSliderValueChanged()
    {
        // �ؽ�Ʈ ������Ʈ
        UpdateTextValue(slider.value);
    }

    // �ؽ�Ʈ ������Ʈ �޼���
    void UpdateTextValue(float value)
    {
        // �����̴� ���� ������ ��ȯ
        int intValue = Mathf.RoundToInt(value);

        // �ؽ�Ʈ ������Ʈ
        textValue.text = intValue.ToString() + "��";
    }

    public void Buy()
    {
        // �����̴� ������ �� ������ �� ��� ���
        int count = Mathf.RoundToInt(slider.value);
        int goldRequired = count * buyGold;

        // ���� ó��
        if (pInfo.player_gold >= goldRequired)
        {
            if (storage.Storage_Space_Finding(ingredient, count))
            {
                pInfo.player_gold -= goldRequired;
                storage.Storage_Add(ingredient, count);
                total_count += count;
                total_Gold_Count += goldRequired;
                UpdateCountText();
                Debug.Log("���� ����!");

                if (npcScript != null)
                {
                    npcScript.OnSuccessfulPurchase();  // NPC ��ũ��Ʈ�� OnSuccessfulPurchase �޼��� ȣ��
                }
                //gameObject.SetActive(false);
                //gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                storage_Danger.SetActive(true);
                Debug.Log("â�� �ڸ��� �����ϴ�!");
            }
        }
        else
        {
            gold_Danger.SetActive(true);
            Debug.Log("���� ��尡 �����մϴ�!");
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
            counterText.text = total_count + "��";
        }
        else
        {
            Debug.LogError("counterText�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }

}
