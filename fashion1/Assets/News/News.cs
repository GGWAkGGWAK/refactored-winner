using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class News : ScriptableObject
{
    public List<Item> item = new List<Item>();

    public int news_Code;           //뉴스 코드

    [Header("해드라인/내용")]
    public string headLine;         //해드라인
    public string detail;           //내용

    [Header("시작날자 최소/최대")]
    public int start_Min_Day;           //시작 날자 (0 일경우 상관 없음)
    public int start_Max_Day;           //min ~ max 사이에 발동

    [Header("유지 기간")]
    public int duration_Days;       //유지 기간

    [Header("증감량")]
    public float effect_amount;     //증감량

    [Header("발동 확률")]
    public float activetion_Probability;      //발동 확률

    [Space(10f)]
    public Sprite news_Sprite;      //이미지
}
