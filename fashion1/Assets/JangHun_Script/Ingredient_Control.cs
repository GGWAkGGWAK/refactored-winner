using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ingredient_Control : MonoBehaviour
{
    public Image sourceImage; // a �̹����� �Ҵ�� ������Ʈ�� �̹���
    public Image targetImage; // ����� �̹����� �Ҵ�� ������Ʈ�� �̹���
    public TextMeshProUGUI sourceText; // a �ؽ�Ʈ�� �Ҵ�� ������Ʈ�� �ؽ�Ʈ
    public TextMeshProUGUI targetText; // ����� �ؽ�Ʈ�� �Ҵ�� ������Ʈ�� �ؽ�Ʈ
    public BuyScript buyScript; // BuyScript �ν��Ͻ�
    public Item newRecipe; // ���ο� ������ ������

    public void OnButtonClick()
    {
        // �̹��� ����
        if (targetImage != null && sourceImage != null)
        {
            targetImage.sprite = sourceImage.sprite;
        }
        else
        {
            Debug.LogError("TargetImage �Ǵ� SourceImage�� �Ҵ���� �ʾҽ��ϴ�.");
        }
        if (targetText != null && sourceText != null)
        {
            targetText.text = sourceText.text;
        }
        else
        {
            Debug.LogError("TargetText �Ǵ� SourceText�� �Ҵ���� �ʾҽ��ϴ�.");
        }

        // BuyScript�� ���ο� ������ �Ҵ�
        if (buyScript != null)
        {
            buyScript.ingredient = newRecipe;
            buyScript.buyGold = newRecipe.item_price; // ���ο� ������ ���ݵ� ������Ʈ
            buyScript.Refresh(); // ī��Ʈ�� ��� �ʱ�ȭ
        }
        else
        {
            Debug.LogError("BuyScript�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}
