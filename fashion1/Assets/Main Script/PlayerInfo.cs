using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public string player_name; //�÷��̾� �̸�
    public int player_level;   //�÷��̾� ����
    public string player_id;   //�÷��̾� ���̵�

    public int player_gold;     //���� ��差
    public int player_crystal;  //���� ������ȭ��
    public int coupon_shop_change; //���� ���λ� ����
    public int coupon_fever;       //���� �ǹ� ����

    public int player_accumulate_gold; //���� �Ǹ� ���
    public int item_accumulate_selse_rate; //������ ���� �Ǹŷ�
    public int item_accumulate_output;     //������ ���� ���귮
    public int part_accumulate_output;     //��� ���� ���귮
    public int item_possession_amount;     //�� ���� ������ ����
    public int part_possession_amount;     //�� ���� ��� ����

    public float item_rail_production_speed; // ������ ���� ���� �ӵ�
    public float item_rail_output;           // ������ ���� ���귮
    public int item_rail_recipe;             // ������ ���� ������ ȹ�� Ȯ��

    public float part_rail_production_speed; // ��� ���� ����ӵ�
    public float part_rail_output;           // ��� ���� ���귮

    public float time_shop_refresh_time;    //���λ� �ð�
    public float time_shop_main_time;       //���׷��̵� �� ���λ� �ð�
    public float time_shop_discount;

    public float train_reward_multiple;     //���� ��� ȹ�� ���
    public float train_reward_time1;        //����1 �ð�
    public float train_reward_time2;        //����2 �ð�
    public float train_reward_time3;        //����3 �ð�
    public float train_main_reward_time1;   //���׷��̵� �� ����1 �ð�
    public float train_main_reward_time2;   //���׷��̵� �� ����2 �ð�
    public float train_main_reward_time3;   //���׷��̵� �� ����3 �ð�
    public bool train1Started;  // ���� 1 ��� ����
    public bool train2Started;  // ���� 2 ��� ����
    public bool train3Started;  // ���� 3 ��� ����

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player_gold = 100000;
        player_crystal = 10;

        item_rail_production_speed = 1;

        
        time_shop_main_time = 60f;
        time_shop_refresh_time = time_shop_main_time;
        time_shop_discount = 0.75f;

        train_reward_multiple = 2.0f;
        train_main_reward_time1 = 60f;
        train_main_reward_time2 = 60f;
        train_main_reward_time3 = 60f;
        train_reward_time1 = train_main_reward_time1;
        train_reward_time2 = train_main_reward_time2;
        train_reward_time3 = train_main_reward_time3;
        train1Started = false;
        train2Started = false;
        train3Started = false;


    }

}