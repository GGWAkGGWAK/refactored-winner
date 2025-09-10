using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sale : MonoBehaviour
{
    public Item saleItem;

    public TextMeshProUGUI saleNameText;
    public Image itemImage;
    public TextMeshProUGUI totalGoldText;
    public TextMeshProUGUI saleCountText;
    public int totalSaleGold;
    public GameObject item_Danger;
    public GameObject pInfoObject;
    PlayerInfo pInfo;

    [SerializeField]
    int count;

    public GameObject storageObject;
    Storage storage;


    // Start is called before the first frame update
    void Start()
    {
        storageObject = GameObject.Find("Storage");
        storage = storageObject.GetComponent<Storage>();
        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        saleCountText.text = count.ToString() + " 개";
        totalGoldText.text = (saleItem.item_price * count).ToString();
    }
    public void countPlus1()
    {
        var itemSlot = storage.slots.Find(slot => slot.item == saleItem);
        if (count > itemSlot.item_count)
        {
            count = itemSlot.item_count;
        }
        else
        {
            count++;
        }
    }
    public void countPlus10()
    {
        var itemSlot = storage.slots.Find(slot => slot.item == saleItem);
        if (count > itemSlot.item_count)
        {
            count = itemSlot.item_count;
        }
        else
        {
            count += 10;
        }
    }
    public void countAll()
    {
        if (saleItem != null)
        {
            var itemSlot = storage.slots.Find(slot => slot.item == saleItem);
            if (itemSlot != null)
            {
                count = itemSlot.item_count;
            }
        }
    }
    public void SaleItem()
    {
        if (storage.Storage_Finding(saleItem, count))
        {
            totalSaleGold = saleItem.item_price * count;
            // 스토리지에서 아이템 제거
            storage.Storage_Remove(saleItem, count, true); // 원래 count를 전달
            storage.Storage_Organization();
            gameObject.SetActive(false);
        }
        else
        {
            item_Danger.SetActive(true);
        }
    }


    public void UpdatePopup(Item item, int count)
    {
        saleItem = item;
        saleNameText.text = item.item_K_name;
        itemImage.sprite = item.item_sprite;
    }

    public void Refresh()
    {
        count = 0;
        totalSaleGold = 0;
        gameObject.SetActive(false);
    }
}
