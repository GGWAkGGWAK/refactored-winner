using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recipe : ScriptableObject
{
    public List<Item> ingredient = new List<Item>(); 
    public List<int> ingredient_count = new List<int>();

    public float production_time;
    public Item result_item;

    public int recipe_price;
}
