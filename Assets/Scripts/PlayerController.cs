using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 m_Direction;
    private Vector3 m_TargetPosition;
    private bool m_RecivingInput = true;
    private float m_Speed = 20f;

    private void Update()
    {
        if(m_RecivingInput == false)
        {
            Move();
        }
        SetDirection();
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 target = new Vector3();
        RaycastHit hit;
        if(Physics.Raycast(transform.position, m_Direction, out hit,100f))
        {
            if(hit.transform.CompareTag("Wall"))
            {
                target = hit.transform.position;
            }
        }
        return target;
    }

    private void SetDirection()
    {
        if(m_RecivingInput)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_Direction = Vector3.left;
                m_TargetPosition = GetTargetPosition();
                m_RecivingInput = false;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                m_Direction = Vector3.forward;
                m_TargetPosition = GetTargetPosition();
                m_RecivingInput = false;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                m_Direction = Vector3.right;
                m_TargetPosition = GetTargetPosition();
                m_RecivingInput = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                m_Direction = Vector3.back;
                m_TargetPosition = GetTargetPosition();
                m_RecivingInput = false;
            }
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition - m_Direction, m_Speed * Time.deltaTime);
        m_RecivingInput = Arrived();
    }

    private bool Arrived()
    {
        if(transform.position == m_TargetPosition - m_Direction)
        {
            return true;
        }
        return false;
    }
}
