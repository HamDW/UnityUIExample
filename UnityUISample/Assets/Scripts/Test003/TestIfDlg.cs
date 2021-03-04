using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 문제1 : 시험 점수를 입력받아 90 ~ 100점은 A, 80 ~ 89점은 B, 70 ~ 79점은 C, 
//        60 ~69점은 D, 나머지 점수는 F를 출력하는 프로그램을 작성하시오
//
// 입력 : 시험 점수는 0보다 크거나 같고, 100보다 작거나 같은 정수이다.


public class TestIfDlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] InputField m_editScore = null;

    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
    }


    public void OnClicked_OK()
    {
        m_txtResult.text = "";
        if (m_editScore.text == null || m_editScore.text.Equals(""))
            return;

        int nScore = int.Parse(m_editScore.text);

        string sGrade = MakeGrade(nScore);
        m_txtResult.text = "당신의 등급은 " + sGrade + " 입니다.";
    }

    public string MakeGrade(int nScore)
    {
        string sGrade = "";
        if( nScore >= 90 )
        {
            sGrade = "A";
        }
        else if( nScore >= 80 )
        {
            sGrade = "B";
        }
        else if( nScore >= 70 )
        {
            sGrade = "C";
        }
        else if (nScore >= 60)
        {
            sGrade = "D";
        }
        else
        {
            sGrade = "F";
        }

        return sGrade;
    }


    public string MakeGrade2(int nScore)
    {
        string sGrade = "";
        int nValue = nScore / 10;
        switch( nValue )
        {
            case 10:
            case 9 :
                sGrade = "A";
                break;
            case 8:
                sGrade = "B";
                break;
            case 7:
                sGrade = "C";
                break;
            case 6:
                sGrade = "D";
                break;
            default:
                sGrade = "F";
                break;
        }
        return sGrade;
    }


    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
        m_editScore.text = "";
    }
}
