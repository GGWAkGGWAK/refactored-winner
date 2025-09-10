using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfo : MonoBehaviour
{
    public float game_time_minute; //�ΰ��� �ð�(��)
    public int game_time_hour;     //�ΰ��� �ð�(�ð�)
    public int game_time_day;      //�ΰ��� �ð�(��¥)
    public int game_time_month;    //�ΰ��� �ð�(��)
    public int game_time_year;     //�ΰ��� �ð�(��)

    public int game_time_day_count; //�ΰ��� �ϼ�(1-365)

    public int game_season;        //����

    public float game_realtime;    //���� Ÿ�̸�

    public bool game_on;           //���� ���� ����

    public bool game_time_stop;    //���� ���� ����

    public int game_sound_full;    //��ü ���� ũ��
    public int game_sound_effect;  //ȿ���� ũ��
    public int game_sound_background; //������� ũ��
    public bool game_sound_mute;   //���Ұ� ����

    public bool ad_runing;         //���� ������
    public int ad_today_views;     //�Ϸ� ���� ��û Ƚ��
    public int ad_total_views;     //���� ���� ��û Ƚ��
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        game_time_year = 2024;
        game_time_month = 5;
        game_time_day = 24;
        game_time_day_count = 124;
    }
}
