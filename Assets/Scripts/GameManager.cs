using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public List<Level> Levels = new List<Level>();
    public UnityEvent OnLevelFinished;

    private int m_CurrentLevel = 0;
    public int CurrentLevel => m_CurrentLevel;

    private GameObject m_PlayerClone;
    private PlayerController m_PlayerController;
    private GameObject m_LevelClone;

    private SoundManager m_SoundManager;

    private List<Transform> m_LevelTiles = new List<Transform>();
    public static int TilesCount { get; private set; }

    private void Awake()
    {
        m_SoundManager = GetComponent<SoundManager>();
    }

    private void Start()
    {
        LoadGame();
        OnLevelFinished.AddListener(SaveGame);
        OnLevelFinished.AddListener(m_SoundManager.LevelCompleted);
        InitialLevel(m_CurrentLevel);
    }

    private void InitialLevel(int index)
    {
        m_CurrentLevel = index;

        Vector3 entryPointPos = Levels[m_CurrentLevel].LevelObject.GetComponentsInChildren<Transform>().Where(t => t.name.Contains("EntryPoint")).FirstOrDefault().position;
        m_PlayerClone = Instantiate(PlayerPrefab, entryPointPos, Quaternion.identity);
        m_PlayerController = m_PlayerClone.GetComponent<PlayerController>();
        m_LevelClone = Instantiate(Levels[m_CurrentLevel].LevelObject, Vector3.zero, Quaternion.identity);

        m_LevelTiles = m_LevelClone.GetComponentsInChildren<Transform>().Where(t => t.name.Contains("Tile")).ToList();
        TilesCount = m_LevelTiles.Count;

        if (Levels[m_CurrentLevel].IsBig)
            CameraController.LargeLevelCameraSetup();
        else
            CameraController.SmallLevelCameraSetup();
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

    private void SaveGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", m_CurrentLevel + 1);
    }

    private void LoadGame()
    {
        m_CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
    }
}

[Serializable]
public struct Level
{
    public GameObject LevelObject;
    public bool IsBig;
}
