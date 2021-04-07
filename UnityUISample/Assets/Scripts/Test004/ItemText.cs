using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    public int m_Index = 0;
    private bool m_bSelected = false;

    public Text m_txtCity = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(int idx, string sName)
    {
        m_Index = idx;
        m_txtCity.text = sName;
    }

    public void SetBgColor(Color color)
    {
        m_txtCity.color = color;
    }


    public void SetSelect(bool bSelect)
    {
        m_bSelected = bSelect;

        if (m_bSelected)
            SetBgColor(new Color32(50, 207, 76, 255));
        else
            SetBgColor(Color.white);

    }


}
