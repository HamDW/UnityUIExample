using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class SliderTestDlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Slider m_sliderNum = null;

    [SerializeField] int m_MinValue = 0;        // 초기값 설정 연습
    [SerializeField] int m_MaxValue = 1;


    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);

        m_sliderNum.minValue = m_MinValue;
        m_sliderNum.maxValue = m_MaxValue;
        m_sliderNum.value = 0;
    }
      
    public void OnValueChanged_SliderNumber()
    {
        string strValue = "" + m_sliderNum.value;
        
        m_txtResult.text = strValue;
    }

    public void OnClicked_Result() {

        float fValue = m_sliderNum.value;
        string strResult = "현재 진행된 값은 <color=#FAA500>" + fValue + "</color> 입니다.";
        m_txtResult.text = strResult;

    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "초기화 되었습니다.";
        m_sliderNum.value = 0;
    }


}
