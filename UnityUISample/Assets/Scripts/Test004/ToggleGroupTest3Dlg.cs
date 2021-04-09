using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToggleGroupTest3Dlg : MonoBehaviour
{
    public static string[] DName = { "사과", "배", "오렌지" };
    public const int COUNT = 100;

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

        m_toggleApple.onValueChanged.AddListener( delegate(bool bOn) {
             OnValueChanged_Value(0, bOn); 
        });
        m_togglePear.onValueChanged.AddListener((bool isOn) => { 
            OnValueChanged_Value(1, isOn); 
        });
        m_toggleOrange.onValueChanged.AddListener((isOn) => OnValueChanged_Value(2, isOn));
        

    }

    public void OnClicked_Result()
    {
        string strResult = "당신이 선택한 과일은 <color=#1CD9BA>" + m_sValue + "</color> 입니다.";
        m_txtResult.text = strResult;


    }

    public void OnValueChanged_Value(int idx, bool isOn)
    {
        m_sValue = DName[idx];
        m_txtResult.text = m_sValue;

    }


    public void OnClicked_Clear()
    {
        m_ToggleGroup.SetAllTogglesOff();
        m_txtResult.text = "초기화 되었습니다.";
    }
}
