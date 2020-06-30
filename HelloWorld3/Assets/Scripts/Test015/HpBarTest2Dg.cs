using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpBarTest2Dg : MonoBehaviour
{
    [SerializeField] int m_MaxHP = 200;             // HP 최대값
    [SerializeField] int m_HPOffsetValue = 150;     // HP 초기값
    [SerializeField] int m_DamageValue = 10;        // 초당 데미지
    [SerializeField] int m_HealingValue = 15;       // 초당 회복값
    [SerializeField] float m_DotDelayTime = 0.5f;   // 독데미지가 들어오는 지연 시간( ex) 0.5초당 데미지가 10 들어옴 )
    [SerializeField] float m_HealDelayTime = 1.0f;  // HP가 회복 지연 시간( ex) 1초단위로 15씩 HP가 회복됨 )
    [SerializeField] Text m_txtValue = null;        // HP출력 텍스트 100/200 
    [SerializeField] Slider m_HPBar = null;         // HP 바
    [SerializeField] Button m_btnStart = null;
    [SerializeField] Button m_btnStop = null;
    [SerializeField] Button m_btnClear = null;


    int m_nHPValue = 0;                 // 현재 HP 값
    int m_nValueType = 0;               // heal = 0 , dot =1 타입
    bool m_bStart = false;              // Dot 데미지, 초당 힐링 시작..

    float m_fCurTime = 0;               // 현재 시간( 초당 단위 계산을 위한 값)


    // Start is called before the first frame update
    void Start()
    {
        m_btnStart.onClick.AddListener(OnClicked_Start);
        m_btnStop.onClick.AddListener(OnClicked_Stop);
        m_btnClear.onClick.AddListener(OnClicked_Clear);

        Initialize();
    }

    public void Initialize()
    {
        m_nHPValue = m_HPOffsetValue;
        m_HPBar.minValue = 0;
        m_HPBar.maxValue = m_MaxHP;
        m_HPBar.value = m_nHPValue;
        PrintHPValue();
    }

    void Update()
    {
        if (m_bStart)
        {
            m_fCurTime += Time.deltaTime;
            if (m_nValueType == 0)
                Update_Dot();
            else
                Update_Healing();
        }
    }

    // 독 데미지
    private void Update_Dot()
    {
        if (m_fCurTime >= m_DotDelayTime)
        {
            m_nHPValue -= m_DamageValue;
            if (m_nHPValue <= 0)
            {
                m_nHPValue = 0;
                m_bStart = false;
            }
            m_fCurTime = 0;

            PrintHPValue();
            m_HPBar.value = m_nHPValue;
        }
    }

    // 힐링
    private void Update_Healing()
    {
        if (m_fCurTime >= m_HealDelayTime)
        {
            m_nHPValue += m_HealingValue;
            if (m_nHPValue >= m_MaxHP)
            {
                m_nHPValue = m_MaxHP;
                m_bStart = false;
            }
            m_fCurTime = 0;
        }

        PrintHPValue();
        m_HPBar.value = m_nHPValue;
    }


    public void OnClicked_Start()
    {
        if (!m_bStart)
        {
            m_bStart = true;
            m_fCurTime = 0;
        }
    }

    public void OnClicked_Stop()
    {
        m_bStart = false;
        m_fCurTime = 0;
    }


    public void OnClicked_Clear()
    {
        m_bStart = false;

        m_nHPValue = m_HPOffsetValue;
        m_HPBar.value = m_nHPValue;
        PrintHPValue();
    }

    public void OnChanged_ValueType(int iIndex)
    {
        m_nValueType = iIndex;

    }

    public void PrintHPValue()
    {
        m_txtValue.text = string.Format("{0}/{1}", m_nHPValue, m_MaxHP);
    }


}
