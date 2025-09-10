using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Upgrade_Part1 : MonoBehaviour
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

    private float firstRailSpeed;
    private int speedUpgradeCount = 0;
    private int currentIndex = 0;

    public GameObject gold_Danger;

    void Start()
    {
        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();
        buyingGold = 100;
        cpRail = rail.GetComponent<ClothesProductRail>();
        firstRailSpeed = cpRail.rail_product_speed;
    }
    void Update()
    {
        buyingGoldText.text = buyingGold.ToString();
        currentGoldText.text = pInfo.player_gold.ToString();
    }

    // ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void ChangeImage()  //���׷��̵� ����
    {
        

        if(pInfo.player_gold >= buyingGold)
        {
            pInfo.player_gold -= buyingGold;
            RailSpeedUpgrade();//���ǵ� ���׷��̵�
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
    public void RailSpeedUpgrade()//���ǵ� ���׷��̵�
    {
        speedUpgradeCount++;
        cpRail.rail_product_speed = firstRailSpeed * (1.0f + 1.0f * speedUpgradeCount);
        //cpRail.rail_product_speed *= 1.5f;
    }
}
