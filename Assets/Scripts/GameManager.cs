using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public List<Transform> Levels = new List<Transform>();
    private int m_CurrentLevel = 0;

    private GameObject m_PlayerClone;
    private PlayerController m_PlayerController;
    private GameObject m_LevelClone;

    private List<Transform> m_LevelTiles = new List<Transform>();
    public static int TilesCount { get; private set; }

    private void Start()
    {
        InitialLevel(13);
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
}
