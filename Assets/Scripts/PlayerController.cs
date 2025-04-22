using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 m_Direction;
    private Vector3 m_TargetPosition;
    private bool m_RecivingInput = true;
    private bool m_CanMove = false;
    private float m_Speed = 20f;
    private Material m_PlayerMat;
    private List<Transform> m_WalkedTiles = new List<Transform>();

    private GameManager m_GameManager;
    private SoundManager m_SoundManager;
    private IInputHandler m_Input;

    private void Awake()
    {
        m_PlayerMat = GetComponent<MeshRenderer>().material;
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_SoundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundManager>();

        m_Input = m_GameManager.InputHandler;
    }

    private void Update()
    {
        if (m_RecivingInput == false && m_CanMove)
        {
            Move();
        }
        SetDirection();
    }

    private bool GetTargetPosition(out Vector3 targetPos)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, m_Direction, out hit, 100f))
        {
            if (hit.transform.CompareTag("Wall"))
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
        if (m_RecivingInput)
        {
            m_Direction = m_Input.SetDirection();
            m_CanMove = GetTargetPosition(out m_TargetPosition);
            m_RecivingInput = !m_CanMove;
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, m_TargetPosition - m_Direction, m_Speed * Time.deltaTime);
        m_RecivingInput = Arrived();
    }

    private bool Arrived()
    {
        if (transform.position == m_TargetPosition - m_Direction)
        {
            m_SoundManager.Triggered();
            m_GameManager.TouchInput.RestValues();
            return true;
        }
        return false;
    }

    private bool LevelFinished()
    {
        if (m_GameManager.TilesCount == m_WalkedTiles.Count)
        {
            m_GameManager.OnLevelFinished?.Invoke();
            m_CanMove = false;
            return true;
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            if (!m_WalkedTiles.Contains(other.transform))
            {
                m_WalkedTiles.Add(other.transform);
                other.GetComponent<MeshRenderer>().material = m_PlayerMat;
                LevelFinished();
            }
        }
    }
}
