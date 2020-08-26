using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTest2Dlg : MonoBehaviour
{
    //public static string[] cCityList = { "서울", "광주", "대전", "부산", "전주" };
    [SerializeField] Dropdown m_Dropdown;
    [SerializeField] Text m_txtResult;
    [SerializeField] Button m_btnResult;
    [SerializeField] Button m_btnClear;

    private List<string> m_listData = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        m_Dropdown.onValueChanged.AddListener(delegate {
            OnValueChanged_CityList(m_Dropdown);
        });
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);

        Initialize1();
    }

    private void Initialize1()
    {
        m_listData.Add("서울");
        m_listData.Add("광주");
        m_listData.Add("대전");
        m_listData.Add("부산");
        m_listData.Add("전주");

        m_Dropdown.AddOptions(m_listData);
    }

    public void OnValueChanged_CityList(Dropdown kDropdown)
    {
        int nPos = kDropdown.value;
        string sCity = m_listData[nPos];

        m_txtResult.text = nPos + " : " + sCity;
    }


    public void OnClicked_Result()
    {
        int nPos = m_Dropdown.value;
        string sCity = m_listData[nPos];
        string sResult = "당신이 이동할 도시는 " + sCity + "입니다. ";
        m_txtResult.text = sResult;
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "초기화 됐습니다.";
        m_Dropdown.value = 0;
    }
}
