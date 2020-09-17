using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTestDlg : MonoBehaviour
{
    public static string[] cCityList = { "서울", "광주", "대전", "부산", "전주" };
    [SerializeField] Dropdown m_Dropdown = null;
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Button m_btnClear = null;


    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);

        m_Dropdown.onValueChanged.AddListener(delegate {
            OnValueChanged_CityList(m_Dropdown);
        });
    }


    public void OnValueChanged_CityList(Dropdown kDropdown)
    {
        int nPos = kDropdown.value;
        string sCity = cCityList[nPos];

        m_txtResult.text = nPos + " : " + sCity;
    }


    public void OnClicked_Result()
    {
        int nPos = m_Dropdown.value;
        string sCity = cCityList[nPos];
        string sResult = "당신이 이동할 도시는 " + sCity + "입니다. ";
        m_txtResult.text = sResult;
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "초기화 됐습니다.";
        m_Dropdown.value = 0;
    }

}
