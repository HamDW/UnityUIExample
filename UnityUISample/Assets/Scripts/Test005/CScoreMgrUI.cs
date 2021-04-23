using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CScoreMgrUI : MonoBehaviour
{

    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] Button m_btnLoad = null;
    [SerializeField] Button m_btnAdd = null;
    [SerializeField] InputField m_InputName = null;
    [SerializeField] InputField m_InputKor = null;
    [SerializeField] InputField m_InputEng = null;
    [SerializeField] InputField m_InputMath = null;


    // Start is called before the first frame update
    void Start()
    {
        //m_btnOK.onClick.AddListener(OnClicked_OK);
        //m_btnClear.onClick.AddListener(OnClicked_Clear);
        //m_btnAdd.onClick.AddListener(OnClicked_Add);
        //m_btnLoad.onClick.AddListener(OnClicked_Load);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
