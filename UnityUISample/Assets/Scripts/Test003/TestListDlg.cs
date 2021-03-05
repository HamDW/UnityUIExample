using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestListDlg : MonoBehaviour
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
        
        //TestArray();
        //TestArray2();
        //TestArray3();
        TestArray5();
        //TestList();
        //TestList2();
        //TestDictionary();
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
    }

    public void TestArray()
    {
        m_txtResult.text = "";

        int[] aTemp = new int[3];
        aTemp[0] = 100;
        aTemp[1] = 200;
        aTemp[2] = 300;

        for (int i = 0; i < aTemp.Length; i++)
        {
            string sLog = string.Format("Temp[{0}] = {1}", i, aTemp[i]);
            Debug.Log(sLog);
            m_txtResult.text += sLog + "\n";
        }
        m_txtResult.text += "--------------------------------------\n\n";
    }

    // while() 테스트 
    public void TestArray2()
    {
        int[] aTemp = new int[3];
        aTemp[0] = 100;
        aTemp[1] = 200;
        aTemp[2] = 300;

        int nIndex = 0;
        while (nIndex < aTemp.Length)
        {
            string sLog = string.Format("Temp[{0}] = {1}", nIndex, aTemp[nIndex]);
            Debug.Log(sLog);
            m_txtResult.text += sLog + "\n";
            nIndex++;
        }
        m_txtResult.text += "---------------------------\n\n";
    }

    // do ~ while() 테스트 
    public void TestArray3()
    {
        int[] aTemp = new int[3];
        aTemp[0] = 100;
        aTemp[1] = 200;
        aTemp[2] = 300;
        int nIndex = 0;
        do
        {
            string sLog = string.Format("Temp[{0}] = {1}", nIndex, aTemp[nIndex]);
            Debug.Log(sLog);
            m_txtResult.text += sLog + "\n";
            nIndex++;
        }
        while (nIndex < aTemp.Length);

        m_txtResult.text += "--------------------------------------\n\n";
    }

    // 2차원 배열 
    public void TestArray5()
    {
        int[,] array1 = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } };

        PrintArray(array1);

        int[,] array = new int[3, 2];
        array[0, 0] = 1;
        array[0, 1] = 2;
        array[1, 0] = 3;
        array[1, 1] = 4;
        array[2, 0] = 5;
        array[2, 1] = 6;

        PrintArray(array);

        //string[,] sNames = new string[2, 3]
        //{
        //         {  "서신동",  "김말자", "고도화" },
        //         {  "오감자",  "박사과", "한과자" }
        //};

        //PrintArray(sNames);
    }


    private void PrintArray(int[,] arr)
    {
        int count =  arr.Length;
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                m_txtResult.text += string.Format("array[{0},{1}]={2} \n", i, j, arr[i, j]);
            }
        }
        m_txtResult.text += "---------------------------------------\n";
    }
    private void PrintArray(string[,] arr)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                m_txtResult.text += string.Format("str[{0},{1}]={2} \n", i, j, arr[i, j]);
            }
        }
        m_txtResult.text += "---------------------------------------\n";
    }


    // 리스트 테스트
    public void TestList()
    {
        List<int> list = new List<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        for (int i = 0; i < list.Count; i++)
        {
            string sLog = string.Format("list[{0}] = {1}", i, list[i]);
            Debug.Log(sLog);
            m_txtResult.text += sLog + "\n";
        }
        m_txtResult.text += "--------------------------------------\n\n";
    }

    // foreach 테스트
    public void TestList2()
    {
        List<int> list = new List<int>();
        list.Add(10);
        list.Add(20);
        list.Add(30);

        int i = 0;
        foreach (int value in list)
        {
            string sLog = string.Format("list[{0}] = {1}", i, value);
            Debug.Log(sLog);
            m_txtResult.text += sLog + "\n";
            i++;
        }
        m_txtResult.text += "--------------------------------------\n\n";
    }

    // Dictionary
    public void TestDictionary()
    {
        Dictionary<int, string> dic = new Dictionary<int, string>();

        dic.Add(1, "사과");
        dic.Add(2, "배");
        dic.Add(3, "수박");

        foreach (KeyValuePair<int, string> item in dic)
        {
            int key = item.Key;
            string value = item.Value;

            string sLog = string.Format("dic[{0}] = {1}", key, value);
            Debug.Log(sLog);
            m_txtResult.text += sLog + "\n";
        }

        m_txtResult.text += "--------------------------------------\n\n";

        dic[1] = "맛있는 사과";
        dic[2] = "맛있는 배";

        Debug.Log(dic[1]);
        Debug.Log(dic[2]);

        m_txtResult.text += dic[1] + "\n";
        m_txtResult.text += dic[2] + "\n";

        m_txtResult.text += "--------------------------------------\n\n";

        // 삭제
        dic.Remove(1);
        Debug.Log("Count = " + dic.Count);

        foreach (KeyValuePair<int, string> item in dic)
        {
            int key = item.Key;
            string value = item.Value;

            string sLog = string.Format("dic[{0}] = {1}", key, value);
            Debug.Log(sLog);
            m_txtResult.text += sLog + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
