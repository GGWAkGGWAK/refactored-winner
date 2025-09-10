using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI textValue; // TextMeshProUGUI로 변경
    public Slider slider;

    private void Start()
    {
        // 슬라이더의 최소값과 최대값을 설정
        slider.minValue = 0;
        slider.maxValue = 100;

        // 슬라이더의 기본값을 0으로 설정
        slider.value = 0;

        // 슬라이더 값이 변경될 때 호출될 메서드 등록
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        // 텍스트 업데이트
        UpdateTextValue(slider.value);
    }

    // 슬라이더 값이 변경될 때 호출되는 메서드
    public void OnSliderValueChanged()
    {
        // 텍스트 업데이트
        UpdateTextValue(slider.value);
    }

    // 텍스트 업데이트 메서드
    void UpdateTextValue(float value)
    {
        // 슬라이더 값을 정수로 변환
        int intValue = Mathf.RoundToInt(value);

        // 텍스트 업데이트
        textValue.text = intValue.ToString() + "개";
    }
}
