using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

/*
 *  참고) OnValueChanged_SliderNumber을 인스펙터창에 직접 넣어 사용할경우에는
 *        함수인자 없이 사용하면 가능하다. 
 *        - position값은 m_sliderNum.value를 직접 이용하면 됨
 * 
 */
public class SliderTestDlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Slider m_sliderNum = null;


    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_sliderNum.onValueChanged.AddListener(OnValueChanged_SliderNumber);
        m_sliderNum.value = 0;
    }
      
    public void OnValueChanged_SliderNumber(float pos)
    {
        m_txtResult.text = pos.ToString();
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



    // 이렇게 사용해도 됨 
    // ( 인스펙터창의 Slider의 onValueChanged에  함수를 직접 추가하여 사용할경우 )
    public void OnValueChanged_Slider()
    {
        m_txtResult.text = m_sliderNum.value.ToString();
    }

}
