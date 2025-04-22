using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 m_StartPosition;
    private Vector2 m_EndPosition;

    [SerializeField] private Vector3 m_Direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_StartPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_EndPosition = eventData.position;
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        m_Direction = (m_EndPosition - m_StartPosition).normalized;

        if (Mathf.Abs(m_Direction.x) > Mathf.Abs(m_Direction.y))
        {
            if (m_Direction.x > 0)
            {
                m_Direction = Vector3.right;
            }
            else
            {
                m_Direction = Vector3.left;
            }
        }
        else
        {
            if (m_Direction.y > 0)
            {
                m_Direction = Vector3.forward;
            }
            else
            {
                m_Direction = Vector3.back;
            }
        }
    }
}
