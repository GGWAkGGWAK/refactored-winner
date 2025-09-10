using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientStore : MonoBehaviour
{
    PlayerInfo playerinfo;
    SystemInfo systeminfo;
    Storage storage;
    ItemBook itemBook;
    
    void Start()
    {
        playerinfo = GameObject.Find("Playerinfo").GetComponent<PlayerInfo>();
        systeminfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        storage = GameObject.Find("Storage").GetComponent<Storage>();
        itemBook = GameObject.Find("Item_Book").GetComponent<ItemBook>();
    }

    public void Buy_ngredient_10()
    {
        if(playerinfo.player_gold >= 10)
        {
            if(storage.Storage_Space_Finding(itemBook.ItemCode(19999), 10))
            {
                playerinfo.player_gold -= 10;
                storage.Storage_Add(itemBook.ItemCode(19999), 10);
                storage.Storage_Organization();
            }
            else
            {
                Debug.Log("창고에 공간이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void Buy_Ingredient_100()
    {
        if (playerinfo.player_gold >= 100)
        {
            if (storage.Storage_Space_Finding(itemBook.ItemCode(19999), 1000))
            {
                playerinfo.player_gold -= 100;
                storage.Storage_Add(itemBook.ItemCode(19999), 1000);
                storage.Storage_Organization();
            }
            else
            {
                Debug.Log("창고에 공간이 부족합니다.");
            }
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }
}
