using UnityEngine;
using System.Collections;


//public enum Options
//{
//    OPT_NONE = 0x00,    //없음.
 
//    OPT_A = 0x01, // 1       
//    OPT_B = 0x02, // 2
//    OPT_C = 0x04, // 4
//    OPT_D = 0x08, // 8
//    OPT_E = 0x10, // 16
//    OPT_F = 0x20, // 32 
//    OPT_G = 0x40, // 64
//    OPT_H = 0x80, // 128
//};

/*
 * 
 *  비트 플래그를 사용하기위한 클래스
 * 
 * */

public class CBitFlag {

    protected long m_lBits = 0;

    public CBitFlag() {
        
    }

    // 필요한 비트를 저장(추가)한다.
    public void AddBits( long lBit )
    {
        m_lBits |= lBit;
    }

    // bits 전체를 셋팅함..( 사용을 자제할것. )
    public void SetBits(long lBits)
    {
        m_lBits = lBits;
    }
    // bits 전체를 얻는다.
    public long GetBits()
    { 
        return m_lBits;  
    }

    // lBit가 존재하는지 체크하기. 
    public bool IsBits(long lBit)
    {
        long res = ( m_lBits & lBit );
        return res != 0 ? true: false;
    }

    // bit를 초기화 한다.
    public void ClearBits()
    {
        m_lBits = 0;
    }

    // value = true : 추가
    // value = false : 삭제
    public void SetBit(long lBit, bool value)
    {
        if (value)
            m_lBits |= lBit;
        else
            m_lBits &= ~lBit;
    }

}
