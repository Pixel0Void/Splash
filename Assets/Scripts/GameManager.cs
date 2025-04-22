using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public enum InputTypeEnum
    {
        Keyboard, Touch
    }

    public InputTypeEnum InputType;
    public IInputHandler InputHandler;
    public TouchInput TouchInput;
    public KeyboardInput KeyboardInput;

    public GameObject PlayerPrefab;
    public List<Level> Levels = new List<Level>();
    public GameObject FinishLevelPanel;
    public UnityEvent OnLevelFinished;

    private int m_CurrentLevel = 0;
    public int CurrentLevel => m_CurrentLevel;

    private GameObject m_PlayerClone;
    private GameObject m_LevelClone;

    private SoundManager m_SoundManager;

    public int TilesCount { get; private set; }

    private void Awake()
    {
        m_SoundManager = GetComponent<SoundManager>();
        switch (InputType)
        {
            case InputTypeEnum.Keyboard:
                InputHandler = KeyboardInput;
                break;
            case InputTypeEnum.Touch:
                InputHandler = TouchInput;
                break;
        }
    }

    private void Start()
    {
        LoadGame();
        OnLevelFinished.AddListener(SaveGame);
        OnLevelFinished.AddListener(EnableFinishLevelPanel);
        InitialLevel(m_CurrentLevel);
    }

    private void EnableFinishLevelPanel()
    {
        StartCoroutine(EnableFinishLevelPanel(0.2f));
    }

    IEnumerator EnableFinishLevelPanel(float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        FinishLevelPanel.SetActive(true);
        m_SoundManager.LevelCompleted();
    }

    private void InitialLevel(int index)
    {
        m_CurrentLevel = index;

        Vector3 entryPointPos = Levels[m_CurrentLevel].LevelObject.GetComponentsInChildren<Transform>().Where(t => t.name.Contains("EntryPoint")).FirstOrDefault().position;
        m_PlayerClone = Instantiate(PlayerPrefab, entryPointPos, Quaternion.identity);
        m_LevelClone = Instantiate(Levels[m_CurrentLevel].LevelObject, Vector3.zero, Quaternion.identity);

        TilesCount = m_LevelClone.GetComponentsInChildren<Transform>().Where(t => t.name.Contains("Tile")).Count();

        CameraController.SetOrthographicSize(Levels[m_CurrentLevel].OrthographicSize);
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
        TouchInput.ResetValues();
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
    public int OrthographicSize;
}
