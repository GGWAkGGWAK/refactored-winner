using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NickNameDisplay : MonoBehaviour
{

    public TMP_Text playerNameText;

    void Start()
    {
        string ConvertName = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(playerNameText.text)); 
        string playerName = PlayerPrefs.GetString("PlayerName");
        playerNameText.text = playerName;
    }

    // Update is called once per frame
    void Update()
    {

    }
    // Start is called before the first frame update
}
