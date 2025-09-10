using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBook : MonoBehaviour
{
    public List<Item> item = new List<Item>();
    public List<Item> ingredeints = new List<Item>();
    private void Start()
    {
        ItemBook_Organization();
    }

    public void ItemBook_Organization() //********아이템 코드순으로 정렬
    {
        item = item.OrderBy(items => items.item_code) //아이템 코드순으로 정렬
                     .ToList(); //덮어쓰기
    }

    public Item ItemCode(int itemCode)
    {
        for(int i = 0; i < item.Count; i++)
        {
            if(item[i].item_code == itemCode)
            {
                return item[i];
            }
        }
        return null;
    }

}

