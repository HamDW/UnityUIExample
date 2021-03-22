using System.Collections;
using System.Linq;
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
    public Monster( Monster k )
    {
        m_Name = k.m_Name;
        m_HP = k.m_HP;
    }

    public void Set(string sName, int nHP)
    {
        m_Name = sName;
        m_HP = nHP;
    }
    public void Set(Monster k)
    {
        m_Name = k.m_Name;
        m_HP = k.m_HP;
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

        //CalculateHP();
        OrderByAscending();
        //OrderBy_Test();
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


    public void OrderByAscending()
    {
        for (int i = 0; i < m_listMonster.Count-1; i++)
        {
            Monster k1 = m_listMonster[i];
            for( int j = i; j < m_listMonster.Count; j++)
            {
                Monster k2 = m_listMonster[j];

                if ( k1.m_HP > k2.m_HP )
                    Swap(k1, k2);
            }
        }
    }

    public void OrderBy_Test()
    {
        List<Monster> SortedList = m_listMonster.OrderBy(x => x.m_HP).ToList();

        for (int i = 0; i < SortedList.Count; i++)
        {
            Monster kMon = SortedList[i];
            m_txtResult.text += string.Format("{0} Name={1}, HP={2}\n", i + 1, kMon.m_Name, kMon.m_HP);
        }
    }


    // 정렬 관련 함수들
    public void OrderBy_Sort()
    {
        // 반환값없는 자체 정렬하기
        m_listMonster.Sort((x1, x2) => {
            return x1.m_HP.CompareTo(x2.m_HP);
        });

        m_listMonster.Sort((x1, x2) => x1.m_HP.CompareTo(x2.m_HP) );

        // HP 순 오름차순 정렬하기
        List<Monster> SortedList = m_listMonster.OrderBy(x => x.m_HP).ToList();

        // HP 순 내림차순 정렬하기
        List<Monster> SortedList2 = m_listMonster.OrderByDescending(x => x.m_HP).ToList();

        // HP 순 이후에 Name 순으로 정렬하기
        List<Monster> SortedList3 = m_listMonster.OrderBy(x => x.m_HP).ThenBy(x => x.m_Name).ToList();
    }


    public void OrderByDescending2()
    {
        m_listMonster.Sort((x1, x2) => x1.m_HP.CompareTo(x2.m_HP));
    }


    public void Swap(Monster a, Monster b)
    {
        Monster c = new Monster(a);
        a.Set(b);
        b.Set(c);
    }





}
