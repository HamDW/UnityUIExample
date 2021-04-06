using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * ��ũ�Ѻ� �����
 * 
 * 1. ��ũ�� �信 �� ������ ������Ʈ( ItemSlot )�� �����.  
 *    - ItemSlot�� ���鶧 �ݵ�� Button ������Ʈ�� �߰��Ѵ�. ( Button Ŭ������ �߰� )
 * 2. ItemSlot ������Ʈ�� ��Ʈ���� Ŭ���� (ItemSlot.cs )�� ����
 * 3. ItemSlot ������Ʈ�� ���������� �����.
 * 4. ������ ������� �� �����̸�(string)�� �ӽ÷� �����.
 * 5. List<ItemSlot>�� ItemSlot�� �����Ͽ� �����̸��� ������ŭ �߰��Ѵ�.
 * 
 */

public class ScrollViewTest2Dlg : MonoBehaviour
{
    public static string[] cCityList = { "����", "����", "����", "�λ�", "����", "�뱸", "��õ" };
    [SerializeField] ScrollRect m_ScrollRect = null;    // ��ũ�Ѻ� ������Ʈ
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

        //m_ScrollRect.onValueChanged.AddListener((Vector2 value) =>
        //{
        //    OnValueChanged_CityList(value);
        //});

        Initialize();
    }



    public void Initialize()
    {
        m_listItem.Clear();

        for (int i = 0; i < cCityList.Length; i++)
        {
            GameObject go = Instantiate(m_prefabItem, m_ScrollRect.content) as GameObject;
            ItemSlot kItem = go.GetComponent<ItemSlot>();

            kItem.transform.localScale = new Vector3(1, 1, 1);  // �ݵ�� �Ұ�
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
        for (int i = 0; i < m_listItem.Count; i++)
        {
            m_listItem[i].SetSelect(false);
        }
    }


    //public void OnValueChanged_CityList(Vector2 value)
    //{
    //    //Debug.Log("Scroll Pos = " + value.x + ", " + value.y);
    //}


    public void OnClicked_Result()
    {
        //int nPos = m_Dropdown.value;
        string sCity = cCityList[m_iSelectIndex];
        string sResult = "����� �̵��� ���ô� <color=#73F804>" + sCity + "</color> �Դϴ�. ";
        m_txtResult.text = sResult;
    }

    public void OnClicked_Clear()
    {
        m_txtResult.text = "�ʱ�ȭ �ƽ��ϴ�.";
        m_iSelectIndex = 0;
        ClearAllSelectedItem();
    }
}
