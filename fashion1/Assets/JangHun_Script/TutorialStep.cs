using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialStep : MonoBehaviour
{
    public string instructionText;
    public Button highlightButton; // ���̶���Ʈ�� ��ư
    public bool hidePanel; // �г��� ������ ���� 
    public UnityAction onStepComplete; // Ʃ�丮�� ���� �Ϸ�� ����� �Լ�
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