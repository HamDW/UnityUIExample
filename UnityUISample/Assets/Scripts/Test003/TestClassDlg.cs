using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestClassDlg : MonoBehaviour
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
        TestClass();
        //TestClass2();
        //TestLogic();
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
    }

    // 클래스 멤버 변수, 멤버함수
    public void TestClass()
    {
        m_txtResult.text = "";
        Actor kMaster = new Actor();

        m_txtResult.text += string.Format("Master HP = {0}\n", kMaster.m_HP);

        kMaster.SetDamage(50);
        m_txtResult.text += string.Format("Master HP = {0}\n", kMaster.m_HP);
        m_txtResult.text += "------------- -------------------------\n";

        Actor kEnemy = new Actor();
        kEnemy.m_HP = 2000;
        kEnemy.m_Attack = 200;
        m_txtResult.text += string.Format("Enemy HP = {0}\n", kEnemy.m_HP);

        kEnemy.SetDamage(100);
        m_txtResult.text += string.Format("Enemy HP = {0}\n", kEnemy.m_HP);

        m_txtResult.text += "--------------------------------------\n";

        Actor kActor = kMaster;
        kActor.AddHP(100);
        m_txtResult.text += string.Format("Master HP = {0}\n", kActor.m_HP);

        kActor = kEnemy;
        kActor.AddHP(200);
        m_txtResult.text += string.Format("Enemy HP = {0}\n", kActor.m_HP);

        m_txtResult.text += "--------------------------------------\n";
    }

    // 상속 테스트
    public void TestClass2()
    {
        m_txtResult.text = "";

        Animal kAnimal = null;

        Dog kDog = new Dog();
        kDog.Initialize();
        kDog.PrintName();
        m_txtResult.text += string.Format("Name = {0}\n", kDog.m_Name);
        m_txtResult.text += "-------------------------------\n";

        kAnimal = kDog;
        kAnimal.PrintName();
        m_txtResult.text += string.Format("Name = {0}\n", kAnimal.m_Name);

        kAnimal.m_Name = "강아지 22";
        kDog.PrintName();
        m_txtResult.text += string.Format("Name = {0}\n", kDog.m_Name);

        kAnimal.PrintName();
        m_txtResult.text += string.Format("Name = {0}\n", kAnimal.m_Name);
        m_txtResult.text += "-------------------------------\n";

        Cat kCat = new Cat();
        kCat.Initialize();
        kCat.PrintName();
        m_txtResult.text += string.Format("Name = {0}\n", kCat.m_Name);

        kAnimal = kCat;
        kAnimal.PrintName();
        m_txtResult.text += string.Format("Name = {0}\n", kAnimal.m_Name);
        m_txtResult.text += "-------------------------------\n";
    }


    // 이진 논리 연산자
    public void TestLogic()
    {
        m_txtResult.text = "";
        // 첫번째 : 부호비트
        // 실제 : 31비트 사용
        // 0000 0000 0000 0000 0000 0000 0000 0000 

        int a = 11;     // =  1011   = 0xb
        int b = 9;      // =  1001   = 0x9

        int c = a & b;    // and       = 1001
        int d = a | b;    // or        = 1011
        int e = ~a;       // not       = -(a+1)        
        int f = a ^ b;    // xor       = 0010
        int g = b >> 2;   // 우측으로 2비트 이동      1001 -> 0010    
        int h = b << 2;   // 좌측으로 2비트 이동      1001 -> 100100  

        string sLog = string.Format(
            " a = {0}\n b = {1}\n a&b = {2}\n a|b = {3}\n ~a = {4}\n a^b = {5}\n b>>2 = {6}\n b<<2 = {7}", 
            a, b, c, d, e, f, g, h);
        Debug.Log(sLog);

        m_txtResult.text += sLog;
    }
}
