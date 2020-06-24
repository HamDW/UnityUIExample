using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTestDlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult;
    [SerializeField] Button m_btnResult;
    [SerializeField] Toggle m_toggleApple;
    [SerializeField] Toggle m_togglePear;
    [SerializeField] Toggle m_toggleOrange;

    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
    }


    public void OnClicked_Result()
    {
        string strValue = "";
        if( m_toggleApple.isOn == true )
        {
            strValue += "사과 ";
        }
        if( m_togglePear.isOn == true)
        {
            strValue += "배 ";
        }
        if (m_toggleOrange.isOn == true)
        {
            strValue += "오렌지 ";
        }

        string strResult = "당신이 선택한 과일은 " + strValue + "입니다.";

        m_txtResult.text = strResult;
    }

    public void OnValueChanged_Value(int idx )
    {

    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "출력";
        m_togglePear.isOn = false;
        m_toggleApple.isOn = false;
        m_toggleOrange.isOn = false;
    }

}
