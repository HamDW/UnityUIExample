using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Score083Dlg : MonoBehaviour
{
    public class CScore
    {
        public string m_Name;
        public int m_Kor;
        public int m_Eng;
        public int m_Mat;

        public int Total()
        {
            return m_Kor + m_Eng + m_Mat;
        }

        public float Average()
        {
            return (float)Total() / 3;
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
        PrintResult();
        SaveFile();
    }

    public void PrintResult()
    {
        m_txtResult.text = "";
        m_txtResult.text += "[성적관리]\n";
        m_txtResult.text += "===================================\n";

        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore sr = m_listScore[i];
            m_txtResult.text += string.Format("{0} : {1}, {2}, {3} : 합계={4}\t 평균={5:0.0} \n",
                                sr.m_Name, sr.m_Kor, sr.m_Eng, sr.m_Mat, sr.Total(), sr.Average());
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
        int nSumK = 0, nSumE =0, nSumM = 0;

        for ( int i = 0; i < m_listScore.Count; i++)
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
