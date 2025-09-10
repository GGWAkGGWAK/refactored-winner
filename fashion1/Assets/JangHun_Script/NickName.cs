using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public string playerName;

    public void StartGame()
    {
        playerName = playerNameInput.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            // �÷��̾��� �г����� ����
            PlayerPrefs.SetString("PlayerName", playerName);
        }
        else
        {
            Debug.Log("�г����� �Է��ϼ���.");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
