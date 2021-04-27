using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CListViewPool : MonoBehaviour
{
    public int m_maxPool = 6;

    public List<GameObject> m_Pools = new List<GameObject>();
    public GameObject m_prefabItem = null;
    private ScrollRect m_ScrollRect = null;

    private Vector2 m_vScrollPos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        m_ScrollRect = GetComponent<ScrollRect>();
        m_ScrollRect.onValueChanged.AddListener(OnValueChanged_Scroll);

        Initialize_Pool();
    }

    public void Initialize_Pool()
    {
        // 메모리 풀 확보하기
        for( int i = 0; i < m_maxPool; i++)
        {
            GameObject go = Instantiate<GameObject>(m_prefabItem, m_ScrollRect.content);
            go.name = i.ToString();
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(false);
            m_Pools.Add(go);
        }
    }

    public GameObject CreateItem()
    {
        GameObject go = FindUsableItem();
        if (go != null)
        {
            go.SetActive(true);
            return go;
        }
        return null;
    }

    public float GetItemListHeight()
    {
        float fH = 0;
        for( int i = 0; i < m_Pools.Count; i++ )
        {
            RectTransform rt = m_Pools[i].GetComponent<RectTransform>();
            fH += rt.rect.height;
        }
        return fH;
    }

    public float GetItemHeight(GameObject go)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        return rt.rect.height;
    }

    public GameObject FindUsableItem()
    {
        return m_Pools.Find(x => x.activeSelf == false);
    }

    public void RemoveItem(GameObject go)
    {
        go.SetActive(false);
    }

    private void OnValueChanged_Scroll(Vector2 vPos)
    {
        float fMax = GetItemListHeight();
        float fH = GetItemHeight(m_Pools[0]);

        float pos = vPos.y - m_vScrollPos.y;

        bool bUP = false;

        if (pos > 0)
            bUP = true;


        if( Mathf.Abs(pos) > (fH / fMax) * 0.5f  )   // 아이템 높이의 반절이 넘을 경우
        {
            if (bUP)
                MoveUp_Item();
            else
                MoveDown_Item();
        }
        m_vScrollPos = vPos;
    }


    // 0 -> Last로
    public void MoveUp_Item()
    {
        GameObject goFirst = m_Pools[0];
        GameObject goLast = m_Pools[m_Pools.Count - 1];

        Vector3 vPos1 = goFirst.transform.localPosition;
        Vector3 vPos2 = goLast.transform.localPosition;

        vPos1.y = vPos2.y + GetItemHeight(goLast);
        goFirst.transform.localPosition = vPos1;
    }

    // Last --> 0 으로
    public void MoveDown_Item()
    {
        GameObject goFirst = m_Pools[0];
        GameObject goLast = m_Pools[m_Pools.Count - 1];

        Vector3 vPos1 = goFirst.transform.localPosition;
        Vector3 vPos2 = goLast.transform.localPosition;

        vPos2.y = vPos1.y - GetItemHeight(goFirst);
        goLast.transform.localPosition = vPos2;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
