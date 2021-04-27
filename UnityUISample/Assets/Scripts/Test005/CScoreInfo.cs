using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CScoreInfo
{
    public class CScore
    {
        public int m_No = 0;            // 학번
        public string m_Name = "";      // 이름
        public int m_Kor = 0;
        public int m_Eng = 0;
        public int m_Mat = 0;


        public int Total { get { return m_Kor + m_Eng + m_Mat; } }


        public float Average
        {
            get { return (float)Total / 3; }
        }

        public CScore(int no, string name, int kor, int eng, int mat)
        {
            m_No = no;
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


    public CScore AddItem(int nNo, string name, int kor, int eng, int mat)
    {
        CScore kItem = new CScore(nNo, name, kor, eng, mat);
        m_listScore.Add(kItem);
        return kItem;
    }

    public void RemoveItem( string name)
    {
        CScore kScore =  m_listScore.Find((x) => { return x.m_Name == name; } );
        if (kScore != null)
            m_listScore.Remove(kScore);

    }

    public CScore Repair( int nNo, string name, int kor, int eng, int mat )
    {
        CScore kScore = FindScore(nNo);
        if (kScore == null)
            kScore = FindScore(name);

        if (kScore != null)
        {
            kScore.m_No = nNo;
            kScore.m_Name = name;
            kScore.m_Kor = kor;
            kScore.m_Eng = eng;
            kScore.m_Mat = mat;

            return kScore;
        }
        
        return null;
    }

    public CScore FindScore( int nNo )
    {
       return m_listScore.Find( x => x.m_No == nNo);
    }
    public CScore FindScore( string name)
    {
        return m_listScore.Find(x => x.m_Name.Equals(name));
    }

    public void Clear()
    {
        m_listScore.Clear();
    }

    public void SaveFile()
    {
        FileStream fs = new FileStream("Score2.dat", FileMode.Create, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs);

        bw.Write(m_listScore.Count);
        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore kScore = m_listScore[i];
            bw.Write(kScore.m_No);
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

        FileStream fs = new FileStream("Score2.dat", FileMode.Open, FileAccess.Read);
        if (fs == null) return;

        BinaryReader br = new BinaryReader(fs);
        int nCount = br.ReadInt32();
        for (int i = 0; i < nCount; i++)
        {
            int nNo = br.ReadInt32();
            string name = br.ReadString();
            int kor = br.ReadInt32();
            int eng = br.ReadInt32();
            int mat = br.ReadInt32();

            m_listScore.Add(new CScore(nNo, name, kor, eng, mat));
        }
        br.Close();
        fs.Close();
    }
}
