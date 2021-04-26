using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Score084Dlg : MonoBehaviour
{
    public class CScore
    {
        public string m_Name="";
        public int m_Kor = 0;
        public int m_Eng = 0;
        public int m_Mat = 0;

        //public int Total()
        //{
        //    return (m_Kor + m_Eng + m_Mat);
        //}

        public int Total { get { return m_Kor + m_Eng + m_Mat; } }


        public float Average
        {
            get { return (float)Total / 3; }
        }

        public CScore(string name, int kor, int eng, int mat)
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
    //--------------------------------------------------
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] Button m_btnLoad = null;
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
        m_btnLoad.onClick.AddListener(OnClicked_Load);
    }

    public void OnClicked_Add()
    {
        string name = m_InputName.text;
        int kor = int.Parse(m_InputKor.text);
        int eng = int.Parse(m_InputEng.text);
        int mat = int.Parse(m_InputMath.text);

        CScore kScore = new CScore(name, kor, eng, mat);
        m_listScore.Add(kScore);

        PrintSubData();
    }

    public void OnClicked_OK()
    {
        OrderByDescending();
        //PrintResult();
        PrintResult2();
        SaveFile();
    }

    public void OrderByDescending()
    {
        m_listScore.Sort(delegate (CScore a, CScore b)
        {
            if (a.Total < b.Total) return 1;
            else return -1;
        });
    }
    public void OrderByAscending()
    {
        m_listScore.Sort(delegate (CScore a, CScore b)
        {
            if (a.Total > b.Total) return 1;
            else return -1;
        });
    }
    public void OrderByAscending2()
    {
        m_listScore.Sort( (CScore a, CScore b) =>
        {
            return a.Total.CompareTo(b.Total);      // a가 b보다 크면 1,작으면 -1, 같으면 0
        });
    }

    public void OrderByAscending3()
    {
        m_listScore.Sort((a, b) => 
        {
            return a.Total.CompareTo(b.Total);
        });
    }
    public void OrderByAscending4()
    {
        m_listScore.Sort((a, b) => a.Total.CompareTo(b.Total) );
    }


    public void OrderByAscending5()
    {
        m_listScore.OrderBy((a) => a.Total );
    }

    
    public void PrintResult()
    {
        m_txtResult.text = "";
        m_txtResult.text += "[성적관리]\n";
        m_txtResult.text += "===================================\n";

        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore sr = m_listScore[i];
            m_txtResult.text += string.Format("{0}등 : {1}  {2}  {3}  {4} \t:합계={5}\t 평균={6:0.0} \n",
                                i+1, sr.m_Name, sr.m_Kor, sr.m_Eng, sr.m_Mat, sr.Total, sr.Average);
        }
        m_txtResult.text += "===================================\n";

        int nCount = m_listScore.Count;

        int nSumKor = 0;
        int nSumEng = 0;
        int nSumMat = 0;

        CalculateSum(out nSumKor, out nSumEng, out nSumMat);

        m_txtResult.text += string.Format("과목별 합계 -- 국어:({0}, {1:0.0}), 영어:({2}, {3:0.0}), 수학({4}, {5:0.0})\n",
                            nSumKor, (float)nSumKor / nCount,
                            nSumEng, (float)nSumEng / nCount,
                            nSumMat, (float)nSumMat / nCount);
    }
    
    public void CalculateSum(out int sumKor, out int sumEng, out int sumMat)
    {
        int nSumK = 0, nSumE = 0, nSumM = 0;

        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore kScore = m_listScore[i];
            nSumK += kScore.m_Kor;
            nSumE += kScore.m_Eng;
            nSumM += kScore.m_Mat;
        }
        sumKor = nSumK;
        sumEng = nSumE;
        sumMat = nSumM;
    }

    public void PrintResult2()
    {
        m_txtResult.text = "";
        m_txtResult.text = " Name kor Eng Mat   < 종합 >\n";
        m_txtResult.text += "===================================\n";
        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore sr = m_listScore[i];
            string sKor = MakeGrade(sr.m_Kor);
            string sEng = MakeGrade(sr.m_Eng);
            string sMat = MakeGrade(sr.m_Mat);
            string sAvg = MakeGrade((int)sr.Average);

            m_txtResult.text += string.Format("{0}    {1}    {2}    {3}       < {4} >  \n",
                                     sr.m_Name, sKor, sEng, sMat, sAvg );
        }

    }



    public void OnClicked_Load()
    {
        LoadFile();
        PrintSubData();
    }

    public void PrintSubData()
    {
        m_txtSubRes.text = "";

        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore sr = m_listScore[i];
            m_txtSubRes.text += string.Format("{0}: {1}, {2}, {3}\n",
                                sr.m_Name, sr.m_Kor, sr.m_Eng, sr.m_Mat);
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

    
    public string MakeGrade(int nScore)
    {
        string sGrade = "";
        if (nScore >= 90)
        {
            sGrade = "A";
        }
        else if (nScore >= 80)
        {
            sGrade = "B";
        }
        else if (nScore >= 70)
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



    public void SaveFile()
    {
        FileStream fs = new FileStream("Score.dat", FileMode.Create, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs);

        bw.Write(m_listScore.Count);
        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore kScore = m_listScore[i];
            bw.Write(kScore.m_Name);
            bw.Write(kScore.m_Kor);
            bw.Write(kScore.m_Eng);
            bw.Write(kScore.m_Mat);
        }
        bw.Close();
        fs.Close();
    }

    public void LoadFile()
    {
        m_listScore.Clear();

        FileStream fs = new FileStream("Score.dat", FileMode.Open, FileAccess.Read);
        if (fs == null) return;

        BinaryReader br = new BinaryReader(fs);
        int nCount = br.ReadInt32();
        for (int i = 0; i < nCount; i++)
        {
            string name = br.ReadString();
            int kor = br.ReadInt32();
            int eng = br.ReadInt32();
            int mat = br.ReadInt32();

            m_listScore.Add(new CScore(name, kor, eng, mat));
        }
        br.Close();
        fs.Close();
    }
}
