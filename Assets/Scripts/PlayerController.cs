using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 m_Direction;
    private Vector3 m_TargetPosition;
    private bool m_RecivingInput = true;
    private bool m_CanMove = false;
    private float m_Speed = 20f;

    private void Update()
    {
        if(m_RecivingInput == false && m_CanMove)
        {
            Move();
        }
        SetDirection();
    }

    private bool GetTargetPosition(out Vector3 targetPos)
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, m_Direction, out hit,100f))
        {
            if(hit.transform.CompareTag("Wall"))
            {
                targetPos = hit.transform.position;
                return true;
            }
        }
        targetPos = new Vector3();
        return false;
    }

    private void SetDirection()
    {
        if(m_RecivingInput)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                m_Direction = Vector3.left;
                m_CanMove = GetTargetPosition(out m_TargetPosition);
                m_RecivingInput = !m_CanMove;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                m_Direction = Vector3.forward;
                m_CanMove = GetTargetPosition(out m_TargetPosition);
                m_RecivingInput = !m_CanMove;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                m_Direction = Vector3.right;
                m_CanMove = GetTargetPosition(out m_TargetPosition);
                m_RecivingInput = !m_CanMove;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                m_Direction = Vector3.back;
                m_CanMove = GetTargetPosition(out m_TargetPosition);
                m_RecivingInput = !m_CanMove;
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
