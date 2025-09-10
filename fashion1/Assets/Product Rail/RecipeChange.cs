using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeChange : MonoBehaviour , IPointerClickHandler
{
    PlayerInfo playerinfo;
    SystemInfo systeminfo;
    RecipeBook recipebook;
    [SerializeField]
    ClothesProductRail clothes_rail;

    [Header("현재 레시피")]
    public Image recipe_sprite;

    void Start()
    {
        playerinfo = GameObject.Find("Playerinfo").GetComponent<PlayerInfo>();
        systeminfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        recipebook = GameObject.Find("Recipe_Book").GetComponent<RecipeBook>();
    }
    void Update()
    {
        
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) //터치시
    {
        RecipeBook_Open();//RecipBook 오픈
    }

    void RecipeBook_Open()
    {
        recipebook.clothes_rail = clothes_rail;  //레시피 변경할 레일을 레시피 북에 전달
        recipebook.recipeChange = GetComponent<RecipeChange>(); //이거 전달
        recipebook.OnOff_Recipe_Book();          //레시피 북 온
    }
}
