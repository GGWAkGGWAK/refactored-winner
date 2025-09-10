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

    public List<Slot> slots = new List<Slot>();                 //슬롯
    public List<Vector2> slots_position = new List<Vector2>();  //슬롯 위치

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
    public void Storage_Slots_Add(int count) //슬롯 추가
    {
        for(int i = 0; i < count; i++)
        {
            slots.Add(null);
        }
    }
    public bool Storage_Finding(Recipe recipe) //********제작 가능 여부 판단(레시피로)
    {
        List<bool> check_item = new List<bool>();
        
        for(int i = 0; i < recipe.ingredient.Count; i++)
        {
            int search_item_code = recipe.ingredient[i].item_code;       //찾아야하는 재료

            var indexes = Enumerable.Range(0, slots.Count)
                          .Where(i => slots[i].item != null && slots[i].item.item_code == search_item_code)
                          .ToList(); //재료가 위치한 인덱스

            int sum_Of_item_count = 0;
            for (int j = 0; j < indexes.Count; j++) //보유한 재료양의 합
            {
                sum_Of_item_count = sum_Of_item_count + slots[indexes[j]].item_count;
            }
            if(recipe.ingredient_count[i] <= sum_Of_item_count) //가지고 있는양이 같거나 더 많으면
            {
                check_item.Add(true); 
            }
            else //없으면 false
            {
                check_item.Add(false);
            }
        }

        return check_item.All(availability => availability); //모두 ture면 ture를 반환 아니면 false 반환
    }

    public bool Storage_Finding(Item item, int count) //********제작 가능 여부 판단(직접 입력)
    {
        List<bool> check_item = new List<bool>();

        int search_item_code = item.item_code;       //찾아야하는 재료

        var indexes = Enumerable.Range(0, slots.Count)
                      .Where(i => slots[i].item != null && slots[i].item.item_code == search_item_code)
                      .ToList(); //재료가 위치한 인덱스

        int sum_Of_item_count = 0;

        for (int j = 0; j < indexes.Count; j++) //보유한 재료양의 합
        {
            sum_Of_item_count = sum_Of_item_count + slots[indexes[j]].item_count;
        }

        if (count <= sum_Of_item_count) //가지고 있는양이 같거나 더 많으면
        {
            check_item.Add(true);
        }
        else //없으면 false
        {
            check_item.Add(false);
        }

        return check_item.All(availability => availability); //모두 ture면 ture를 반환 아니면 false 반환
    }

    public bool Storage_Space_Finding(Item item, int count) //********창고 자리 탐색
    {
        var null_indexes = Enumerable.Range(0, slots.Count)             //빈칸 찾기
                           .Where(slot => slots[slot].item == null)
                           .ToList();

        int null_space = null_indexes.Count * slots[0].item_max_count;  //빈슬롯에서 사용가능한 공간

        var item_indexes = Enumerable.Range(0, slots.Count)             //중복 아이템칸 찾기
                           .Where(slot => slots[slot].item != null && slots[slot].item == item)
                           .ToList();

        for(int i = 0; i < item_indexes.Count; i ++)  //중복 아이템칸에서 빈 공간 찾기
        {
            if(slots[item_indexes[i]].item_count != slots[item_indexes[i]].item_max_count)
            {
                null_space = null_space + slots[item_indexes[i]].item_max_count - slots[item_indexes[i]].item_count; 
            }       //활용 가능한 공간 더하기
        }

        if(null_space >= count) { return true; } //필요한 양과 활용 가능한 공간 비교
        else { return false; }

    }
    public void Storage_Organization() //********창고 정렬 코드
    {
       slots = slots.OrderBy(slot => slot.item == null ? int.MaxValue :slot.item.item_code) //먼저 코드순으로 오름차순
                    .ThenByDescending(slot =>slot.item == null ? int.MaxValue: slot.item_count) //같은 아이템은 갯수 별로 내림차순
                    .ToList(); //덮어쓰기
    }
    public void Storage_Add(Item item, int count) // 창고에 아이템 추가
    {
        var item_indexes = Enumerable.Range(0, slots.Count) // 중복된 아이템 찾기
            .Where(slot => slots[slot].item == item && slots[slot].item_count < slots[slot].item_max_count)
            .ToList();

        foreach (var index in item_indexes)
        {
            int availableSpace = slots[index].item_max_count - slots[index].item_count;
            int toAdd = Mathf.Min(count, availableSpace);
            slots[index].item_count += toAdd;
            count -= toAdd;
            if (count == 0) return; // 모든 아이템을 추가하면 종료
        }

        var null_space = Enumerable.Range(0, slots.Count) // 빈 슬롯 찾기
            .Where(slot => slots[slot].item == null)
            .ToList();

        foreach (var index in null_space)
        {
            if (count <= slots[index].item_max_count)
            {
                slots[index].item = item;
                slots[index].item_count = count;
                return; // 모든 아이템을 추가하면 종료
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
            Debug.Log("창고에 자리가 충분하지 않습니다!");
        }
    }

    public void Storage_Remove(Item item, int count)//*********창고에 아이템 제거  ㅡㅡㅡㅡㅡ 수정필요
    {
        int item_max_count = slots[0].item_max_count;
        var item_indexes = Enumerable.Range(0, slots.Count)     //중복된 아이템 찾기
                           .Where(slot => slots[slot].item == item)
                           .ToList();

        while(count > 0 && item_indexes.Count > 0)
        {
            int i = item_indexes.Count - 1;
            if (slots[item_indexes[i]].item_count >= count)
            {
                slots[item_indexes[i]].item_count -= count; // 필요한 겟수만큼 빼기
                count = 0; // 남은 개수 0
            }
            else if (slots[item_indexes[i]].item_count < count)
            {
                count -= slots[item_indexes[i]].item_count; //다쓰기
                slots[item_indexes[i]].item_count = 0; //칸을 한칸 비우기
                item_indexes.RemoveAt(i);
            }
            i--; // 밀기

            if(count == 0) // 다사용하면 탈출
            {
                break;
            }
        }
    }
    public void Storage_Remove(Item item, int count, bool sell)
    {
        int originalCount = count; // 원래 카운트를 저장합니다.
        int item_max_count = slots[0].item_max_count;
        var item_indexes = Enumerable.Range(0, slots.Count)     //중복된 아이템 찾기
                           .Where(slot => slots[slot].item == item)
                           .ToList();

        while (count > 0 && item_indexes.Count > 0)
        {
            int i = item_indexes.Count - 1;
            if (slots[item_indexes[i]].item_count >= count)
            {
                slots[item_indexes[i]].item_count -= count; // 필요한 겟수만큼 빼기
                count = 0; // 남은 개수 0
            }
            else if (slots[item_indexes[i]].item_count < count)
            {
                count -= slots[item_indexes[i]].item_count; //다쓰기
                slots[item_indexes[i]].item_count = 0; //칸을 한칸 비우기
                item_indexes.RemoveAt(i);
            }
        }

        if (sell)
        {
            playerinfo.player_gold += (item.item_price * originalCount); // 원래 카운트를 사용하여 골드 추가
            questManager.gold_targetAmount += (item.item_price * originalCount);
        }
    }

}
