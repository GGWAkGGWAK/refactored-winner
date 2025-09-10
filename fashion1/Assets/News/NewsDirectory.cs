using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewsDirectory : MonoBehaviour
{
    SystemInfo systemInfo;
    NewsPopUp newsPopUp;

    public List<News> all_news = new List<News>();  //��ü ����
    public List<News> activated_news = new List<News>(); //�ߵ��� ����

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
    public void News_Trigger() // �����ߵ�
    {
        List<News> today_News = new List<News>();   //���� �ߵ��� ���� �ĺ�

        foreach(var news in all_news)
        {
            if(activated_news.Contains(news)) { continue; } //�ߵ��� ������ �ǳʶ��

            if(news.start_Min_Day <= systemInfo.game_time_day_count && 
                systemInfo.game_time_day_count <= news.start_Max_Day) //�ߵ��Ⱓ�ȿ� �ִ� ����
            {
                if(random.NextDouble() < news.activetion_Probability) //�ߵ� Ȯ���� �ߵ�
                {
                    today_News.Add(news);  //���� �ߵ��� ����
                }
            }
            else if(news.start_Max_Day == 0) //���� ����
            {
                if (random.NextDouble() < news.activetion_Probability) //�ߵ� Ȯ���� �ߵ�
                {
                    today_News.Add(news);  //���� �ߵ��� ����
                }
            }
        }

        if(today_News.Count > 0)
        {
            int index = random.Next(today_News.Count); // ����Ʈ�߿� �������� ����
            News_List_Add(today_News[index]); //���� �ߵ�
        }
    }

    public void Check_News_Duration() //���� ���� �Ⱓ üũ
    {
        List<News> expiredNews = new List<News>(); //���ŵǾ��� ������

        foreach(var news in activated_news)
        {
            int elapsed_Period = systemInfo.game_time_day_count - news_Activation_Day[news]; //���� ��¥ - �ߵ��� ��¥ (����Ⱓ)
            if(elapsed_Period < 0) { elapsed_Period = elapsed_Period + 365; } //������ + 365
            
            if(elapsed_Period == news.duration_Days) //����Ⱓ == �����Ⱓ
            {
                expiredNews.Add(news);              //���Ÿ���Ʈ�� ����
            }
        }

        foreach(var news in expiredNews)
        {
            News_List_Remove(news);     //���� ����
        }
    }
    public void News_List_Add(News news)
    {
        new_News = true;

        activated_news.Add(news);
        news_Activation_Day[news] = systemInfo.game_time_day_count; //�ߵ��� ���
        News_Effect_Activated(news);
        News_PopUp_Update(news); //�˾� ������Ʈ
    }   //���� ����Ʈ�� �߰�

    public void News_List_Remove(News news)
    {
        activated_news.Remove(news);
        news_Activation_Day.Remove(news);
        News_Effect_Deactivate(news);
    } //���� ����Ʈ���� ����
    void News_Effect_Activated(News news)
    {
        for (int i = 0; i < news.item.Count; i++)
        {
            news.item[i].item_price = (int)news.effect_amount * news.item[i].item_price;
        }
    } //ȿ�� ����

    void News_Effect_Deactivate(News news)
    {
        for (int i = 0; i < news.item.Count; i++)
        {
            news.item[i].item_price = (int)news.effect_amount / news.item[i].item_price; 
        }
    }   //ȿ�� ����

    void News_PopUp_Update(News news)
    {
        newsPopUp.newsHaedLine.text = news.headLine;
        newsPopUp.newsDetails.text = news.detail;
        newsPopUp.newsPublication.text = systemInfo.game_time_year.ToString() + "�� " + systemInfo.game_time_month.ToString() + "�� " + systemInfo.game_time_day.ToString() + "��";

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
    }//  ���� �׽�Ʈ �ڵ�
}
