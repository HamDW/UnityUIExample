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
        m_scrollbarNum.onValueChanged.AddListener( OnValueChanged_Scrollbar );
        
        // 같은 내용임.
        //m_scrollbarNum.onValueChanged.AddListener(delegate( float pos) {
        //    OnValueChanged_Scrollbar(pos);
        //});

        //m_scrollbarNum.onValueChanged.AddListener((float pos)=> {
        //    OnValueChanged_Scrollbar(pos);
        //});

        //m_scrollbarNum.onValueChanged.AddListener((pos) => {
        //    OnValueChanged_Scrollbar(pos);
        //});

        //m_scrollbarNum.onValueChanged.AddListener((pos) => OnValueChanged_Scrollbar(pos) );

    }

    public void OnValueChanged_Scrollbar(float pos)
    {
        m_txtResult.text = pos.ToString();
    }

    // 인스펙터창에 직접 넣어서 사용할경우에 사용
    public void OnValueChanged_Scrollbar2()
    {
        m_txtResult.text = m_scrollbarNum.value.ToString();
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
