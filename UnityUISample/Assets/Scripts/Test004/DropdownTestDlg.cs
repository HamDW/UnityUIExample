﻿          using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTestDlg : MonoBehaviour
{
    //public static string[] cCityList = { "서울", "광주", "대전", "부산", "전주" };
    [SerializeField] Dropdown m_Dropdown = null;
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Button m_btnClear = null;

    List<string> m_listData = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);

        m_Dropdown.onValueChanged.AddListener(OnValueChanged_CityList);

        //m_Dropdown.onValueChanged.AddListener( delegate (int pos) {
        //    OnValueChanged_CityList(pos);
        //});

        Initialize();
    }

    public void Initialize()
    {
        for( int i = 0; i < m_Dropdown.options.Count; i++)
        {
            Dropdown.OptionData kData = m_Dropdown.options[i];
            m_listData.Add(kData.text);
        }
    }


    public void OnValueChanged_CityList(int pos)
    {
        string sCity = m_listData[pos];
        m_txtResult.text = pos + " : " + sCity;
    }


    public void OnClicked_Result()
    {
        int nPos = m_Dropdown.value;
        string sCity = m_listData[nPos];
        string sResult = "당신이 이동할 도시는 <color=#8BF65A>" + sCity + "</color>입니다. ";
        m_txtResult.text = sResult;
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "초기화 됐습니다.";
        m_Dropdown.value = 0;
    }

}
