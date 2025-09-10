using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] 
    public List<Recipe> all_recipe = new List<Recipe>();  //전체 레시피
    public List<Recipe> unlock_recipe = new List<Recipe>(); //보유한 레시피
    public List<Recipe_Slot> recipe_slots = new List<Recipe_Slot>(); //레시피 슬롯(보유한것만 표시)
    
    private Recipe_Slot new_recipe_slot;
    public ClothesProductRail clothes_rail;
    public RecipeChange recipeChange;

    bool open_UI;
    
    void Awake()
    {
        
    }

    void Start()
    {
        open_UI = false;

        Recipe_Book_Organization();
    }

    public void Recipe_Book_Organization() //********리스트 정렬 코드
    {
        all_recipe = all_recipe.OrderBy(recipe => recipe.result_item.item_code) //아이템 코드 순서로 오름차순
                      .ToList();

        unlock_recipe = unlock_recipe.OrderBy(recipe => recipe.result_item.item_code) 
                      .ToList();

        //recipe_slots = recipe_slots.OrderBy(slot => slot.recipe.result_item.item_code) 
        //              .ToList();
    }

    public void Unlock_Recipe(Recipe recipe) //신규 레시피 언락
    {
        //new_recipe_slot.Set_Recipe(recipe); //새로운 레시피 슬롯에 레시피 입력

        unlock_recipe.Add(recipe);      //언락 레시피 추가
        //recipe_slots.Add(new_recipe_slot);//레시피 슬롯 추가

        Recipe_Book_Organization(); //정렬
    }

    public void OnOff_Recipe_Book() // 껏다키기
    {
        if(open_UI)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            open_UI = false;
        }
        else if(!open_UI)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            open_UI = true;
        }
    }

    public void Set_Recipe_Change(Recipe recipe) //레시피 변경
    {
        recipeChange.recipe_sprite.sprite = recipe.result_item.item_sprite;
        clothes_rail.product_item_recipe_change = recipe;
    }
}
