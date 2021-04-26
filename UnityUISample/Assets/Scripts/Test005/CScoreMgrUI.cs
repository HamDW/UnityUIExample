using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CScoreMgrUI : MonoBehaviour
{
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

    [HideInInspector] public List<CScoreItemUI> m_listItem = new List<CScoreItemUI>();
    private CScoreInfo m_kScoreInfo = new CScoreInfo();

    private int m_iSelect = -1;

    // Start is called before the first frame update
    void Start()
    {
        m_btnSave.onClick.AddListener(OnClicked_Save);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
        m_btnLoad.onClick.AddListener(OnClicked_Load);


        m_btnAdd.onClick.AddListener(OnClicked_Add);
        m_btnRepair.onClick.AddListener(OnClicked_Repair);
        m_btnDelete.onClick.AddListener(OnClicked_Delete);

    }


    public void Initialize()
    {

    }


    public void OnClicked_Add()
    {
        string sName =  m_InputName.text;
        int nKor = int.Parse(m_InputKor.text);
        int nMath = int.Parse(m_InputMath.text);
        int nEng = int.Parse(m_InputEng.text);

        CScoreInfo.CScore kScore = m_kScoreInfo.AddItem(sName, nKor, nEng, nMath);
        CScoreItemUI kItemUI = CreateItemUI(kScore);
        m_listItem.Add(kItemUI);
    }

    public CScoreItemUI CreateItemUI( CScoreInfo.CScore kScore )
    {
        GameObject go = Instantiate(m_prefabItem, m_ScrollRect.content);
        go.transform.localScale = new Vector3(1, 1, 1);
        CScoreItemUI kItemUI = go.GetComponent<CScoreItemUI>();
        kItemUI.Initialize(kScore);

        Button btn = go.GetComponent<Button>();
        btn.onClick.AddListener(() => OnSelectedItem(kScore));

        return kItemUI;
    }


    public void OnSelectedItem(CScoreInfo.CScore kScore)
    {
        if( m_iSelect > 0 )
            m_listItem[m_iSelect - 1].SetSelectd(false);

        m_InputName.text = kScore.m_Name;
        m_InputKor.text = kScore.m_Kor.ToString();
        m_InputMath.text = kScore.m_Mat.ToString();
        m_InputEng.text = kScore.m_Eng.ToString();

        m_iSelect = kScore.m_idx;


        m_listItem[m_iSelect-1].SetSelectd(true);
    }


    public void OnClicked_Repair()
    {
        string sName = m_InputName.text;
        int nKor = int.Parse(m_InputKor.text);
        int nMath = int.Parse(m_InputMath.text);
        int nEng = int.Parse(m_InputEng.text);

        CScoreInfo.CScore kScore = m_kScoreInfo.Repair(m_iSelect, sName, nKor, nEng, nMath);

        CScoreItemUI kItemUI = m_listItem[m_iSelect-1];
        kItemUI.Initialize(kScore);
    }

    public void OnClicked_Delete()
    {
        CScoreInfo.CScore kScore =  m_kScoreInfo.GetScore(m_iSelect);
        if( kScore != null)
        {
            m_kScoreInfo.RemoveItem( kScore.m_Name );
            m_listItem.RemoveAt(m_iSelect-1);
            
            Destroy(m_listItem[m_iSelect-1].gameObject);
        }
    }

    public void OnClicked_Save()
    {
        m_kScoreInfo.SaveFile();
    }

    public void OnClicked_Clear()
    {
        m_listItem.Clear();
    }

    public void OnClicked_Load()
    {
        m_kScoreInfo.LoadFile();

        m_listItem.Clear();
        for( int i = 0; i < m_kScoreInfo.m_listScore.Count; i++)
        {
            CScoreInfo.CScore kScore = m_kScoreInfo.m_listScore[i];
            CScoreItemUI kItemUI = CreateItemUI(kScore);
            m_listItem.Add(kItemUI);
        }
    }

}

