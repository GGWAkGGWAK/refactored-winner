using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ingredient_Control : MonoBehaviour
{
    public Image sourceImage; // a 이미지가 할당된 오브젝트의 이미지
    public Image targetImage; // 변경될 이미지가 할당된 오브젝트의 이미지
    public TextMeshProUGUI sourceText; // a 텍스트가 할당된 오브젝트의 텍스트
    public TextMeshProUGUI targetText; // 변경될 텍스트가 할당된 오브젝트의 텍스트
    public BuyScript buyScript; // BuyScript 인스턴스
    public Item newRecipe; // 새로운 레시피 아이템

    public void OnButtonClick()
    {
        // 이미지 변경
        if (targetImage != null && sourceImage != null)
        {
            targetImage.sprite = sourceImage.sprite;
        }
        else
        {
            Debug.LogError("TargetImage 또는 SourceImage가 할당되지 않았습니다.");
        }
        if (targetText != null && sourceText != null)
        {
            targetText.text = sourceText.text;
        }
        else
        {
            Debug.LogError("TargetText 또는 SourceText가 할당되지 않았습니다.");
        }

        // BuyScript에 새로운 레시피 할당
        if (buyScript != null)
        {
            buyScript.ingredient = newRecipe;
            buyScript.buyGold = newRecipe.item_price; // 새로운 아이템 가격도 업데이트
            buyScript.Refresh(); // 카운트와 골드 초기화
        }
        else
        {
            Debug.LogError("BuyScript가 할당되지 않았습니다.");
        }
    }
}
