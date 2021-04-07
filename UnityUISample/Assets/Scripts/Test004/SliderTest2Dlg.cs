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
    [SerializeField] Text m_txtR = null;
    [SerializeField] Text m_txtG = null;
    [SerializeField] Text m_txtB = null;


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

    // 슬라이더 기본적인 사용법
    public void Initialize_Slider()
    {
        m_sliderR.onValueChanged.AddListener(OnValueChanged_SliderR);
        m_sliderG.onValueChanged.AddListener(OnValueChanged_SliderG);
        m_sliderB.onValueChanged.AddListener(OnValueChanged_SliderB);
    }
    public void Initialize()
    {
        //m_sliderR.minValue = 0;
        //m_sliderR.maxValue = 1;
        //m_sliderG.minValue = 0;
        //m_sliderG.maxValue = 1;
        //m_sliderB.minValue = 0;
        //m_sliderB.maxValue = 1;

        m_sliderR.value = 0;
        m_sliderG.value = 0;
        m_sliderB.value = 0;
    }

    public void OnValueChanged_SliderR(float pos)
    {
        m_Color.r = pos;
        m_txtResult.color = m_Color;
        m_txtResult.text = "현재 색상 값 입니다.";

        int nPos = (int)(pos * 255);
        m_txtR.text = nPos.ToString();
    }
    public void OnValueChanged_SliderG(float pos)
    {
        m_Color.g = pos;
        m_txtResult.color = m_Color;
        m_txtResult.text = "현재 색상 값 입니다.";

        int nPos = (int)(pos * 255);
        m_txtG.text = nPos.ToString();

    }
    public void OnValueChanged_SliderB(float pos)
    {
        m_Color.b = pos;
        m_txtResult.color = m_Color;
        m_txtResult.text = "현재 색상 값 입니다.";

        int nPos = (int)(pos * 255);
        m_txtB.text = nPos.ToString();
    }


    public void OnClicked_Result()
    {
        int r = (int)(m_Color.r * 255);
        int g = (int)(m_Color.g * 255);
        int b = (int)(m_Color.b * 255);

        m_txtResult.text = string.Format("현재 RGB 색상값=> ( R:{0}, G:{1}, B:{2} )", r, g, b);
    }

    public void OnClicked_Clear()
    {
        Initialize();
        m_txtResult.text = "초기화 되었습니다.";
        
    }

    // 슬라이더 응용 사용법 ( 람다식 이용 )
    public void Initialize_Slider2()
    {
        m_sliderR.onValueChanged.AddListener((pos) => OnValueChanged_Slider(0, pos));
        m_sliderG.onValueChanged.AddListener((pos) => OnValueChanged_Slider(1, pos));
        m_sliderB.onValueChanged.AddListener((pos) => OnValueChanged_Slider(2, pos));
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

}
