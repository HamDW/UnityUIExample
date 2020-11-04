using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 [ 원형큐 ]

 ○ 리스트 기반의 큐가 원형으로 이루어진 상태.
 
 ○ 원형으로 이루어져 있기 때문에 큐가 가득 찼을때나 완전히 비어있을때 Front와 Rear의 index는 동일하므로
   Empty인지 Full인지 구분할 수 없다.

 ○ 그러므로 원형큐를 생성할때는 더미(Dummy) 공간을 생성하여 원래 용량(capacity)보다 +1 만큼 증가시켜 배열을 생성.
     예) 큐의 사이즈 : 9   =>  배열 사이즈 : 10
 
 ○ 비어있는 상태 : 'Front == Rear'  
 ○ 가득찬 상태   : 'Rear + 1 == Front'  
 
*/

public class CCQueue<T> where T : class, new()  {
    
    //---------------------------------------------------
    private  List<T> m_Nodes = new List<T>();
    private int m_capacity = 0;     // 전체 용량
    private int m_front = 0;
    private int m_rear = 0;

    //---------------------------------------------------
    public int Capacity { get { return m_capacity; } }
    public int Front { get { return m_front; } }
    public int Rear { get { return m_rear; } }

    //---------------------------------------------------
    public CCQueue(int nCapacity=0)
    {
        if( nCapacity > 0)
            Initialize(nCapacity);
    }

    //---------------------------------------------------
    // 메모리 추가 확보하기.
    public int Realocate(int capacity )
    {
        m_capacity += capacity;

        for (int i = 0; i < capacity; i++)
        {
            m_Nodes.Add(new T());
        }
        return m_capacity;
    }

    //---------------------------------------------------
    // 초기화 하기 (순환 큐 생성)
    public void Initialize(int nCapacity)
    {
        m_capacity = nCapacity;

        // 배열 생성 & 메모리 할당
        for (int i = 0; i < m_capacity+1; i++)
        {
            m_Nodes.Add(new T());
        }
    }

    //---------------------------------------------------
    // 삽입
    public void Enqueue(T data)
    {
        int nCount = Count;

        // 가득 차있는데 들어오면 용량을 1배 더 확보한다.
        if (IsFull)
        {
            Realocate(m_capacity);
        }

        int position = 0;

        // 큐의 후방(rear)이 배열을 벗어났다면
        if( m_rear == m_capacity )
        {
            // 후방의 index를 0으로 초기화(순환 큐이므로 계속 돌아온다.)
            m_rear = 1;
            position = 0;
        }
        else // 그렇지 않다면 그대로 증가
        {
            position = m_rear;
            m_rear++;
        }

        // 데이터 삽입
        m_Nodes[position] = data;
    }

    //---------------------------------------------------
    // 리턴 및 제거
    public T Dequeue()
    {
        int nCount = Count;

        if ( IsEmpty )
        {
            return null;
        }

        int position = m_front;

        // 큐의 전방(front)이 배열 끝에 위치해있으면
        if (m_front == m_capacity-1)
            m_front = 0;
        else
            // 그렇지 않다면 그대로 증가
            m_front++;

        return m_Nodes[position];
    }

    //---------------------------------------------------
    // 사용 사이즈 반환
    public int Count
    {
        get
        {
            // 전방 index가 후방 index보다 앞에 위치해 있다면
            if (m_front <= m_rear)
                return m_rear - m_front;
            else
                return (m_capacity+1) - m_front + m_rear;
        }
    }

    //---------------------------------------------------
    // 완전히 비어있는지
    public bool IsEmpty
    {
        get
        {
            return m_front == m_rear;
        }
    }
    //---------------------------------------------------
    // 꽉 차 있는지
    public bool IsFull
    {
        get
        {
            // 전방 index가 후방 index보다 앞에 위치해 있다면
            if (m_front < m_rear)
                return (m_rear - m_front) == m_capacity;
            else
                return (m_rear + 1) == m_front;
        }
    }

}
