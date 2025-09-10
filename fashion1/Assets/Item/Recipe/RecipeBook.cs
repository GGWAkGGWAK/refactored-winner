using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBook : MonoBehaviour
{
    [SerializeField] 
    public List<Recipe> all_recipe = new List<Recipe>();  //��ü ������
    public List<Recipe> unlock_recipe = new List<Recipe>(); //������ ������
    public List<Recipe_Slot> recipe_slots = new List<Recipe_Slot>(); //������ ����(�����Ѱ͸� ǥ��)
    
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

    public void Recipe_Book_Organization() //********����Ʈ ���� �ڵ�
    {
        all_recipe = all_recipe.OrderBy(recipe => recipe.result_item.item_code) //������ �ڵ� ������ ��������
                      .ToList();

        unlock_recipe = unlock_recipe.OrderBy(recipe => recipe.result_item.item_code) 
                      .ToList();

        //recipe_slots = recipe_slots.OrderBy(slot => slot.recipe.result_item.item_code) 
        //              .ToList();
    }

    public void Unlock_Recipe(Recipe recipe) //�ű� ������ ���
    {
        //new_recipe_slot.Set_Recipe(recipe); //���ο� ������ ���Կ� ������ �Է�

        unlock_recipe.Add(recipe);      //��� ������ �߰�
        //recipe_slots.Add(new_recipe_slot);//������ ���� �߰�

        Recipe_Book_Organization(); //����
    }

    public void OnOff_Recipe_Book() // ����Ű��
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

    public void Set_Recipe_Change(Recipe recipe) //������ ����
    {
        recipeChange.recipe_sprite.sprite = recipe.result_item.item_sprite;
        clothes_rail.product_item_recipe_change = recipe;
    }
}
