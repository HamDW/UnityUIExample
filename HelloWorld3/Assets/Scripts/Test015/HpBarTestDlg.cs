using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarTestDlg : MonoBehaviour
{
    [SerializeField] int m_MaxHP = 100;                 // HP 최대값
    [SerializeField] int m_HPOffsetValue = 100;         // HP 초기값
    [SerializeField] int m_DamageValue = 10;            // 초당 데미지
    [SerializeField] int m_HealingValue = 15;           // 초당 회복값당 회복값

    [SerializeField] Text m_txtValue = null;
    [SerializeField] Slider m_HPBar = null;
    [SerializeField] Button m_btnStart = null;
    [SerializeField] Button m_btnStop = null;
    [SerializeField] Button m_btnClear = null;

    int m_nHPValue = 0;
    int m_nValueType = 0;               // heal = 0 , dot =1 타입
    bool m_bStart = false;



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

    IEnumerator EnumFunc_HealingUpdate(float fDelay)
    {
        while (m_bStart)
        {
            yield return new WaitForSeconds(fDelay);

            CalculateHP();
          }

        yield return null;
    }

    public void CalculateHP()
    {
        if (m_nValueType == 0)
        {
            Calculate_Dot();
        }
        else
        {
            Calculate_Healing();
        }
    }

    public void Calculate_Dot()
    {
        m_nHPValue -= m_DamageValue;
        if (m_nHPValue <= 0)
        {
            m_nHPValue = 0;
            m_bStart = false;
        }
        PrintHPValue();
        m_HPBar.value = m_nHPValue;
    }

    public void Calculate_Healing()
    {
        m_nHPValue += m_HealingValue;
        if (m_nHPValue >= m_MaxHP)
        {
            m_nHPValue = m_MaxHP;
            m_bStart = false;
        }
        PrintHPValue();
        m_HPBar.value = m_nHPValue;
    }


    public void OnClicked_Start()
    {
        if (!m_bStart)
        {
            m_bStart = true;
            StartCoroutine("EnumFunc_HealingUpdate", 1.0f);
        }
    }

    public void OnClicked_Stop()
    {
        m_bStart = false;
    }


    public void OnClicked_Clear()
    {
        m_bStart = false;

        m_nHPValue = m_HPOffsetValue;
        m_HPBar.value = m_nHPValue;

    }

    public void OnChanged_ValueType(int iIndex)
    {
        m_nValueType = iIndex;

    }

    public void PrintHPValue()
    {
        m_txtValue.text = string.Format("{0}/{1}", m_nHPValue, m_MaxHP);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
