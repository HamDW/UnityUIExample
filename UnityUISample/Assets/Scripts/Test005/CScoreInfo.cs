using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CScoreInfo
{
    public class CScore
    {
        public string m_Name = "";
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


    //-------------------------------------------
    public List<CScore> m_listScore = new List<CScore>();

    public void Initialize()
    {

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
