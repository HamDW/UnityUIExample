using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * 
 *  상속 연습하기
 * 
 * 
 */


public class Actor 
{
    public int m_HP = 0;
    
    // 생성자
    public Actor()  {

    }

    public virtual void Initialize() 
    {
        m_HP = 100;
    }

    public virtual void SetDamage(int nAttack)   {   }
}


public class Master : Actor
{
    public int m_MP;

    // 생성자
    public Master() {  }

    public override void Initialize()
    {
        base.Initialize();
        m_MP = 100;
    }
    public override void SetDamage(int nAttack)
    {
        m_HP -= nAttack;
    }


}



public class Monster : Actor
{
    public override void Initialize()
    {
        base.Initialize();

    }
    public override void SetDamage(int nAttack)
    {
        m_HP -= nAttack;
    }
}


public class Boss : Actor
{
    public override void Initialize()
    {
        base.Initialize();

    }

    // 보스는 데미지를 0.5배만 입는다고 가정할때
    public override void SetDamage(int nAttack)
    {
        m_HP -= (int)(nAttack * 0.5f);
    }

}



























public class TestActor  
{

}
