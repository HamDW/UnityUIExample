using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CScoreInfo
{
    public class CScore
    {
        public int m_idx = 0;
        public string m_Name = "";
        public int m_Kor = 0;
        public int m_Eng = 0;
        public int m_Mat = 0;


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
        public CScore(int idx, string name, int kor, int eng, int mat)
        {
            m_idx = idx;
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


    public CScore AddItem( string name, int kor, int eng, int mat)
    {
        CScore kItem = new CScore(name, kor, eng, mat);
        kItem.m_idx = m_listScore.Count+1;
        m_listScore.Add(kItem);
        return kItem;
    }

    public void RemoveItem( string name)
    {
        CScore kScore =  m_listScore.Find((x) => { return x.m_Name == name; } );
        if (kScore != null)
            m_listScore.Remove(kScore);

        //for( int i = 0; i < m_listScore.Count; i++)
        //{
        //    if( m_listScore[i].m_Name.Equals(name))
        //    {
        //        m_listScore.RemoveAt(i);
        //        break;
        //    }
        //}
    }

    public CScore Repair( int idx, string name, int kor, int eng, int mat )
    {
        CScore kScore = GetScore(idx);
        if (kScore != null)
        {
            kScore.m_Name = name;
            kScore.m_Kor = kor;
            kScore.m_Eng = eng;
            kScore.m_Mat = mat;

            return kScore;
        }
        return null;
    }

    public CScore GetScore( int idx )
    {
        if( idx > 0 && idx <= m_listScore.Count)
        {
            return m_listScore[idx - 1];
        }
        return null;
    }


    public void SaveFile()
    {
        FileStream fs = new FileStream("Score2.dat", FileMode.Create, FileAccess.Write);
        BinaryWriter bw = new BinaryWriter(fs);

        bw.Write(m_listScore.Count);
        for (int i = 0; i < m_listScore.Count; i++)
        {
            CScore kScore = m_listScore[i];
            bw.Write(kScore.m_idx);
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
            int idx = br.ReadInt32();
            string name = br.ReadString();
            int kor = br.ReadInt32();
            int eng = br.ReadInt32();
            int mat = br.ReadInt32();

            m_listScore.Add(new CScore(idx, name, kor, eng, mat));
        }
        br.Close();
        fs.Close();
    }
}
