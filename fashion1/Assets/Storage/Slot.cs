using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour 
{
    public Item item;

    public int item_max_count;
    public int item_count;

    public GameObject item_image_object;
    Image item_image;

    public Sale sale;
    public TextMeshProUGUI item_count_text;

    private void Start()
    {
        item_image = item_image_object.GetComponent<Image>();
        
    }
    private void Update()
    {
        if(item_count == 0) // 아이템을 다쓰면 빈칸으로
        {
            item = null;
            item_image_object.SetActive(false);
        }
        else
        {
            item_image_object.SetActive(true);
            foreach (var obj in FindObjectsOfType<Button>())
            {
                obj.interactable = true;
            }
        }

        Slot_Info();

        
    }

    public void Slot_Info()
    {
        if (item == null) { item_image.sprite = null; item_count_text.text = " "; }
        else { item_image.sprite = item.item_sprite; item_count_text.text = item_count.ToString(); }
    }

    public void OnSlotClick() // 슬롯 클릭 이벤트 함수
    {
        if (item != null)
        {
            sale.gameObject.SetActive(true);
            sale.UpdatePopup(item, item_count); // 아이템과 수량을 SaleItem 함수로 전달
        }
    }
}
