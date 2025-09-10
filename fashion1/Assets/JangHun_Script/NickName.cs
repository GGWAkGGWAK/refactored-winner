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
            // 플레이어의 닉네임을 저장
            PlayerPrefs.SetString("PlayerName", playerName);
        }
        else
        {
            Debug.Log("닉네임을 입력하세요.");
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
