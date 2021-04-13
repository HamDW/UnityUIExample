          using System;
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

    public float m_yOffset = 430;
    public float m_xOffset = 620;

    List<string> m_listData = new List<string>();


    Vector3 m_vPos = Vector3.zero;      // 주의) 반드시 localPosition기준으로 체크해야 한다.
    public float m_Speed = 1.0f;

    bool m_bLeft = false;
    bool m_bRight = false;
    bool m_bUp = false;
    bool m_bDown = false;


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

        m_vPos = this.transform.localPosition; // m_rectTransform.position;
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
        Move_ToDown();
        Move_ToUp();
        Move_ToRight();
        Move_ToLeft();
    }

    public void Move_ToDown()
    {
        if (!m_bDown) return;

        m_vPos.y -= Time.deltaTime * m_Speed * 100;
        if (m_vPos.y <= 0.0f){
            m_vPos.y = 0.0f;
        }
        this.transform.localPosition = m_vPos;
    }

    public void Move_ToUp()
    {
        if (!m_bUp) return;

        m_vPos.y += Time.deltaTime * m_Speed * 100;
        if (m_vPos.y >= 0.0f)
        {
            m_vPos.y = 0.0f;
        }
        this.transform.localPosition = m_vPos;
    }

    public void Move_ToRight()
    {
        if (!m_bRight) return;

        m_vPos.x += Time.deltaTime * m_Speed * 100;
        if (m_vPos.x >= 0.0f)
        {
            m_vPos.x = 0.0f;
        }
        this.transform.localPosition = m_vPos;
    }

    public void Move_ToLeft()
    {
        if (!m_bLeft) return;

        m_vPos.x -= Time.deltaTime * m_Speed * 100;
        if (m_vPos.x <= 0.0f)
        {
            m_vPos.x = 0.0f;
        }
        this.transform.localPosition = m_vPos;
    }


    public void Update_Key()
    {
        // down -> Up 
        if ( Input.GetKeyDown(KeyCode.W))
        {
            ClearKey();
            m_vPos = this.transform.localPosition;
            m_vPos.y = -m_yOffset;
            this.transform.localPosition = m_vPos;
            m_bUp = true;
        }
        // Up -> down  
        if (Input.GetKeyDown(KeyCode.S))
        {
            ClearKey();
            m_vPos = this.transform.localPosition;
            m_vPos.y = m_yOffset;
            this.transform.localPosition = m_vPos;
            m_bDown = true;
        }
        // Right -> Left
        if (Input.GetKeyDown(KeyCode.A))
        {
            ClearKey();
            m_vPos = this.transform.localPosition;
            m_vPos.x = m_xOffset;
            this.transform.localPosition = m_vPos;
            m_bLeft = true;
        }
        // Left -> Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            ClearKey();
            m_vPos = this.transform.localPosition;
            m_vPos.x = -m_xOffset;
            this.transform.localPosition = m_vPos;
            m_bRight = true;
        }
    }

    public void ClearKey()
    {
        m_bLeft = false;
        m_bRight = false;
        m_bUp = false;
        m_bDown = false;
    }

}
