using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

// 배열을 이용해서 결과를 출력해보자


public class ToggleGroupTestDlg : MonoBehaviour
{
    static string[] DName = { "사과", "배", "오렌지" };

    [SerializeField] ToggleGroup m_ToggleGroup = null;
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Button m_btnClear = null;

    [SerializeField] Toggle m_toggleApple = null;
    [SerializeField] Toggle m_togglePear = null;
    [SerializeField] Toggle m_toggleOrange = null;


    string m_sValue = "";


    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
        m_sValue = DName[0];

        m_toggleApple.onValueChanged.AddListener(OnValueChanged_Apple);
        m_togglePear.onValueChanged.AddListener(OnValueChanged_Pear);
        m_toggleOrange.onValueChanged.AddListener(OnValueChanged_Orange);
    }

    public void OnClicked_Result()
    {
        string strResult = "";
        if (m_sValue.Equals(""))
            strResult = "선택된 과일이 없습니다.";
        else
            strResult = "당신이 선택한 과일은 <color=#1CD9BA>" + m_sValue + "</color> 입니다.";
        
        m_txtResult.text = strResult;
    }



    //public void OnValueChanged_Value(int idx, bool isOn)
    //{
    //    m_sValue = DName[idx];
    //    m_txtResult.text = m_sValue;

    //}
    public void OnValueChanged_Apple(bool isOn)
    {
        if (isOn)
        {
            m_sValue = DName[0];
            m_txtResult.text = DName[0];
        }
    }
    public void OnValueChanged_Pear(bool isOn)
    {
        if (isOn)
        {
            m_sValue = DName[1];
            m_txtResult.text = DName[1];
        }
    }
    public void OnValueChanged_Orange(bool isOn)
    {
        if (isOn)
        {
            m_sValue = DName[2];
            m_txtResult.text = DName[2];
        }
    }

    public void OnClicked_Clear()
    {
        m_ToggleGroup.SetAllTogglesOff();
        m_txtResult.text = "초기화 되었습니다.";
        m_sValue = "";
    }

}
