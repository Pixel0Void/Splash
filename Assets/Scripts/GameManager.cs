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
    private GameObject m_LevelClone;

    private void Start()
    {
        InitialLevel(15);
    }

    private void InitialLevel(int index)
    {
        m_CurrentLevel = index;

        Vector3 entryPointPos = Levels[m_CurrentLevel].GetComponentsInChildren<Transform>().Where(t => t.name.Contains("EntryPoint")).FirstOrDefault().position;
        m_PlayerClone = Instantiate(PlayerPrefab, entryPointPos, Quaternion.identity);
        m_LevelClone = Instantiate(Levels[m_CurrentLevel].gameObject, Vector3.zero, Quaternion.identity);
    }
}
