using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * 1. Hello world 출력하기
 * 2. 글자를 좌우 이동하기
 * 3. 글자를 확대 축소하기
 * 4. 글자를 회전하기
 * 5. 추가(실습)
 *    버튼을 만들어 위 기능을 처리하기

 */
public class GameScene : MonoBehaviour
{
    public Text m_Hello;
    public SpriteRenderer m_BgRenderer;

    public Sprite[] m_Sprites;

    [HideInInspector] public bool m_bCheck = true;

    void Start()
    {
        Initialize();   
    }

    public void Initialize()
    {
        if (m_Sprites.Length == 0)
            return;

        StartCoroutine("IEnum_ChangeBg", 1.0f);
    }

    IEnumerator IEnum_ChangeBg(float fDelay)
    {
        int iIndex = 0;

        while (m_bCheck)
        {
            yield return new WaitForSeconds(fDelay);

            m_BgRenderer.sprite = m_Sprites[iIndex];

            iIndex++;
            if ( iIndex >= m_Sprites.Length )
            {
                iIndex = 0;
            }
        }

        yield return null;
    }

    void Update()
    {
        // 확대
        if( Input.GetMouseButtonUp( 0 ) )
        {
            Vector3 vScale = m_Hello.transform.localScale;
            vScale.x += 0.1f;
            vScale.y += 0.1f;
            m_Hello.transform.localScale = vScale;
        }    

        // 축소
        if( Input.GetMouseButtonUp(1))
        {
            Vector3 vScale = m_Hello.transform.localScale;
            vScale.x -= 0.1f;
            vScale.y -= 0.1f;
            m_Hello.transform.localScale = vScale;
        }

        
        // 한방향 회전
        if (Input.GetKey(KeyCode.T))
        {
            Vector3 vRot = m_Hello.transform.localEulerAngles;
            vRot.z += 5.0f;
            m_Hello.transform.localEulerAngles = vRot;
            //m_Hello.transform.Rotate(0, 0, 5.0f);
        }


        Update_Move2();
    }




    private void Update_Move2()
    {
        // 수평축 과 수직축의 입력값을 감지하여 저장
        float xDelta = Input.GetAxis("Horizontal");
        float yDelta = Input.GetAxis("Vertical");

        // 리지드바디에 할당할 새로운 속도 계산
        float xSpeed = xDelta * 1.0f;
        float ySpeed = yDelta * 1.0f;

        Vector3 newDist = new Vector3(xSpeed, ySpeed, 0);

        m_Hello.transform.Translate(newDist);

    }

    private void Update_Move1()
    {
        // 좌우 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 vPos = m_Hello.transform.localPosition;
            vPos.x -= 0.5f;
            m_Hello.transform.localPosition = vPos;
            //m_Hello.transform.Translate(-0.1f, 0, 0);         // 디폴트 : 로컬좌표
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 vPos = m_Hello.transform.localPosition;
            vPos.x += 0.5f;
            m_Hello.transform.localPosition = vPos;
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.Self);
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.World);    // 주의: 월드 좌표로 이동
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Vector3 vPos = m_Hello.transform.localPosition;
            vPos.y += 0.5f;
            m_Hello.transform.localPosition = vPos;
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.Self);
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.World);    // 주의: 월드 좌표로 이동
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector3 vPos = m_Hello.transform.localPosition;
            vPos.y -= 0.5f;
            m_Hello.transform.localPosition = vPos;
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.Self);
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.World);    // 주의: 월드 좌표로 이동
        }
    }


}
