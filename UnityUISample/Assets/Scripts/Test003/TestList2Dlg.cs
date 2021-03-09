using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/*
 * 
[ 문제 ]: 
5개의 정수를 입력받아 리스트에 추가하고 작은 순서대로 정렬하여 출력한다.

[ 입력 ] 
값은  0보다 크거나 같고, 100보다 작거나 같은 정수이다.

 * 
 */

public class TestList2Dlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;

    [SerializeField] InputField m_editNum = null;
    [SerializeField] Text m_txtNumbers = null;
    [SerializeField] Button m_btnAdd = null;


    private List<int> m_listNum = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
        m_btnAdd.onClick.AddListener(OnClicked_AddNum);

    }
    public void OnClicked_AddNum()
    {
        if (m_listNum.Count > 5)
        {
            m_txtNumbers.text = "5개를 초과 했습니다.";
            return;
        }

        if (m_editNum.text.Equals(""))
        {
            m_txtNumbers.text = "숫자를 입력해주세요.";
            return;
        }

        int nNum = int.Parse(m_editNum.text);
        m_listNum.Add(nNum);


        PrintNumbers();
    }

    public void PrintNumbers()
    {
        m_txtNumbers.text = "";
        for (int i = 0; i < m_listNum.Count; i++)
        {
            m_txtNumbers.text += m_listNum[i] + ", ";
        }
    }

    public void OnClicked_OK()
    {
        m_txtResult.text = "";
        m_listNum.Sort();                               // 작은수 부터 정렬
        //m_listNum.Sort((a, b) => a > b ? 1 : -1);     // 작은수 부터 정렬
        //m_listNum.Sort( (a, b) => b.CompareTo(a) );   // 큰수 부터 정렬
        //m_listNum.Reverse();                          // 역순으로 다시 정렬

        for ( int i = 0; i < m_listNum.Count; i++)
        {
            m_txtResult.text += m_listNum[i] + ", ";
        }
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
        m_txtNumbers.text = "숫자 리스트";
    }

}
