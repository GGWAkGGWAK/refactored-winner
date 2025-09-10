using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Recipe_Slot : MonoBehaviour, IPointerClickHandler
{
    RecipeBook recipeBook;

    public Recipe recipe;
    public Image recipe_image;
    void Start()
    {
        recipeBook = GameObject.Find("Recipe_Book").GetComponent<RecipeBook>();
        recipe_image.sprite = recipe.result_item.item_sprite;
    }
    public void Set_Recipe(Recipe recipe)
    {
        this.recipe = recipe;
        recipe_image.sprite = recipe.result_item.item_sprite;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        recipeBook.Set_Recipe_Change(recipe); //변경할 레시피 선택
        recipeBook.OnOff_Recipe_Book();       //레시피창 닫기
    }
}
