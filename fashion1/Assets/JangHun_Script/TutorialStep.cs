using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialStep : MonoBehaviour
{
    public string instructionText;
    public Button highlightButton; // 하이라이트할 버튼
    public bool hidePanel; // 패널을 숨길지 여부 
    public UnityAction onStepComplete; // 튜토리얼 스텝 완료시 실행될 함수
    public TutorialStep()
    {
        instructionText = " ";
        highlightButton = null;
        hidePanel = false;
        onStepComplete = null;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}