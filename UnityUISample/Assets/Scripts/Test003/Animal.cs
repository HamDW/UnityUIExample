using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
* 상속 가상함수 테스트
*   virtual -- override
*/
public class Animal
{
    public string m_Name = "";

    public Animal()
    {
        m_Name = "동물";
    }

    public virtual string GetSound()
    {
        return "소리";
    }

}
public class Dog : Animal
{
    public Dog()
    {
        m_Name = "강아지";
    }

    public override string GetSound()
    {
        return "멍멍";
    }

}
public class Cat : Animal
{
    public Cat()
    {
        m_Name = "고양이";
    }

    public override string GetSound()
    {
        return "야옹";
    }
}



//============================================================================================
//============================================================================================
/*
    * 상속 생성자 테스트
    */
public class Animal2
{
    public int m_Type = 0;
    public string m_Name = "";

    public void PrintName()
    {
        Debug.Log("Name = " + m_Name);
    }

    public Animal2()
    {
        m_Type = 0;
        m_Name = "동물";
    }

    public Animal2(string name)
    {
        m_Type = 0;
        m_Name = name;
    }
}
public class Dog2 : Animal2
{
    public Dog2() : base()
    {
        m_Type = 1;
        m_Name = "강아지";
    }

    public Dog2(string name) : base(name)
    {
        m_Type = 1;
    }

}
public class Cat2 : Animal2
{
    public Cat2() : base()
    {
        m_Type = 2;
        m_Name = "고양이";
    }

    public Cat2(string name) : base(name)
    {
        m_Type = 2;
    }

    // 생성자 오버로드를 아래와 같이 사용해도 된다.
    //public Cat2(string name)
    //{
    //    m_Type = 2;
    //    m_Name = name;
    //}

}

