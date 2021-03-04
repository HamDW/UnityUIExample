using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestOrderDlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] InputField m_editA = null;
    [SerializeField] InputField m_editB = null;
    [SerializeField] InputField m_editC = null;


    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
    }


    public void OnClicked_OK()
    {
        m_txtResult.text = "";
        if (m_editA.text.Equals("") ||
            m_editB.text.Equals("") ||
            m_editC.text.Equals("") )
            return;

        int nValue1 = int.Parse(m_editA.text);
        int nValue2 = int.Parse(m_editB.text);
        int nValue3 = int.Parse(m_editC.text);

        int nMax = FindMaxNumber( nValue1,  nValue2, nValue3);
        m_txtResult.text = string.Format( "가장 큰 수 = {0}", nMax);
    }


    public int FindMaxNumber(int a, int b, int c)
    {
        int nMax = 0;
        if( a > b) 
        {
            if (a > c)
                nMax = a;
            else
                nMax = c;
        }
        else
        {
            if (b > c)
                nMax = b;
            else
                nMax = c;
        }

        return nMax;       
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
        m_editA.text = "";
        m_editB.text = "";
        m_editC.text = "";
    }
}
