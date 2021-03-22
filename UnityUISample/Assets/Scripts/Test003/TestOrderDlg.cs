using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 [ 문제 ]: 
3개의 정수를 입력받아 가장 큰 수를 출력하세요.

[ 입력 ] 
값은  0보다 크거나 같고, 100보다 작거나 같은 정수이다.
  
 */

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

        int[] aOrder = OrderByLarge(nValue1, nValue2, nValue3);
        
        m_txtResult.text = string.Format( "가장 큰 수 = {0}\n", nMax);

        for( int i = 0; i < aOrder.Length; i++)
        {
            m_txtResult.text += aOrder[i] + ", ";
        }

    }

    // 3개중 가장 큰수 찾기
    public int FindMaxNumber(int a, int b, int c)
    {
        int nMax = a;

        if (b > nMax)
            nMax = b;

        if (c > nMax)
            nMax = c;

        return nMax;       
    }

    // 큰수부터 정렬하기
    public int[] OrderByLarge(int a, int b, int c)
    {
        int[] arr = new int[3] { a, b, c };
        
        for( int i = 0; i < arr.Length-1; i++)
        {
            for( int j = 1; j < arr.Length; j++)
            {
                if (arr[i] < arr[j])
                {
                    Swap( ref arr[i],  ref arr[j]);
                }
            }
        }

        return arr;
    }


    public void Swap( ref int a, ref int b)
    {
        int c = a;
        a = b;
        b = c;
    }


    public void Swap<T>( ref T a, ref T b)
    {
        T c = a;
        a = b;
        b = c; 
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
        m_editA.text = "";
        m_editB.text = "";
        m_editC.text = "";
    }
}
