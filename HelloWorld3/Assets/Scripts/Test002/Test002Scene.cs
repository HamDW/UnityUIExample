using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test002Scene : MonoBehaviour
{
    public GameObject m_Qube;
    public Rigidbody m_QubeRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        m_QubeRigidBody = m_Qube.GetComponent<Rigidbody>();

        
    }

    void Update1()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 vScale = m_Qube.transform.localScale;
            vScale.x += 0.1f;
            vScale.y += 0.1f;
            vScale.z += 0.1f;
            m_Qube.transform.localScale = vScale;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Vector3 vScale = m_Qube.transform.localScale;
            vScale.x -= 0.1f;
            vScale.y -= 0.1f;
            vScale.z -= 0.1f;
            m_Qube.transform.localScale = vScale;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Qube.transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Qube.transform.Translate(0.1f, 0, 0, Space.Self);
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_Qube.transform.Rotate(5.0f, 0, 0);
        }
    }


    // Update is called once per frame
    void Update()
    {
        Update_Scale();
        Update_Move4();
        Update_Rotate2();
    }


    private void Update_Scale()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 vScale = m_Qube.transform.localScale;
            vScale.x += 0.1f;
            vScale.y += 0.1f;
            vScale.z += 0.1f;
            m_Qube.transform.localScale = vScale;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Vector3 vScale = m_Qube.transform.localScale;
            vScale.x -= 0.1f;
            vScale.y -= 0.1f;
            vScale.z -= 0.1f;
            m_Qube.transform.localScale = vScale;
        }
    }

    private void Update_Move1()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 vPos = m_Qube.transform.position;
            vPos.x -= 0.05f;
            m_Qube.transform.position = vPos;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 vPos = m_Qube.transform.position;
            vPos.x += 0.05f;
            m_Qube.transform.position = vPos;
        }
    }


    private void Update_Move2()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_Qube.transform.Translate(0, 0, 0.05f, Space.World);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_Qube.transform.Translate(0, 0, -0.05f, Space.World);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Qube.transform.Translate(-0.05f, 0, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Qube.transform.Translate(0.05f, 0, 0, Space.World);
            //m_Hello.transform.Translate(0.1f, 0, 0, Space.Self);
        }
    }

    private void Update_Move3()
    {
        // 수평축 과 수직축의 입력값을 감지하여 저장
        float xDelta = Input.GetAxis("Horizontal");
        float yDelta = Input.GetAxis("Vertical");

        // 이동된 거리 계산
        float xPos = xDelta * 0.2f; 
        float yPos = yDelta * 0.2f; 

        Vector3 newVelocity = new Vector3(xPos, yPos, 0);

        m_Qube.transform.Translate(newVelocity);
    }


    private void Update_Rotate1()
    {
        if (Input.GetKey(KeyCode.T))
        {
            float fValue = 5.0f;
            Vector3 vRot = m_Qube.transform.localEulerAngles;
             vRot.z += fValue;
            m_Qube.transform.localEulerAngles = vRot;
        }
    }

    private void Update_Rotate2()
    {
        if (Input.GetKey(KeyCode.T))
        {
            float fValue = 5.0f;
            m_Qube.transform.Rotate(fValue, 0, 0);
        }
    }



    private void Update_Move4()
    {
        float fSpeed = 1.0f;
        // 수평축 과 수직축의 입력값을 감지하여 저장
        float xDelta = Input.GetAxis("Horizontal");
        float yDelta = Input.GetAxis("Vertical");

        // 리지드바디에 할당할 새로운 속도 계산
        float xSpeed = xDelta * fSpeed;
        float ySpeed = yDelta * fSpeed;

        Vector3 newVelocity = new Vector3(xSpeed, 0, ySpeed);

        // 리지드바디의 속도에 할당
        m_QubeRigidBody.velocity = newVelocity;
    }

}
