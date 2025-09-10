using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string item_K_name;
    public string item_name;
    public Sprite item_sprite;
    public GameObject item_prefab;

    public int item_price;
    public int[] item_typ;
    public Recipe item_recipe;

    public int item_code;
}
