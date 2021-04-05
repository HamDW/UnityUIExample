using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTestDlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Toggle m_toggleApple = null;
    [SerializeField] Toggle m_togglePear = null;
    [SerializeField] Toggle m_toggleOrange = null;

    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);

        m_toggleApple.onValueChanged.AddListener(OnValueChanged_Apple);
        m_togglePear.onValueChanged.AddListener(OnValueChanged_Pear);
        m_toggleOrange.onValueChanged.AddListener(OnValueChanged_Orange);
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

        string strResult = "";
        if (strValue.Equals(""))
        {
            strResult = "당신이 선택한 과일은 없습니다.";
        }
        else
        {
            strResult = "당신이 선택한 과일은 <color=#FF8020>" + strValue + "</color>입니다.";
        }

        m_txtResult.text = strResult;
    }

    public void OnValueChanged_Apple(bool isOn)
    {
        if( isOn )
            m_txtResult.text = "사과";
        else
            m_txtResult.text = "";
    }
    public void OnValueChanged_Pear(bool isOn)
    {
        if (isOn)
            m_txtResult.text = "배";
        else
            m_txtResult.text = "";
    }
    public void OnValueChanged_Orange(bool isOn)
    {
        if (isOn)
            m_txtResult.text = "오렌지";
        else
            m_txtResult.text = "";
    }


    //public void OnValueChanged_Value(int idx, bool isOn )
    //{
    //    string strValue = "";
    //    if (isOn)
    //    {
    //        if (idx == 0)
    //            strValue = "사과 ";
    //        else if (idx == 1)
    //            strValue = "배";
    //        else
    //            strValue = "오렌지";
    //    }
    //    m_txtResult.text = strValue;

    //}

    public void OnClicked_Clear()
    {
        m_txtResult.text = "출력";
        m_togglePear.isOn = false;
        m_toggleApple.isOn = false;
        m_toggleOrange.isOn = false;
    }

}
