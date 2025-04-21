using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public List<Transform> Levels = new List<Transform>();
    public UnityEvent OnLevelFinished;

    private int m_CurrentLevel = 0;
    public int CurrentLevel => m_CurrentLevel;

    private GameObject m_PlayerClone;
    private PlayerController m_PlayerController;
    private GameObject m_LevelClone;

    private List<Transform> m_LevelTiles = new List<Transform>();
    public static int TilesCount { get; private set; }

    private void Start()
    {
        InitialLevel(m_CurrentLevel);
    }

    private void InitialLevel(int index)
    {
        m_CurrentLevel = index;

        Vector3 entryPointPos = Levels[m_CurrentLevel].GetComponentsInChildren<Transform>().Where(t => t.name.Contains("EntryPoint")).FirstOrDefault().position;
        m_PlayerClone = Instantiate(PlayerPrefab, entryPointPos, Quaternion.identity);
        m_PlayerController = m_PlayerClone.GetComponent<PlayerController>();
        m_LevelClone = Instantiate(Levels[m_CurrentLevel].gameObject, Vector3.zero, Quaternion.identity);

        m_LevelTiles = m_LevelClone.GetComponentsInChildren<Transform>().Where(t => t.name.Contains("Tile")).ToList();
        TilesCount = m_LevelTiles.Count;
    }

    public void MoveToNextLevel()
    {
        if (m_CurrentLevel < Levels.Count - 1)
            ++m_CurrentLevel;
        else
            m_CurrentLevel = 0;
        Destroy(m_PlayerClone);
        Destroy(m_LevelClone);
        InitialLevel(CurrentLevel);
    }
}
