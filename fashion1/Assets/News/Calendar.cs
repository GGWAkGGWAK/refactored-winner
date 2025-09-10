using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    SystemInfo systemInfo;
    NewsDirectory newsDirectory;

    private Dictionary<int, int> daysInMonth = new Dictionary<int, int> //달별 일수
    {
        {1, 31}, {2, 28}, {3, 31}, {4, 30},
        {5, 31}, {6, 30}, {7, 31}, {8, 31},
        {9, 30}, {10, 31}, {11, 30}, {12, 31}

    };

    float realtimer; //타이머
    [SerializeField]
    float day_by_realtime; //인게임 하루 실시간

    void Start()
    {
        systemInfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        newsDirectory = GameObject.Find("NewsDirectory").GetComponent<NewsDirectory>();

        realtimer = 0;
        day_by_realtime = 600;
    }
    void Update()
    {
        realtimer += Time.deltaTime;

        if(realtimer > day_by_realtime) // 하루가 지나면
        {
            DateUpdate();
        }
    }

    int GetDaysInMonth(int month) // 달을 넣으면 일수 반환
    {
        return daysInMonth.ContainsKey(month) ? daysInMonth[month] : 0;
    }

    void DateUpdate()
    {
        int daysInCurrentMonth = GetDaysInMonth(systemInfo.game_time_month); //현재 달의 마지막 날

        if (systemInfo.game_time_day < daysInCurrentMonth)//마지막 날이 아니라면
        {
            systemInfo.game_time_day += 1; // 하루+
        }
        else
        {
            // 마지막 날일 경우
            if(systemInfo.game_time_month == 12) //12월
            {
                systemInfo.game_time_year += 1; // 일년 +
                systemInfo.game_time_month = 1; // 1월
                systemInfo.game_time_day = 1;   // 1일
            }
            else //다른 달
            {
                systemInfo.game_time_month += 1; // 한달 +
                systemInfo.game_time_day = 1; // 1일로 변경
            }
        }

        if(systemInfo.game_time_day_count != 365) //몇번째 날인지
        {
            systemInfo.game_time_day_count += 1;
        }
        else
        {
            systemInfo.game_time_day_count = 1;
        }
        realtimer = 0; //타이머 초기화
    }
}
