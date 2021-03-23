using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScore
{
    public string m_Name;
    public int m_Kor;
    public int m_Eng;
    public int m_Mat;

    public int GetTotal()
    {
        return m_Kor + m_Eng + m_Mat;
    }

    public float GetAvg()
    {
        return GetTotal() / 3;
    }

    public CScore( string name, int kor, int eng, int mat )
    {
        m_Name = name;
        m_Kor = kor;
        m_Mat = mat;
        m_Eng = eng;
    }
    public CScore()
    {

    }
}

public class Score082Dlg : MonoBehaviour
{
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] Button m_btnAdd = null;
    [SerializeField] InputField m_InputName = null;
    [SerializeField] InputField m_InputKor = null;
    [SerializeField] InputField m_InputEng = null;
    [SerializeField] InputField m_InputMath = null;
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Text m_txtSubRes = null;

    public List<CScore> m_listScore = new List<CScore>();

    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
        m_btnAdd.onClick.AddListener(OnClicked_Add);
    }


    public void OnClicked_Add()
    {
        string name = m_InputName.text;
        int kor = int.Parse(m_InputKor.text);
        int eng = int.Parse(m_InputEng.text);
        int mat = int.Parse(m_InputMath.text);

        CScore kScore = new CScore(name, kor, eng, mat);
        m_listScore.Add(kScore);

        m_txtSubRes.text = "";

        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore sr = m_listScore[i];
            m_txtSubRes.text += string.Format("{0}: {1}, {2}, {3}\n",
                                sr.m_Name, sr.m_Kor, sr.m_Eng, sr.m_Mat);
        }

    }

    public void OnClicked_OK()
    {
        m_txtResult.text = "";

        m_txtResult.text += "[성적관리]\n";
        m_txtResult.text += "===================================\n";

        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore sr = m_listScore[i];
            m_txtResult.text += string.Format("{0} : {1}, {2}, {3} : 합계={4}\t 평균={5:0.0} \n",
                                sr.m_Name, sr.m_Kor, sr.m_Eng, sr.m_Mat, sr.GetTotal(), sr.GetAvg());
        }
    }


    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
        m_InputName.text = "";
        m_InputKor.text = "";
        m_InputEng.text = "";
        m_InputMath.text = "";
    }
}
