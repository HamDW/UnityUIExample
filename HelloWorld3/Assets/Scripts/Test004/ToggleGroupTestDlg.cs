using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

// 배열을 이용해서 결과를 출력해보자


public class ToggleGroupTestDlg : MonoBehaviour
{
    [SerializeField] ToggleGroup m_ToggleGroup;
    [SerializeField] Text m_txtResult;
    [SerializeField] Button m_btnResult;
    string m_sValue = "";

    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_sValue = "사과";


    }


    public void OnClicked_Result()
    {
        string strResult = "당신이 선택하 과일은 " + m_sValue + " 입니다.";
        m_txtResult.text = strResult;
    }

    public void OnChanged_Toggle(int iIndex)
    {
        string[] aName = {"사과", "배", "오렌지" };
        m_sValue = aName[iIndex];
    }

    public void OnClicked_Clear()
    {
        m_ToggleGroup.SetAllTogglesOff();
        m_txtResult.text = "초기화 되었습니다.";
    }


}
