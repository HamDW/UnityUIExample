using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
     *  클래스 테스트 
     * 
     *  1. 멤버 변수
     *  2. 멤버 함수
     *  3. 기본 생성자
     * 
     */

public class Actor
{
    // 멤버 변수
    public int m_HP = 0;           // 체력
    public int m_Attack = 0;       // 공격력

    // 기본 생성자
    public Actor()
    {
        m_HP = 5000;
        m_Attack = 100;
    }

    // 멤버 함수
    public void SetDamage(int nDamage)
    {
        m_HP -= nDamage;
    }

    public void AddHP(int nValue)
    {
        m_HP += nValue;
    }

    // 생성자 오버로드
    //public Actor(int hp, int attack)
    //{
    //    m_HP = hp;
    //    m_Attack = attack;
    //}
}


/*
 *  클래스 테스트 
 * 
 *  1. 멤버 변수
 *  2. 멤버 함수
 *  3. 기본 생성자
 *  4. 생성자 오버로드
 *  5. 프로퍼티
 *  6. 접근자 ( public, protected, private )
 * 
 */

public class Actor2
{
    // 멤버 변수
    private int m_HP = 0;           // 체력
    private int m_Attack = 0;       // 공격력


    // 기본 생성자
    public Actor2()
    {
        m_HP = 5000;
        m_Attack = 100;
    }
    // 생성자 오버로드
    public Actor2(int nHp, int nAttack)
    {
        m_HP = nHp;
        m_Attack = nAttack;
    }

    // 프로퍼티 ( 속성 )
    public int hp
    {
        get { return m_HP; }
        set { m_HP = value; }
    }

    // 프로퍼티 ( 속성 )
    public int attack
    {
        get { return m_Attack; }
        set { m_Attack = value; }
    }


    // 멤버 함수
    public void SetDamage(int nDamage)
    {
        m_HP -= nDamage;
    }

    public void AddHP(int nValue)
    {
        m_HP += nValue;
    }

}





public class TestActor  
{

}
