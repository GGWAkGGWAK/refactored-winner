using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    PlayerInfo playerinfo;
    SystemInfo systeminfo;
    QuestManager questManager;

    public Button sTorage_cancle;

    public List<Slot> slots = new List<Slot>();                 //����
    public List<Vector2> slots_position = new List<Vector2>();  //���� ��ġ

    [Space (10f)]
    public int item_slot_count = 30;
    public int item_slot_count_max = 500;

    public int first_cloth;
    private void Start()
    {
        playerinfo = GameObject.Find("Playerinfo").GetComponent<PlayerInfo>();
        systeminfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
    }
    void Update()
    {
        if (slots.Count > 0 && slots[0] != null)
        {
            first_cloth = slots[0].item_count;
        }
    }
    public void Storage_Slots_Add(int count) //���� �߰�
    {
        for(int i = 0; i < count; i++)
        {
            slots.Add(null);
        }
    }
    public bool Storage_Finding(Recipe recipe) //********���� ���� ���� �Ǵ�(�����Ƿ�)
    {
        List<bool> check_item = new List<bool>();
        
        for(int i = 0; i < recipe.ingredient.Count; i++)
        {
            int search_item_code = recipe.ingredient[i].item_code;       //ã�ƾ��ϴ� ���

            var indexes = Enumerable.Range(0, slots.Count)
                          .Where(i => slots[i].item != null && slots[i].item.item_code == search_item_code)
                          .ToList(); //��ᰡ ��ġ�� �ε���

            int sum_Of_item_count = 0;
            for (int j = 0; j < indexes.Count; j++) //������ ������ ��
            {
                sum_Of_item_count = sum_Of_item_count + slots[indexes[j]].item_count;
            }
            if(recipe.ingredient_count[i] <= sum_Of_item_count) //������ �ִ¾��� ���ų� �� ������
            {
                check_item.Add(true); 
            }
            else //������ false
            {
                check_item.Add(false);
            }
        }

        return check_item.All(availability => availability); //��� ture�� ture�� ��ȯ �ƴϸ� false ��ȯ
    }

    public bool Storage_Finding(Item item, int count) //********���� ���� ���� �Ǵ�(���� �Է�)
    {
        List<bool> check_item = new List<bool>();

        int search_item_code = item.item_code;       //ã�ƾ��ϴ� ���

        var indexes = Enumerable.Range(0, slots.Count)
                      .Where(i => slots[i].item != null && slots[i].item.item_code == search_item_code)
                      .ToList(); //��ᰡ ��ġ�� �ε���

        int sum_Of_item_count = 0;

        for (int j = 0; j < indexes.Count; j++) //������ ������ ��
        {
            sum_Of_item_count = sum_Of_item_count + slots[indexes[j]].item_count;
        }

        if (count <= sum_Of_item_count) //������ �ִ¾��� ���ų� �� ������
        {
            check_item.Add(true);
        }
        else //������ false
        {
            check_item.Add(false);
        }

        return check_item.All(availability => availability); //��� ture�� ture�� ��ȯ �ƴϸ� false ��ȯ
    }

    public bool Storage_Space_Finding(Item item, int count) //********â�� �ڸ� Ž��
    {
        var null_indexes = Enumerable.Range(0, slots.Count)             //��ĭ ã��
                           .Where(slot => slots[slot].item == null)
                           .ToList();

        int null_space = null_indexes.Count * slots[0].item_max_count;  //�󽽷Կ��� ��밡���� ����

        var item_indexes = Enumerable.Range(0, slots.Count)             //�ߺ� ������ĭ ã��
                           .Where(slot => slots[slot].item != null && slots[slot].item == item)
                           .ToList();

        for(int i = 0; i < item_indexes.Count; i ++)  //�ߺ� ������ĭ���� �� ���� ã��
        {
            if(slots[item_indexes[i]].item_count != slots[item_indexes[i]].item_max_count)
            {
                null_space = null_space + slots[item_indexes[i]].item_max_count - slots[item_indexes[i]].item_count; 
            }       //Ȱ�� ������ ���� ���ϱ�
        }

        if(null_space >= count) { return true; } //�ʿ��� ��� Ȱ�� ������ ���� ��
        else { return false; }

    }
    public void Storage_Organization() //********â�� ���� �ڵ�
    {
       slots = slots.OrderBy(slot => slot.item == null ? int.MaxValue :slot.item.item_code) //���� �ڵ������ ��������
                    .ThenByDescending(slot =>slot.item == null ? int.MaxValue: slot.item_count) //���� �������� ���� ���� ��������
                    .ToList(); //�����
    }
    public void Storage_Add(Item item, int count) // â�� ������ �߰�
    {
        var item_indexes = Enumerable.Range(0, slots.Count) // �ߺ��� ������ ã��
            .Where(slot => slots[slot].item == item && slots[slot].item_count < slots[slot].item_max_count)
            .ToList();

        foreach (var index in item_indexes)
        {
            int availableSpace = slots[index].item_max_count - slots[index].item_count;
            int toAdd = Mathf.Min(count, availableSpace);
            slots[index].item_count += toAdd;
            count -= toAdd;
            if (count == 0) return; // ��� �������� �߰��ϸ� ����
        }

        var null_space = Enumerable.Range(0, slots.Count) // �� ���� ã��
            .Where(slot => slots[slot].item == null)
            .ToList();

        foreach (var index in null_space)
        {
            if (count <= slots[index].item_max_count)
            {
                slots[index].item = item;
                slots[index].item_count = count;
                return; // ��� �������� �߰��ϸ� ����
            }
            else
            {
                slots[index].item = item;
                slots[index].item_count = slots[index].item_max_count;
                count -= slots[index].item_max_count;
            }
        }

        if (count > 0)
        {
            Debug.Log("â�� �ڸ��� ������� �ʽ��ϴ�!");
        }
    }

    public void Storage_Remove(Item item, int count)//*********â�� ������ ����  �ѤѤѤѤ� �����ʿ�
    {
        int item_max_count = slots[0].item_max_count;
        var item_indexes = Enumerable.Range(0, slots.Count)     //�ߺ��� ������ ã��
                           .Where(slot => slots[slot].item == item)
                           .ToList();

        while(count > 0 && item_indexes.Count > 0)
        {
            int i = item_indexes.Count - 1;
            if (slots[item_indexes[i]].item_count >= count)
            {
                slots[item_indexes[i]].item_count -= count; // �ʿ��� �ټ���ŭ ����
                count = 0; // ���� ���� 0
            }
            else if (slots[item_indexes[i]].item_count < count)
            {
                count -= slots[item_indexes[i]].item_count; //�پ���
                slots[item_indexes[i]].item_count = 0; //ĭ�� ��ĭ ����
                item_indexes.RemoveAt(i);
            }
            i--; // �б�

            if(count == 0) // �ٻ���ϸ� Ż��
            {
                break;
            }
        }
    }
    public void Storage_Remove(Item item, int count, bool sell)
    {
        int originalCount = count; // ���� ī��Ʈ�� �����մϴ�.
        int item_max_count = slots[0].item_max_count;
        var item_indexes = Enumerable.Range(0, slots.Count)     //�ߺ��� ������ ã��
                           .Where(slot => slots[slot].item == item)
                           .ToList();

        while (count > 0 && item_indexes.Count > 0)
        {
            int i = item_indexes.Count - 1;
            if (slots[item_indexes[i]].item_count >= count)
            {
                slots[item_indexes[i]].item_count -= count; // �ʿ��� �ټ���ŭ ����
                count = 0; // ���� ���� 0
            }
            else if (slots[item_indexes[i]].item_count < count)
            {
                count -= slots[item_indexes[i]].item_count; //�پ���
                slots[item_indexes[i]].item_count = 0; //ĭ�� ��ĭ ����
                item_indexes.RemoveAt(i);
            }
        }

        if (sell)
        {
            playerinfo.player_gold += (item.item_price * originalCount); // ���� ī��Ʈ�� ����Ͽ� ��� �߰�
            questManager.gold_targetAmount += (item.item_price * originalCount);
        }
    }

}
