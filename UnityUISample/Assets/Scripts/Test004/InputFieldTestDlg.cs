using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldTestDlg : MonoBehaviour
{
    [SerializeField] Text m_ResultText = null;
    [SerializeField] InputField m_InputName = null;
    [SerializeField] Button m_btnStart = null;

    // Start is called before the first frame update
    void Start()
    {
        m_btnStart.onClick.AddListener(OnClicked_Start); 
    }
     
    public void OnClicked_Start()
    {
        string sValue = m_InputName.text;
        if( m_InputName.text == "")
        {
            m_ResultText.text = "입력한 내용이 없어요.!!!\n이름을 입력해 주세요";
            return;
        }

        string sRes = "당신이 입력한 이름은 " + "<color=#FAA500>" + sValue  +"</color>" + " 입니다.";
        m_ResultText.text = sRes;
    }

    public void OnClicked_Clear()
    {
        m_ResultText.text = "정리 됐습니다.";
        m_InputName.text = "";
    }

}
