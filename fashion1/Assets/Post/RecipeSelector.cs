using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSelector : MonoBehaviour
{
    public GameObject recipeDetailPopup; // 레시피 상세 정보를 표시할 팝업창
    public Image recipeImage; // 레시피 이미지
    public TextMeshProUGUI recipeDescription; // 레시피 설명
    public Transform ingredientListContent; // 재료 목록을 표시할 콘텐츠
    public GameObject ingredientItemPrefab; // 재료 아이템 프리팹

    // 레시피 선택 시 상세 정보 표시
    public void OnRecipeSelected(Recipe recipe)
    {
        recipeImage.sprite = recipe.result_item.item_sprite; // 레시피 아이템 아이콘 설정
        recipeDescription.text = $"{recipe.result_item.item_K_name}\nTime: {recipe.production_time}s\nPrice: {recipe.result_item.item_price}";

        // 기존 재료 목록 삭제
        foreach (Transform child in ingredientListContent)
        {
            Destroy(child.gameObject);
        }

        // 재료 목록 생성
        for (int i = 0; i < recipe.ingredient.Count; i++)
        {
            GameObject ingredientItem = Instantiate(ingredientItemPrefab, ingredientListContent);
            TextMeshProUGUI ingredientText = ingredientItem.GetComponentInChildren<TextMeshProUGUI>();
            if (ingredientText != null)
            {
                ingredientText.text = $"{recipe.ingredient[i].item_K_name} x{recipe.ingredient_count[i]}";
            }
        }

        recipeDetailPopup.SetActive(true); // 팝업창 표시
    }

    // 팝업창 닫기
    public void ClosePopup()
    {
        recipeDetailPopup.SetActive(false);
    }
}
