using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * 스크롤뷰 만들기
 * 
 * 1. 스크롤 뷰에 들어갈 아이템 오브젝트( ItemSlot )를 만든다.  
 *    - ItemSlot을 만들때 반드시 Button 컴포넌트를 추가한다. ( Button 클래스만 추가 )
 * 2. ItemSlot 오브젝트를 컨트롤할 클래스 (ItemSlot.cs )를 생성
 * 3. ItemSlot 오브젝트를 프리팹으로 만든다.
 * 4. 아이템 목록으로 들어갈 도시이름(string)을 임시로 만든다.
 * 5. List<ItemSlot>에 ItemSlot을 생성하여 도시이름들 갯수만큼 추가한다.
 * 
 */

public class ScrollViewTestDlg : MonoBehaviour
{
    public static string[] cAnimalList = { "토끼", "호랑이", "사자", "강아지", "고양이", "너구리", "개구리" };
    
    [SerializeField] ScrollRect m_ScrollRect = null;    // 스크롤뷰 컴포넌트
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Button m_btnClear = null;

    [SerializeField] GameObject m_prefabItem = null;

    private List<ItemText> m_listItem = new List<ItemText>();

    private int m_iSelectIndex = 0;


    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
        Initialize();
    }


    public void Initialize()
    {
        m_listItem.Clear();

        for( int i = 0; i < cAnimalList.Length; i++ )
        {
            ItemText kItem = CreateItem(i);

            Button btn = kItem.GetComponent<Button>();
            int idx = i;
            btn.onClick.AddListener(() => {
                OnClicked_SelectItem(idx); 
            });

            m_listItem.Add(kItem);
        }
    }

    public ItemText CreateItem( int idx )
    {
        GameObject go = Instantiate(m_prefabItem, m_ScrollRect.content);
        ItemText kItem = go.GetComponent<ItemText>();

        kItem.transform.localScale = new Vector3(1, 1, 1);  // 반드시 할것
        kItem.Initialize(idx, cAnimalList[idx]);

        return kItem;
    }

    public void OnClicked_SelectItem(int iIndex)
    {
        ClearAllSelectedItem();
        ItemText kItem = m_listItem[iIndex];
        kItem.SetSelect(true);

        m_iSelectIndex = iIndex;
        m_txtResult.text = cAnimalList[iIndex];

        Debug.LogFormat(" Select Index = {0}", iIndex);
    }

    public void ClearAllSelectedItem()
    {
        for( int i = 0; i < m_listItem.Count; i++ )
        {
            m_listItem[i].SetSelect(false);
        }
    }


    public void OnClicked_Result()
    {
        string sCity = cAnimalList[m_iSelectIndex];
        string sResult = "당신이 선택한 동물은 <color=#73F804>" + sCity + "</color> 입니다. ";
        m_txtResult.text = sResult;
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "초기화 됐습니다.";
        m_iSelectIndex = 0;
        ClearAllSelectedItem();
    }


    //// value : 가로 세로 스크롤 position ( 0 ~ 1 )값
    //public void OnValueChanged_CityList(Vector2 value)
    //{
    //    Debug.Log("Scroll Pos = " + value.x + ", " + value.y);
    //}

}
