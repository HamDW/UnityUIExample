using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*
 *  수동으로 리사이클러뷰와 같은 스크롤뷰 만들기
 *    - 작업 완료 안됨 ( 더 작업해야 함 )
 *    - 동작 안됨.
 */
public class CTestScoreUI : MonoBehaviour
{
    [SerializeField] InputField m_InputNo = null;
    [SerializeField] InputField m_InputName = null;
    [SerializeField] InputField m_InputKor = null;
    [SerializeField] InputField m_InputEng = null;
    [SerializeField] InputField m_InputMath = null;

    [SerializeField] Button m_btnAdd = null;
    [SerializeField] Button m_btnRepair = null;
    [SerializeField] Button m_btnDelete = null;

    [SerializeField] Button m_btnSave = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] Button m_btnLoad = null;

    [SerializeField] ScrollRect m_ScrollRect = null;
    [SerializeField] GameObject m_prefabItem = null;
    public CListViewPool m_ListView = null;


    [HideInInspector] public List<CScoreItemUI> m_listItem = new List<CScoreItemUI>();
    private CScoreInfo m_kScoreInfo = new CScoreInfo();
    private CScoreItemUI m_kCurItemUI = null;


    void Start()
    {
        m_btnSave.onClick.AddListener(OnClicked_Save);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
        m_btnLoad.onClick.AddListener(OnClicked_Load);

        m_btnAdd.onClick.AddListener(OnClicked_Add);
        m_btnRepair.onClick.AddListener(OnClicked_Repair);
        m_btnDelete.onClick.AddListener(OnClicked_Delete);
    }

    public void ClearInput()
    {
        m_InputNo.text = "";
        m_InputName.text = "";
        m_InputKor.text = "";
        m_InputMath.text = "";
        m_InputEng.text = "";
    }

    public void OnClicked_Add()
    {
        int nNo = int.Parse(m_InputNo.text);
        string sName = m_InputName.text;
        int nKor = int.Parse(m_InputKor.text);
        int nMath = int.Parse(m_InputMath.text);
        int nEng = int.Parse(m_InputEng.text);

        CScoreInfo.CScore kScore = m_kScoreInfo.AddItem(nNo, sName, nKor, nEng, nMath);
        CScoreItemUI kItemUI = CreateItemUI(kScore);
        m_listItem.Add(kItemUI);
    }

    public CScoreItemUI CreateItemUI(CScoreInfo.CScore kScore)
    {
        GameObject go = m_ListView.CreateItem();

        if (go != null)
        {
            CScoreItemUI kItemUI = go.GetComponent<CScoreItemUI>();
            kItemUI.Initialize(kScore);

            Button btn = go.GetComponent<Button>();
            btn.onClick.AddListener(() => OnSelectedItem(go, kScore));
            return kItemUI;
        }
        return null;
    }


    public void OnSelectedItem(GameObject go, CScoreInfo.CScore kScore)
    {
        if (m_kCurItemUI != null)
            m_kCurItemUI.SetSelectd(false);

        m_InputNo.text = kScore.m_No.ToString();
        m_InputName.text = kScore.m_Name;
        m_InputKor.text = kScore.m_Kor.ToString();
        m_InputMath.text = kScore.m_Mat.ToString();
        m_InputEng.text = kScore.m_Eng.ToString();

        m_kCurItemUI = go.GetComponent<CScoreItemUI>();
        m_kCurItemUI.SetSelectd(true);
    }


    public void OnClicked_Repair()
    {
        int nNo = int.Parse(m_InputNo.text);
        string sName = m_InputName.text;
        int nKor = int.Parse(m_InputKor.text);
        int nMath = int.Parse(m_InputMath.text);
        int nEng = int.Parse(m_InputEng.text);

        CScoreInfo.CScore kScore = m_kScoreInfo.Repair(nNo, sName, nKor, nEng, nMath);
        if (m_kCurItemUI != null)
        {
            m_kCurItemUI.Initialize(kScore);
        }
    }

    public void OnClicked_Delete()
    {
        //CScoreInfo.CScore kScore = m_kScoreInfo.FindScore(m_kCurItemUI.GetNumber());
        //if (kScore != null)
        //{
        //    m_kScoreInfo.RemoveItem(kScore.m_Name);
        //    m_listItem.Remove(m_kCurItemUI);

        //    Destroy(m_kCurItemUI.gameObject);
        //    m_kCurItemUI = null;
        //}
    }

    public void OnClicked_Save()
    {
        m_kScoreInfo.SaveFile();
    }

    public void OnClicked_Clear()
    {
        //for (int i = 0; i < m_listItem.Count; i++)
        //{
        //    Destroy(m_listItem[i].gameObject);
        //}

        //m_listItem.Clear();
        //m_kScoreInfo.Clear();
        //m_kCurItemUI = null;
        //ClearInput();
    }

    public void OnClicked_Load()
    {
        m_kScoreInfo.LoadFile();

        m_listItem.Clear();
        for (int i = 0; i < m_kScoreInfo.m_listScore.Count; i++)
        {
            CScoreInfo.CScore kScore = m_kScoreInfo.m_listScore[i];
            CScoreItemUI kItemUI = CreateItemUI(kScore);
            if( kItemUI != null )
                m_listItem.Add(kItemUI);
        }
    }


}
