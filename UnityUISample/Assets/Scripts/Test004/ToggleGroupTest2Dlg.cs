using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleGroupTest2Dlg : MonoBehaviour
{
    static string[] DName = { "���", "��", "������" };

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
        string strResult = "����� ������ ������ <color=#9FF32F>" + m_sValue + "</color> �Դϴ�.";
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
        m_txtResult.text = "�ʱ�ȭ �Ǿ����ϴ�.";
    }




}
