using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//============================================================
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
//============================================================
public class CStackBuffer<T> where T : class, new()
{

    //--------------------------------------------------------------------------
    //private CStackBufferMgr()
    //{
    //}

    object cs_buffer = new object();
    Stack<T> pool;
    int pool_capacity;

    public void initialize(int capacity)
    {
        pool = new Stack<T>();
        pool_capacity = capacity;
        allocate();
    }

    void allocate()
    {
        for (int i = 0; i < pool_capacity; ++i)
        {
            pool.Push(new T());
        }
    }

    public T pop()
    {
        lock (cs_buffer)
        {
            if (pool.Count <= 0)
            {
                Debug.Log(" CBufferMgr Reallocate.!!!");
                allocate();
            }

            return pool.Pop();
        }
    }

    public void push(T packet)
    {
        lock (cs_buffer)
        {
            pool.Push(packet);
        }
    }
}
//============================================================
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
//============================================================
public class CQueueBuffer<T> where T : class, new()
{
    object cs_buffer = new object();
    CCQueue<T> m_Pool = null;   // 환형큐
    int m_nSize = 0;

    public void Initialize(int capacity)
    {
        m_Pool = new CCQueue<T>();
        m_nSize = capacity;

        m_Pool.Initialize(m_nSize);
    }

    void Allocate()
    {
        m_nSize = m_Pool.Realocate(m_nSize);
    }

    public T Dequeue()
    {
        lock (cs_buffer)
        {
            return m_Pool.Dequeue();
        }
    }

    public void Enqueue(T node)
    {
        lock (cs_buffer)
        {
            m_Pool.Enqueue(node);
        }
    }
}

//-----------------------------------------------------------------------------------------
// 메모리 풀 클래스
// 용도 : 특정 게임오브젝트를 실시간으로 생성과 삭제하지 않고,
//      : 미리 생성해 둔 게임오브젝트를 재활용하는 클래스입니다.
//-----------------------------------------------------------------------------------------
//MonoBehaviour 상속 안받음. IEnumerable 상속시 foreach 사용 가능
//System.IDisposable 관리되지 않는 메모리(리소스)를 해제 함
public class CObjMemoryPool : IEnumerable, System.IDisposable
{
    //-------------------------------------------------------------------------------------
    // 아이템 클래스
    //-------------------------------------------------------------------------------------
    protected class Item
    {
        public bool m_bActive = false; //사용중인지 여부
        public GameObject m_gameObject = null;

        public Item(bool bActive = false, GameObject go = null)
        {
            m_bActive = bActive;
            m_gameObject = go;
        }

        public void Clear()
        {
            m_bActive = false;
            if (m_gameObject != null)
                m_gameObject.SetActive(false);
            else
                Debug.Log("_Warning_BufferMgr" + "Item.m_gameObject == null !!!!!");
        }
    }
    //====================================================================================
    public const string TAG = "MemoryPOOL";
    List<Item> table = new List<Item>();
    private object m_Lock = new object();
    //------------------------------------------------------------------------------------
    // 열거자 기본 재정의
    //------------------------------------------------------------------------------------
    public IEnumerator GetEnumerator()
    {
        if (table == null || table.Count == 0)
            yield break;

        int count = table.Count;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.m_bActive)
                yield return item.m_gameObject;
        }
    }

    //--------------------------------------------------------------------------------------
    // Item 만들기( gameObject 만들기 )
    //--------------------------------------------------------------------------------------
    protected Item MakeItem(Object original)
    {
        if (original == null)
            return null;

        Item item = new Item();
        item.m_bActive = false;
        item.m_gameObject = GameObject.Instantiate(original) as GameObject;
        item.m_gameObject.SetActive(false);
        this.table.Add(item);
        return item;
    }

    //-------------------------------------------------------------------------------------
    // 메모리 풀 생성
    // original : 미리 생성해 둘 원본소스
    // count : 풀 최고 갯수
    //-------------------------------------------------------------------------------------
    public void Create(Object original, int count)
    {
        Dispose();
        lock(m_Lock) {
            for (int i = 0; i < count; i++)
            {
                MakeItem(original);
            }
        }
    }
    //-------------------------------------------------------------------------------------
    // 새 아이템 요청 - 쉬고 있는 객체를 반납한다.
    //-------------------------------------------------------------------------------------
    protected GameObject NewItem()
    {
        if (table == null || table.Count == 0)
            return null;

        lock(m_Lock)
        {
            Item item = null;
            int count = table.Count;
            for (int i = 0; i < count; i++) {

                item = table[i];
                if (item.m_bActive == false) {
                    item.m_bActive = true;

                    if (item.m_gameObject != null){
                        item.m_gameObject.SetActive(true);
                        return item.m_gameObject;
                    }
                    else{
                        table.Remove(item);
                        --i;
                    }
                }
            }

#if _DPOOL_LOG
            string sLog = string.Format("Add Pool item....  table count = {0} ) !!!!", table.Count);
            mylog.Log(TAG, sLog);
#endif
            // 추가 아이템 생성
            if (table[0].m_gameObject != null) {

                Item kItem2 = MakeItem(this.table[0].m_gameObject);
                if (kItem2 != null) {
                    kItem2.m_bActive = true;
                    item.m_gameObject.SetActive(true);
                    return kItem2.m_gameObject;
                }
            }
            if (table[0].m_gameObject == null)
            {
                string sLog2 = string.Format("table[0].gameObject = null ( count = {0} ) !!!!", table.Count);
                Debug.Log(TAG + sLog2);
            }
        }
        return null;
    }
    //-------------------------------------------------------------------------------------
    // 새 아이템 요청 - 쉬고 있는 객체를 반납한다.
    //-------------------------------------------------------------------------------------
    public GameObject NewItem(Object original)
    {
        GameObject go = NewItem();
        if (go != null) return go;

        lock (m_Lock)
        {
            Item kItem = MakeItem(original);
            kItem.m_bActive = true;
            kItem.m_gameObject.SetActive(true);
            return kItem.m_gameObject;
        }
        //Item item = new Item();
        //item.active = false;
        //item.gameObject = GameObject.Instantiate(original) as GameObject;
        //item.gameObject.SetActive(false);
        //this.table.Add(item);
        //return NewItem();
    }

    //--------------------------------------------------------------------------------------
    // 아이템 사용종료 - 사용하던 객체를 쉬게한다.
    // gameOBject : NewItem으로 얻었던 객체
    //--------------------------------------------------------------------------------------
    public void RemoveItem(GameObject gameObject)
    {
        if (table == null || gameObject == null)
            return;

        lock (m_Lock)
        {
            int count = table.Count;

            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                if (item.m_gameObject == gameObject)
                {
                    item.Clear();
                    break;
                }
            }
        }
    }
    //--------------------------------------------------------------------------------------
    // 모든 아이템 사용종료 - 모든 객체를 쉬게한다.
    //--------------------------------------------------------------------------------------
    public void ClearItem()
    {
        if (table == null)
            return;
        int count = table.Count;

        lock (m_Lock)
        {
            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                if (item != null && item.m_bActive)
                {
                    item.Clear();
                }
            }
        }
    }
    //--------------------------------------------------------------------------------------
    // 메모리 풀 삭제
    //--------------------------------------------------------------------------------------
    public void Dispose()
    {
        if (table == null || table.Count == 0)
            return;
        int count = table.Count;
        lock (m_Lock) {
            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                GameObject.Destroy(item.m_gameObject);
            }
            table.Clear();
        }
    }
    //--------------------------------------------------------------------------------------
    // 비활성화 되어있는 아이템 메모리 해제하기
    //--------------------------------------------------------------------------------------
    public void DisposeUnactiveItems()
    {
        lock (m_Lock)
        {
            List<Item> ids = new List<Item>();
            int count = table.Count;
            for (int i = 0; i < count; i++)
            {
                Item item = table[i];
                if (!item.m_bActive){
                    ids.Add(item);
                }
            }

            count = ids.Count;
            for (int i = 0; i < count; i++)
            {
                Item item = ids[i];
                table.Remove(item);
                GameObject.Destroy(item.m_gameObject);
            }
        }
    }

}



////============================================================
///// <summary>
///// 
///// </summary>
///// <typeparam name="T"></typeparam>
////============================================================

////public class CQueueBufferObj<T> : MonoBehaviour where T : MonoBehaviour
//public class CQueueBufferObj
//{
//    GameObject m_goPrefab = null;
//    object cs_buffer = new object();

//    //---------------------------------------------------
//    private List<GameObject> m_Nodes = new List<GameObject>();
//    private int m_capacity = 0;     // 전체 용량
//    private int m_front = 0;
//    private int m_rear = 0;

//    //---------------------------------------------------
//    public int Capacity { get { return m_capacity; } }
//    public int Front { get { return m_front; } }
//    public int Rear { get { return m_rear; } }

//    //---------------------------------------------------
//    public CQueueBufferObj(int nCapacity = 0, GameObject prefab = null)
//    {
//        if (nCapacity > 0)
//            Initialize(nCapacity, prefab);
//    }

//    //---------------------------------------------------
//    // 메모리 추가 확보하기.
//    public int Realocate(int capacity)
//    {
//        m_capacity += capacity;

//        for (int i = 0; i < capacity; i++)
//        {
//            GameObject go = GameObject.Instantiate(m_goPrefab) as GameObject;
//            go.SetActive(false);
//            m_Nodes.Add( go );
//        }
//        return m_capacity;
//    }

//    //---------------------------------------------------
//    // 초기화 하기 (순환 큐 생성)
//    public void Initialize(int nCapacity, GameObject prefab)
//    {
//        m_goPrefab = prefab;
//        m_capacity = nCapacity;

//        // 배열 생성 & 메모리 할당
//        for (int i = 0; i < m_capacity + 1; i++)
//        {
//            GameObject go2 = GameObject.Instantiate(m_goPrefab) as GameObject;
//            go2.SetActive(false);
//            m_Nodes.Add(go2);
//        }
//    }

//    //---------------------------------------------------
//    // 삽입 ( 위치만 이동시킴 )
//    public GameObject Enqueue() //GameObject data)
//    {
//        int nCount = Count;

//        // 가득 차있는데 들어오면 용량을 1배 더 확보한다.
//        if (IsFull)
//        {
//            Realocate(m_capacity);
//        }

//        int position = 0;

//        // 큐의 후방(rear)이 배열을 벗어났다면
//        if (m_rear == m_capacity)
//        {
//            // 후방의 index를 0으로 초기화(순환 큐이므로 계속 돌아온다.)
//            m_rear = 1;
//            position = 0;
//        }
//        else // 그렇지 않다면 그대로 증가
//        {
//            position = m_rear;
//            m_rear++;
//        }

//        // 데이터 삽입
//        //m_Nodes[position] = data;
//        return m_Nodes[position];
//    }

//    //---------------------------------------------------
//    // 리턴 및 제거
//    public GameObject Dequeue()
//    {
//        int nCount = Count;

//        if (IsEmpty)
//        {
//            return null;
//        }

//        int position = m_front;

//        // 큐의 전방(front)이 배열 끝에 위치해있으면
//        if (m_front == m_capacity - 1)
//            m_front = 0;
//        else
//            // 그렇지 않다면 그대로 증가
//            m_front++;

//        return m_Nodes[position];
//    }

//    //---------------------------------------------------
//    // 사용 사이즈 반환
//    public int Count
//    {
//        get
//        {
//            // 전방 index가 후방 index보다 앞에 위치해 있다면
//            if (m_front <= m_rear)
//                return m_rear - m_front;
//            else
//                return (m_capacity + 1) - m_front + m_rear;
//        }
//    }

//    //---------------------------------------------------
//    // 완전히 비어있는지
//    public bool IsEmpty
//    {
//        get
//        {
//            return m_front == m_rear;
//        }
//    }
//    //---------------------------------------------------
//    // 꽉 차 있는지
//    public bool IsFull
//    {
//        get
//        {
//            // 전방 index가 후방 index보다 앞에 위치해 있다면
//            if (m_front < m_rear)
//                return (m_rear - m_front) == m_capacity;
//            else
//                return (m_rear + 1) == m_front;
//        }
//    }
//    //---------------------------------------------------
//    // 모두 파괴하기
//    public void ReleaseAll()
//    {
//        for( int i = 0;i < m_Nodes.Count; i++)
//        {
//            if (m_Nodes[i] != null)
//                GameObject.Destroy(m_Nodes[i].gameObject);
//        }
//        m_Nodes.Clear();

//        m_capacity = 0;
//        m_front = 0;
//        m_rear = 0;
//    }
//}



public class CBufferMgr
{

}
