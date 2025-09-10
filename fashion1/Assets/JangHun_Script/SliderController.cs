using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI textValue; // TextMeshProUGUI�� ����
    public Slider slider;

    private void Start()
    {
        // �����̴��� �ּҰ��� �ִ밪�� ����
        slider.minValue = 0;
        slider.maxValue = 100;

        // �����̴��� �⺻���� 0���� ����
        slider.value = 0;

        // �����̴� ���� ����� �� ȣ��� �޼��� ���
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        // �ؽ�Ʈ ������Ʈ
        UpdateTextValue(slider.value);
    }

    // �����̴� ���� ����� �� ȣ��Ǵ� �޼���
    public void OnSliderValueChanged()
    {
        // �ؽ�Ʈ ������Ʈ
        UpdateTextValue(slider.value);
    }

    // �ؽ�Ʈ ������Ʈ �޼���
    void UpdateTextValue(float value)
    {
        // �����̴� ���� ������ ��ȯ
        int intValue = Mathf.RoundToInt(value);

        // �ؽ�Ʈ ������Ʈ
        textValue.text = intValue.ToString() + "��";
    }
}
