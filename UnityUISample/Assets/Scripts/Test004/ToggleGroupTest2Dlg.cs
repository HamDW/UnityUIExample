using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupTest2Dlg : MonoBehaviour
{
    static string[] DName = { "사과", "배", "오렌지" };

    [SerializeField] ToggleGroup m_ToggleGroup = null;
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    string m_sValue = "";


    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_sValue = DName[0];
    }

    public void OnClicked_Result()
    {
        string strResult = "당신이 선택하 과일은 <color=#9FF32F>" + m_sValue + "</color> 입니다.";
        m_txtResult.text = strResult;
    }

    public void OnChanged_Toggle(int iIndex)
    {
        m_sValue = DName[iIndex];
        m_txtResult.text = m_sValue;
    }

    public void OnClicked_Clear()
    {
        m_ToggleGroup.SetAllTogglesOff();
        m_txtResult.text = "초기화 되었습니다.";
    }




}
