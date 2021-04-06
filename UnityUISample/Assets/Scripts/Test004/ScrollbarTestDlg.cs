using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarTestDlg : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Scrollbar m_scrollbarNum = null;


    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        
        //m_scrollbarNum.onValueChanged.AddListener( delegate { OnValueChanged_Scrollbar(); });
    }

    public void OnValueChanged_Scrollbar()
    {
        string strValue = "" + m_scrollbarNum.value;

        m_txtResult.text = strValue;
    }

    public void OnClicked_Result()
    {

        float fValue = m_scrollbarNum.value;
        string strResult = "현재 진행된 값은 <color=#FAA500>" + fValue + "</color> 입니다.";
        m_txtResult.text = strResult;

    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "초기화 되었습니다.";
        m_scrollbarNum.value = 0;
    }
}
