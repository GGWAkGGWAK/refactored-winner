using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfo : MonoBehaviour
{
    public float game_time_minute; //인게임 시간(분)
    public int game_time_hour;     //인게임 시간(시간)
    public int game_time_day;      //인게임 시간(날짜)
    public int game_time_month;    //인게임 시간(달)
    public int game_time_year;     //인게임 시간(년)

    public int game_time_day_count; //인게임 일수(1-365)

    public int game_season;        //계절

    public float game_realtime;    //현실 타이머

    public bool game_on;           //게임 접속 상태

    public bool game_time_stop;    //게임 정지 상태

    public int game_sound_full;    //전체 사운드 크기
    public int game_sound_effect;  //효과음 크기
    public int game_sound_background; //배경음악 크기
    public bool game_sound_mute;   //음소거 상태

    public bool ad_runing;         //광고 진행중
    public int ad_today_views;     //하루 광고 시청 횟수
    public int ad_total_views;     //누적 광고 시청 횟수
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        game_time_year = 2024;
        game_time_month = 5;
        game_time_day = 24;
        game_time_day_count = 124;
    }
}
