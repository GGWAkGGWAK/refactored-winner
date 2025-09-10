using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class News : ScriptableObject
{
    public List<Item> item = new List<Item>();

    public int news_Code;           //���� �ڵ�

    [Header("�ص����/����")]
    public string headLine;         //�ص����
    public string detail;           //����

    [Header("���۳��� �ּ�/�ִ�")]
    public int start_Min_Day;           //���� ���� (0 �ϰ�� ��� ����)
    public int start_Max_Day;           //min ~ max ���̿� �ߵ�

    [Header("���� �Ⱓ")]
    public int duration_Days;       //���� �Ⱓ

    [Header("������")]
    public float effect_amount;     //������

    [Header("�ߵ� Ȯ��")]
    public float activetion_Probability;      //�ߵ� Ȯ��

    [Space(10f)]
    public Sprite news_Sprite;      //�̹���
}
