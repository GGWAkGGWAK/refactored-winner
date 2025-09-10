using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    SystemInfo systemInfo;
    NewsDirectory newsDirectory;

    private Dictionary<int, int> daysInMonth = new Dictionary<int, int> //�޺� �ϼ�
    {
        {1, 31}, {2, 28}, {3, 31}, {4, 30},
        {5, 31}, {6, 30}, {7, 31}, {8, 31},
        {9, 30}, {10, 31}, {11, 30}, {12, 31}

    };

    float realtimer; //Ÿ�̸�
    [SerializeField]
    float day_by_realtime; //�ΰ��� �Ϸ� �ǽð�

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

        if(realtimer > day_by_realtime) // �Ϸ簡 ������
        {
            DateUpdate();
        }
    }

    int GetDaysInMonth(int month) // ���� ������ �ϼ� ��ȯ
    {
        return daysInMonth.ContainsKey(month) ? daysInMonth[month] : 0;
    }

    void DateUpdate()
    {
        int daysInCurrentMonth = GetDaysInMonth(systemInfo.game_time_month); //���� ���� ������ ��

        if (systemInfo.game_time_day < daysInCurrentMonth)//������ ���� �ƴ϶��
        {
            systemInfo.game_time_day += 1; // �Ϸ�+
        }
        else
        {
            // ������ ���� ���
            if(systemInfo.game_time_month == 12) //12��
            {
                systemInfo.game_time_year += 1; // �ϳ� +
                systemInfo.game_time_month = 1; // 1��
                systemInfo.game_time_day = 1;   // 1��
            }
            else //�ٸ� ��
            {
                systemInfo.game_time_month += 1; // �Ѵ� +
                systemInfo.game_time_day = 1; // 1�Ϸ� ����
            }
        }

        if(systemInfo.game_time_day_count != 365) //���° ������
        {
            systemInfo.game_time_day_count += 1;
        }
        else
        {
            systemInfo.game_time_day_count = 1;
        }
        realtimer = 0; //Ÿ�̸� �ʱ�ȭ
    }
}
