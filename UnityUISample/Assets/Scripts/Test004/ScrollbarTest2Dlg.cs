using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarTest2Dlg : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Scrollbar m_scrollbarNum = null;

    public float m_Speed = 1.0f;
    float m_fDeltaTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_scrollbarNum.onValueChanged.AddListener(OnValueChanged_Scrollbar);

        // ���� ������.
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
        Color color = m_txtResult.color;
        color.a = pos;
        m_txtResult.text = string.Format("{0:0.00}", pos);
        m_txtResult.color = color;
    }

   
    public void OnClicked_Result()
    {
        float fValue = m_scrollbarNum.value;
        string strResult = "���� ����� ���� <color=#FAA500>" + fValue + "</color> �Դϴ�.";
        m_txtResult.text = strResult;
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "�ʱ�ȭ �Ǿ����ϴ�.";
        m_scrollbarNum.value = 0;
    }

    void Update()
    {
        m_fDeltaTime += m_Speed * Time.deltaTime;
        if(m_fDeltaTime > 1.0f )
        {
            m_fDeltaTime = 0.0f;
            m_scrollbarNum.value += 0.05f;
            if (m_scrollbarNum.value > 1.0f)
                m_scrollbarNum.value = 1;
        }
    }
}
