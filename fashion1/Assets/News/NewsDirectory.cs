using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewsDirectory : MonoBehaviour
{
    SystemInfo systemInfo;
    NewsPopUp newsPopUp;

    public List<News> all_news = new List<News>();  //전체 뉴스
    public List<News> activated_news = new List<News>(); //발동한 뉴스

    Dictionary<News, int> news_Activation_Day = new Dictionary<News, int>();

    private System.Random random = new System.Random();

    public bool new_News = false;
    bool test_news_on = false;
    private void Start()
    {
        systemInfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        newsPopUp = GameObject.Find("NewsPopUp").GetComponent<NewsPopUp>();
    }
    private void Update()
    {
        Test_News_ONOFF();
    }
    public void News_Trigger() // 뉴스발동
    {
        List<News> today_News = new List<News>();   //오늘 발동할 뉴스 후보

        foreach(var news in all_news)
        {
            if(activated_news.Contains(news)) { continue; } //발동한 뉴스는 건너띄기

            if(news.start_Min_Day <= systemInfo.game_time_day_count && 
                systemInfo.game_time_day_count <= news.start_Max_Day) //발동기간안에 있는 뉴스
            {
                if(random.NextDouble() < news.activetion_Probability) //발동 확률로 발동
                {
                    today_News.Add(news);  //오늘 발동할 뉴스
                }
            }
            else if(news.start_Max_Day == 0) //랜덤 뉴스
            {
                if (random.NextDouble() < news.activetion_Probability) //발동 확률로 발동
                {
                    today_News.Add(news);  //오늘 발동할 뉴스
                }
            }
        }

        if(today_News.Count > 0)
        {
            int index = random.Next(today_News.Count); // 리스트중에 랜덤으로 선택
            News_List_Add(today_News[index]); //뉴스 발동
        }
    }

    public void Check_News_Duration() //뉴스 진행 기간 체크
    {
        List<News> expiredNews = new List<News>(); //제거되야할 뉴스들

        foreach(var news in activated_news)
        {
            int elapsed_Period = systemInfo.game_time_day_count - news_Activation_Day[news]; //현재 날짜 - 발동한 날짜 (경과기간)
            if(elapsed_Period < 0) { elapsed_Period = elapsed_Period + 365; } //음수면 + 365
            
            if(elapsed_Period == news.duration_Days) //경과기간 == 유지기간
            {
                expiredNews.Add(news);              //제거리스트에 포함
            }
        }

        foreach(var news in expiredNews)
        {
            News_List_Remove(news);     //전부 제거
        }
    }
    public void News_List_Add(News news)
    {
        new_News = true;

        activated_news.Add(news);
        news_Activation_Day[news] = systemInfo.game_time_day_count; //발동일 기록
        News_Effect_Activated(news);
        News_PopUp_Update(news); //팝업 업데이트
    }   //뉴스 리스트에 추가

    public void News_List_Remove(News news)
    {
        activated_news.Remove(news);
        news_Activation_Day.Remove(news);
        News_Effect_Deactivate(news);
    } //뉴스 리스트에서 제거
    void News_Effect_Activated(News news)
    {
        for (int i = 0; i < news.item.Count; i++)
        {
            news.item[i].item_price = (int)news.effect_amount * news.item[i].item_price;
        }
    } //효과 적용

    void News_Effect_Deactivate(News news)
    {
        for (int i = 0; i < news.item.Count; i++)
        {
            news.item[i].item_price = (int)news.effect_amount / news.item[i].item_price; 
        }
    }   //효과 해제

    void News_PopUp_Update(News news)
    {
        newsPopUp.newsHaedLine.text = news.headLine;
        newsPopUp.newsDetails.text = news.detail;
        newsPopUp.newsPublication.text = systemInfo.game_time_year.ToString() + "년 " + systemInfo.game_time_month.ToString() + "월 " + systemInfo.game_time_day.ToString() + "일";

        newsPopUp.newsImage.sprite = news.news_Sprite;
    }

    public void News_PopUp_Open()
    {
        new_News = false;
    }
    void Test_News_ONOFF()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if(!test_news_on)
            {
                News_List_Add(all_news[0]);
                test_news_on = true;
            }
            else if(test_news_on)
            {
                News_List_Remove(all_news[0]);
                test_news_on = false;
            }
        }
    }//  뉴스 테스트 코드
}
