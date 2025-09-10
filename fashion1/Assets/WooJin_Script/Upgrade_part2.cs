using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Upgrade_part2 : MonoBehaviour
{
    public GameObject pInfoObject;
    PlayerInfo pInfo;
    public GameObject rail;
    ClothesProductRail cpRail;
    public Image[] imageElements; // ����� �̹����� ǥ���� �̹��� ��ҵ��� �迭
    public Sprite[] sprites; // ����� �̹������� �迭

    public int buyingGold;
    public TextMeshProUGUI buyingGoldText;
    public TextMeshProUGUI currentGoldText;

    
    private int currentIndex = 0;

    public GameObject gold_Danger;

    void Start()
    {
        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();
        buyingGold = 1000;
        cpRail = rail.GetComponent<ClothesProductRail>();
    }
    void Update()
    {
        buyingGoldText.text = buyingGold.ToString();
        currentGoldText.text = pInfo.player_gold.ToString();
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void ChangeImage()  //���׷��̵� ����
    {


        if (pInfo.player_gold >= buyingGold)
        {
            pInfo.player_gold -= buyingGold;
            cpRail.rail_product_output++;//���귮
            // ���� �̹��� ����
            imageElements[currentIndex].sprite = sprites[currentIndex];
            // ���� �̹��� �ε����� �̵�
            currentIndex++;
            buyingGold *= 10;
            // �̹��� �迭�� ��� ��ȸ�ϸ� �ʱ�ȭ
            if (currentIndex >= sprites.Length)
            {
                currentIndex = 0;
            }
        }
        else
        {
            gold_Danger.SetActive(true);
            Debug.Log("��� ����!");
        }

    }
}
