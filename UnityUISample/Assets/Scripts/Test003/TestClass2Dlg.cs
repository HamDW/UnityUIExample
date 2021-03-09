using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*

[ 문제 ]: 
4개의 이름과 HP를 입력받아 리스트에 추가하고 
공격력 80인 광역스킬을  모두에게) 발사 했을때(4마리 몬스터의 이름과 HP를 출력하세요.(몬스터 리스트 사용)

[입력]
값은 0보다 크거나 같고, 100보다 작거나 같은 정수이다.

 */


public class Monster
{
    public string m_Name = "";
    public int m_HP = 0;

    public Monster(string sName, int nHP)
    {
        m_Name = sName;
        m_HP = nHP;
    }
}

public class Master
{
    public string m_Name = "마스터";
    public int m_HP = 100;
    public int m_Attack = 80;
}


public class TestClass2Dlg : MonoBehaviour
{
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;

    [SerializeField] InputField m_editName = null;
    [SerializeField] InputField m_editHP = null;
    [SerializeField] Text m_txtInitPrint = null;
    [SerializeField] Button m_btnAdd = null;

    List<Monster> m_listMonster = new List<Monster>();

    const int DATTACK = 80;  // 공격력 80;
    Master m_Master = new Master();



    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
        m_btnAdd.onClick.AddListener(OnClicked_AddNum);
    }



    public void OnClicked_AddNum()
    {
        string sName = m_editName.text;
        int nHP = int.Parse(m_editHP.text);

        Monster kMon = new Monster(sName, nHP);
        m_listMonster.Add(kMon);

        PrintInitValue();
    }

    public void PrintInitValue()
    {
        m_txtInitPrint.text = "";
        for( int i = 0; i < m_listMonster.Count; i++)
        {
            Monster kMon = m_listMonster[i];
            m_txtInitPrint.text += string.Format("({0}:{1}),", kMon.m_Name, kMon.m_HP);
        }
    }


    public void OnClicked_OK()
    {
        m_txtResult.text = "";

        CalculateHP();
        PrintResult();
    }

    public void CalculateHP()
    {
        for( int i = 0; i < m_listMonster.Count; i++)
        {
            Monster kMon = m_listMonster[i];
            kMon.m_HP -= m_Master.m_Attack;
            if (kMon.m_HP < 0)
                kMon.m_HP = 0;
        }
    }


    public void PrintResult()
    {
        for (int i = 0; i < m_listMonster.Count; i++)
        {
            Monster kMon = m_listMonster[i];
            m_txtResult.text += string.Format("{0} Name={1}, HP={2}\n", i+1, kMon.m_Name, kMon.m_HP);
        }

    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
        m_txtInitPrint.text = "초기값";
        m_editName.text = "";
        m_editHP.text = "";
    }


}
