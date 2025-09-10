using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSelector : MonoBehaviour
{
    public GameObject recipeDetailPopup; // ������ �� ������ ǥ���� �˾�â
    public Image recipeImage; // ������ �̹���
    public TextMeshProUGUI recipeDescription; // ������ ����
    public Transform ingredientListContent; // ��� ����� ǥ���� ������
    public GameObject ingredientItemPrefab; // ��� ������ ������

    // ������ ���� �� �� ���� ǥ��
    public void OnRecipeSelected(Recipe recipe)
    {
        recipeImage.sprite = recipe.result_item.item_sprite; // ������ ������ ������ ����
        recipeDescription.text = $"{recipe.result_item.item_K_name}\nTime: {recipe.production_time}s\nPrice: {recipe.result_item.item_price}";

        // ���� ��� ��� ����
        foreach (Transform child in ingredientListContent)
        {
            Destroy(child.gameObject);
        }

        // ��� ��� ����
        for (int i = 0; i < recipe.ingredient.Count; i++)
        {
            GameObject ingredientItem = Instantiate(ingredientItemPrefab, ingredientListContent);
            TextMeshProUGUI ingredientText = ingredientItem.GetComponentInChildren<TextMeshProUGUI>();
            if (ingredientText != null)
            {
                ingredientText.text = $"{recipe.ingredient[i].item_K_name} x{recipe.ingredient_count[i]}";
            }
        }

        recipeDetailPopup.SetActive(true); // �˾�â ǥ��
    }

    // �˾�â �ݱ�
    public void ClosePopup()
    {
        recipeDetailPopup.SetActive(false);
    }
}
