using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score081Dlg : MonoBehaviour
{
    [SerializeField] Button m_btnOK = null;
    [SerializeField] Button m_btnClear = null;
    [SerializeField] InputField m_InputName = null;
    [SerializeField] InputField m_InputKor = null;
    [SerializeField] InputField m_InputEng = null;
    [SerializeField] InputField m_InputMath = null;
    [SerializeField] Text m_txtResult = null;

    // Start is called before the first frame update
    void Start()
    {
        m_btnOK.onClick.AddListener(OnClicked_OK);
        m_btnClear.onClick.AddListener(OnClicked_Clear);
    }


    public void OnClicked_OK()
    {
        string name = m_InputName.text;
        int kor = int.Parse(m_InputKor.text);
        int eng = int.Parse(m_InputEng.text);
        int mat = int.Parse(m_InputMath.text);
        int total = kor + mat + eng;
        float avg = total / 3;

        m_txtResult.text = string.Format("\n이름 : {0}\nKor  = {1}\nEng  = {2}\nMath = {3}\n함계 = {4}\n평균 = {5}", 
                                        name, kor, eng, mat, total, avg);


    }


    public void OnClicked_Clear()
    {
        m_txtResult.text = "";
        m_InputName.text = "";
        m_InputKor.text = "";
        m_InputEng.text = "";
        m_InputMath.text = "";
    }

}
