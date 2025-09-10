using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public GameObject pInfoObject;
    PlayerInfo pInfo;
    NewsDirectory newsDirectory;

    public GameObject newsExclamationMark; //트레인
    public GameObject trainExclamationMark; //뉴스
    public GameObject timeExclamationMark; //보부상 느낌표

    public GameObject specialstoreObject;
    private TimeShop timeshop;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI crystalText;

    public GameObject goldDanger;
    public GameObject itemDanger;
    public GameObject storageDanger;
    public GameObject goldDangerRail1;
    public GameObject goldDangerRail2;
    public GameObject goldDangerRail3;
    void Start()
    {
        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();
        specialstoreObject = GameObject.Find("SPECIAL_STORE").gameObject;
        timeshop = specialstoreObject.GetComponent<TimeShop>();
        newsDirectory = GameObject.Find("NewsDirectory").GetComponent<NewsDirectory>();

        timeExclamationMark = GameObject.Find("Special_Store").transform.GetChild(0).gameObject;
        trainExclamationMark = GameObject.Find("Train").transform.GetChild(0).gameObject;
        newsExclamationMark = GameObject.Find("News").transform.GetChild(0).gameObject;

        timeshop.Reroll();
    }

    void Update()
    {
        Timer_shop_Train();

        Button_RedDot();

        Danger();

        if (goldText != null)
            goldText.text = pInfo.player_gold.ToString();
        else
            Debug.LogError("goldText 컴포넌트가 할당되지 않았습니다!");
        if (crystalText != null)
            crystalText.text = pInfo.player_crystal.ToString();
        else
            Debug.LogError("crystalText 컴포넌트가 할당되지 않았습니다!");
    }

    void Timer_shop_Train()
    {
        // time_shop_refresh_time 감소 로직
        if (pInfo.time_shop_refresh_time > 0)
        {
            pInfo.time_shop_refresh_time -= Time.deltaTime;
        }
        else
        {
            timeExclamationMark.SetActive(true);
            timeshop.Reroll();
            pInfo.time_shop_refresh_time = pInfo.time_shop_main_time;
        }

        // train_reward_time 감소 로직
        if (pInfo.train1Started && pInfo.train_reward_time1 > 0)
        {
            pInfo.train_reward_time1 -= Time.deltaTime;
        }

        if (pInfo.train2Started && pInfo.train_reward_time2 > 0)
        {
            pInfo.train_reward_time2 -= Time.deltaTime;
        }
        if (pInfo.train3Started && pInfo.train_reward_time3 > 0)
        {
            pInfo.train_reward_time3 -= Time.deltaTime;
        }
    }

    public void Button_RedDot()
    {
        // 트레인
        if (pInfo.train_reward_time1 <= 0 || pInfo.train_reward_time2 <= 0 || pInfo.train_reward_time3 <= 0)
        {
            trainExclamationMark.SetActive(true);
        }

        if (pInfo.train_reward_time1 >= 0 && pInfo.train_reward_time2 >= 0 && pInfo.train_reward_time3 >= 0)
        {
            trainExclamationMark.SetActive(false);
        }

        //뉴스
        newsExclamationMark.SetActive(newsDirectory.new_News);

    }

    public void TimeExMark()
    {
        if (timeExclamationMark.activeSelf)
        {
            timeExclamationMark.SetActive(false);
        }
        else
        {
            return;
        }
    }

    public void Danger()
    {
        if (goldDanger.activeSelf)
        {
            StartCoroutine(DangerExit(goldDanger));
        }
        if (storageDanger.activeSelf)
        {
            StartCoroutine(DangerExit(storageDanger));
        }
        if (itemDanger.activeSelf)
        {
            StartCoroutine(DangerExit(itemDanger));
        }
        if (goldDangerRail1.activeSelf)
        {
            StartCoroutine(DangerExit(goldDangerRail1));
        }
        if (goldDangerRail2.activeSelf)
        {
            StartCoroutine(DangerExit(goldDangerRail2));
        }
        if (goldDangerRail3.activeSelf)
        {
            StartCoroutine(DangerExit(goldDangerRail3));
        }
    }
    IEnumerator DangerExit(GameObject danger)
    {
        yield return new WaitForSeconds(1f);
        danger.SetActive(false);
    }
}
