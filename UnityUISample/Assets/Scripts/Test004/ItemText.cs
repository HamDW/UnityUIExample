using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemText : MonoBehaviour
{
    public int m_Index = 0;
    public Text m_txtName = null;

    public void Initialize(int idx, string sName)
    {
        m_Index = idx;
        m_txtName.text = sName;
    }

    public void SetSelect(bool bSelect)
    {
        if (bSelect)
            m_txtName.color = new Color32(50, 207, 76, 255);
        else
            m_txtName.color = Color.white;
    }

}
