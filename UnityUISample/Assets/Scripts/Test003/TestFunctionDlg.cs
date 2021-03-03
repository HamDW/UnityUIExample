using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestFunctionDlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;

    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
    }


    public void OnClicked_OK()
    {
        m_txtResult.text = "";

        TestFunction1();
        //TestFunction2();
        //TestIf();
        //TestSwitch();
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
    }

    public void TestFunction1()
    {
        int nSum = Sum(10, 20);
        string sResult = string.Format("sum =  {0}", nSum);
        Debug.Log(sResult);

        m_txtResult.text = sResult + "\n";

        m_txtResult.text += "----------------------------------------\n\n";
    }


    // call by value 와 call by reference
    public void TestFunction2()
    {
        int a = 100;
        int b = 200;

        string sResult = string.Format("a = {0}, b = {1}", a, b);
        m_txtResult.text += sResult + "\n";
        Debug.Log(sResult);

        SwapTest(a, b);
        sResult = string.Format("a = {0}, b = {1}", a, b);
        m_txtResult.text += sResult + "\n";
        Debug.Log(sResult);

        Swap(ref a, ref b);
        sResult = string.Format("a = {0}, b = {1}", a, b);
        m_txtResult.text += sResult + "\n";
        Debug.Log(sResult);

        m_txtResult.text += "----------------------------------------\n\n";
    }


    public int Sum(int a, int b)
    {
        return a + b;
    }

    public void SwapTest(int a, int b)
    {
        int c = a;
        a = b;
        b = c;
    }

    public void Swap(ref int a, ref int b)
    {
        int c = a;
        a = b;
        b = c;
    }


    //// 제너릭 함수
    //public void Swap<T>( ref T a, ref T b )  {
    //    T c = a;
    //    a = b;
    //    b = c;
    //}

    /*
     *  if 문 테스트  ( 프로그래밍 책 89 쪽)
     */
    public void TestIf()
    {
        string sResult = "";

        int a = 10;
        int b = 20;

        if (b > a)
        {
            sResult = "b 가 a 보다 큽니다.";
        }
        else if (a > b)
        {
            sResult = "a 가 b 보다 큽니다.";
        }
        else
        {
            sResult = "a 와 b 는 같다.";
        }

        // &&, || 설명
        Debug.Log(sResult);
        m_txtResult.text += sResult + "\n";

        m_txtResult.text += "----------------------------------------\n\n";
    }

    /*
     *  switch 문 테스트  ( p92 )
     */
    public void TestSwitch()
    {
        string sResult = "";

        const int apple = 1;
        const int orange = 2;
        const int banana = 3;

        int a = orange;

        switch (a)
        {
            case apple:
                sResult = "사과입니다.";
                break;
            case orange:
                sResult = "오렌지입니다.";
                break;
            case banana:
                sResult = "바나나입니다.";
                break;
        }
        Debug.Log(sResult);
        m_txtResult.text += sResult + "\n";
        m_txtResult.text += "----------------------------------------\n";
    }

}
  
