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

    [Header("���� ������")]
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
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData) //��ġ��
    {
        RecipeBook_Open();//RecipBook ����
    }

    void RecipeBook_Open()
    {
        recipebook.clothes_rail = clothes_rail;  //������ ������ ������ ������ �Ͽ� ����
        recipebook.recipeChange = GetComponent<RecipeChange>(); //�̰� ����
        recipebook.OnOff_Recipe_Book();          //������ �� ��
    }
}
