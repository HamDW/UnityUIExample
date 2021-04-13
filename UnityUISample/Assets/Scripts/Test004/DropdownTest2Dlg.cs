using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTest2Dlg : MonoBehaviour
{

    [SerializeField] Dropdown m_Dropdown = null;
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Button m_btnClear = null;

    private List<string> m_listData = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //m_Dropdown.onValueChanged.AddListener(delegate (int pos) {
        //    OnValueChanged_CityList(pos);
        //});
        m_Dropdown.onValueChanged.AddListener(OnValueChanged_CityList);
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);

        Initialize();
    }

    private void Initialize()
    {
        m_listData.Add("서울");
        m_listData.Add("광주");
        m_listData.Add("대전");
        m_listData.Add("부산");
        m_listData.Add("전주");

        m_Dropdown.AddOptions(m_listData);
    }

    public void OnValueChanged_CityList(int nPos)
    {
        //int nPos = kDropdown.value;
        string sCity = m_listData[nPos];

        m_txtResult.text = nPos + " : " + sCity;
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
