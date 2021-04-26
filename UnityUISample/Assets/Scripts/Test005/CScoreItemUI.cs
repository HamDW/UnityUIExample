using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScoreItemUI : MonoBehaviour
{
    [SerializeField] Text m_txtNo = null;
    [SerializeField] Text m_txtName = null;
    [SerializeField] Text m_txtKor = null;
    [SerializeField] Text m_txtMath = null;
    [SerializeField] Text m_txtEng = null;
    [SerializeField] Text m_txtTot = null;
    [SerializeField] Text m_txtAvg = null;


    public void Initialize(CScoreInfo.CScore kScore)
    {
        m_txtNo.text = kScore.m_idx.ToString();
        m_txtName.text = kScore.m_Name;
        m_txtKor.text = kScore.m_Kor.ToString();
        m_txtMath.text = kScore.m_Mat.ToString();
        m_txtEng.text = kScore.m_Eng.ToString();
        m_txtTot.text = kScore.Total.ToString();
        m_txtAvg.text = string.Format("{0:0.00}", kScore.Average);
    }


    public void SetSelectd( bool bSelect )
    {
        if (bSelect)
        {
            SetBgColor(new Color32(50, 255, 50, 255));
        }
        else
        {
            SetBgColor(Color.white);
        }
    }


    public void SetBgColor(Color color)
    {
        Image kImage = GetComponent<Image>();
        kImage.color = color;
    }



}
