using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishedLevelPanel : MonoBehaviour
{
    public Text LevelNumberTxt;
    private GameManager m_GameManager;

    private void Awake()
    {
        m_GameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        LevelNumberTxt.text = (m_GameManager.CurrentLevel + 1).ToString();
    }

    public void NextLevelBtn()
    {
        m_GameManager.MoveToNextLevel();
        gameObject.SetActive(false);
    }
}
