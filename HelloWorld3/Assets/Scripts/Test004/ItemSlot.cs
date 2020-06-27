using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int m_Index = 0;
    [SerializeField] Image m_imgIcon = null;
    [SerializeField] Text m_txtName = null;
    private bool m_bSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Initialize( int idx, string sName)
    {
        m_Index = idx;
        m_txtName.text = sName;
    }

    public void SetBgColor(Color color)
    {
        Image kImage = GetComponent<Image>();
        kImage.color = color;
    }

    public void SetImage(string sName)
    {
        m_imgIcon.sprite = Resources.Load("Textures/" + sName) as Sprite;
    }


    public void SetSelect( bool bSelect )
    {
        m_bSelected = bSelect;

        if (m_bSelected)
            SetBgColor( new Color32(50, 207, 76, 255));
        else
            SetBgColor(Color.white);

    }    


}
