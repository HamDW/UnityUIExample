using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ScrollViewTestDlg : MonoBehaviour
{
    // Start is called before the first frame update
    public static string[] cCityList = { "서울", "광주", "대전", "부산", "전주", "대구", "인천" };
    [SerializeField] ScrollRect m_ScrollRect = null;
    [SerializeField] Text m_txtResult = null;
    [SerializeField] Button m_btnResult = null;
    [SerializeField] Button m_btnClear = null;

    [SerializeField] GameObject m_prefabItem = null;

    private List<ItemSlot> m_listItem = new List<ItemSlot>();

    private int m_iSelectIndex = 0;


    void Start()
    {
        m_btnResult.onClick.AddListener(OnClicked_Result);
        m_btnClear.onClick.AddListener(OnClicked_Clear);

        m_ScrollRect.onValueChanged.AddListener( (Vector2 value) =>{
            OnValueChanged_CityList(value);
        });


        Initialize();
    }



    public void Initialize()
    {
        m_listItem.Clear();

        for( int i = 0; i < cCityList.Length; i++ )
        {
            GameObject go = Instantiate(m_prefabItem, m_ScrollRect.content) as GameObject;
            ItemSlot kItem = go.GetComponent<ItemSlot>();

            kItem.transform.localScale = new Vector3(1, 1, 1);  // 반드시 할것
            kItem.Initialize(i, cCityList[i]);
            
            Button btn = kItem.GetComponent<Button>();
            int idx = i;
            btn.onClick.AddListener(() => {
                OnClicked_SelectItem(idx);
            });

            m_listItem.Add(kItem);
        }
        
    }

    public void OnClicked_SelectItem(int iIndex)
    {
        ClearAllSelectedItem();
        ItemSlot kItem = m_listItem[iIndex];
        kItem.SetSelect(true);

        m_iSelectIndex = iIndex;

        m_txtResult.text = cCityList[iIndex];
        
        string sLog = string.Format(" Select Index = {0}", iIndex);
        Debug.Log(sLog);
    }

    public void ClearAllSelectedItem()
    {
        for( int i = 0; i < m_listItem.Count; i++ )
        {
            m_listItem[i].SetSelect(false);
        }
    }



    public void OnValueChanged_CityList(Vector2 value)
    {
        //Debug.Log("Scroll Pos = " + value.x + ", " + value.y);
    }



    public void OnClicked_Result()
    {
        //int nPos = m_Dropdown.value;
        string sCity = cCityList[m_iSelectIndex];
        string sResult = "당신이 이동할 도시는 <color=#aa00ffff>" + sCity + "</color> 입니다. ";
        m_txtResult.text = sResult;
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "초기화 됐습니다.";
        m_iSelectIndex = 0;
    }
}
