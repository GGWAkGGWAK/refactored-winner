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
    public Image[] imageElements; // 변경될 이미지를 표시할 이미지 요소들의 배열
    public Sprite[] sprites; // 변경될 이미지들의 배열

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

    // 버튼 클릭 시 호출되는 함수
    public void ChangeImage()  //업그레이드 구매
    {
        

        if(pInfo.player_gold >= buyingGold)
        {
            pInfo.player_gold -= buyingGold;
            RailSpeedUpgrade();//스피드 업그레이드
            // 현재 이미지 변경
            imageElements[currentIndex].sprite = sprites[currentIndex];
            // 다음 이미지 인덱스로 이동
            currentIndex++;
            buyingGold *= 10;
            // 이미지 배열을 모두 순회하면 초기화
            if (currentIndex >= sprites.Length)
            {
                currentIndex = 0;
            }
        }
        else
        {
            gold_Danger.SetActive(true);
            Debug.Log("골드 부족!");
        }
        
    }
    public void RailSpeedUpgrade()//스피드 업그레이드
    {
        speedUpgradeCount++;
        cpRail.rail_product_speed = firstRailSpeed * (1.0f + 1.0f * speedUpgradeCount);
        //cpRail.rail_product_speed *= 1.5f;
    }
}
