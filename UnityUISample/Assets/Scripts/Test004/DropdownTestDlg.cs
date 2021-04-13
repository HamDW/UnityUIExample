          using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownTestDlg : MonoBehaviour
{
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
        //m_Dropdown.onValueChanged.AddListener((int pos)=>{
        //    OnValueChanged_CityList(pos);
        //});

        //m_Dropdown.onValueChanged.AddListener((pos) => OnValueChanged_CityList(pos) );

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
    void Update()
    {
        Update_Key();
        Move_ToUp();
    }


    public void Move_ToUp()
    {
        Vector3 vPos = transform.localPosition;
        vPos.y += Time.deltaTime * 200;
        if (vPos.y >= 0.0f){
            vPos.y = 0.0f;
        }
        this.transform.localPosition = vPos; // 주의) 반드시 localPosition기준으로 체크해야 한다.
    }

    // 위와 같은 결과
    public void Move_ToUp2()
    {
        this.transform.localPosition += Vector3.up * Time.deltaTime * 200;
        if (transform.localPosition.y > 0.0f)
            transform.localPosition = Vector3.zero;
    }

    public void Update_Key()
    {
        // down -> Up 
        if ( Input.GetKeyDown(KeyCode.W))
        {
            transform.localPosition = new Vector3(0, -400, 0);
        }

    }


}
