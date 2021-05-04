using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public int m_Index = 0;
    [SerializeField] Image m_imgIcon = null;
    [SerializeField] Text m_txtName = null;


    private void Start()
    {
        // 이미지 로딩 예)
        //m_imgIcon.sprite = Resources.Load<Sprite>("Textures/Backdrop");
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
        //주의) Sprite는 반드시 이렇게 Load<Sprite>를 사용해야 로딩할수 있다.
        m_imgIcon.sprite = Resources.Load<Sprite>("Textures/" + sName);

        //아래쪽은 2가지 모두 로딩 실패한다.
        //m_imgIcon.sprite = Resources.Load("Textures/" + sName) as Sprite;
        //m_imgIcon.sprite = (Sprite)Resources.Load("Textures/" + sName);
    }


    public void SetSelect( bool bSelect )
    {
        if (bSelect)
            SetBgColor( new Color32(50, 207, 76, 255));
        else
            SetBgColor(Color.white);
    }    


}
