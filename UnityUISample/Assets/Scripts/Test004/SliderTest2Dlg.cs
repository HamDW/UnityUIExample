using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTest2Dlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] Slider m_sliderR = null;
    [SerializeField] Slider m_sliderG = null;
    [SerializeField] Slider m_sliderB = null;

    Color m_Color = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
     
        Initialize_Slider();
        //Initialize_Slider2();
        Initialize();
    }

    public void Initialize_Slider()
    {
        m_sliderR.onValueChanged.AddListener(OnValueChanged_SliderR);
        m_sliderG.onValueChanged.AddListener(OnValueChanged_SliderG);
        m_sliderB.onValueChanged.AddListener(OnValueChanged_SliderB);
    }
    public void Initialize_Slider2()
    {
        m_sliderR.onValueChanged.AddListener((pos) => OnValueChanged_Slider(0, pos));
        m_sliderG.onValueChanged.AddListener((pos) => OnValueChanged_Slider(1, pos));
        m_sliderB.onValueChanged.AddListener((pos) => OnValueChanged_Slider(2, pos));
    }
    public void Initialize()
    {
        m_sliderR.minValue = 0;
        m_sliderR.maxValue = 1;
        m_sliderR.value = 0;

        m_sliderG.minValue = 0;
        m_sliderG.maxValue = 1;
        m_sliderG.value = 0;

        m_sliderB.minValue = 0;
        m_sliderB.maxValue = 1;
        m_sliderB.value = 0;
    }

    public void OnValueChanged_SliderR(float pos)
    {
        m_Color.r = pos;
        m_txtResult.color = m_Color;
        m_txtResult.text = "현재 색상 값 입니다.";
    }
    public void OnValueChanged_SliderG(float pos)
    {
        m_Color.g = pos;
        m_txtResult.color = m_Color;
        m_txtResult.text = "현재 색상 값 입니다.";
    }
    public void OnValueChanged_SliderB(float pos)
    {
        m_Color.b = pos;
        m_txtResult.color = m_Color;
        m_txtResult.text = "현재 색상 값 입니다.";
    }

    public void OnValueChanged_Slider(int idx, float pos)
    {
        Color color = m_txtResult.color;
        if (idx == 0)
            color.r = pos;
        if (idx == 1)
            color.g = pos;
        if (idx == 2)
            color.b = pos;

        m_txtResult.color = color;
        m_txtResult.text = "현재 색상 값 입니다.";
    }

    public void OnClicked_Result()
    {
        m_txtResult.text = "현재 색상 값 입니다.";
    }

    public void OnClicked_Clear()
    {
        Initialize();
        m_txtResult.text = "초기화 되었습니다.";
        
    }

}
