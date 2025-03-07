using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Lives;

    public string levelName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadMainMenu();
        
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && Lives <0)
        {
            LoadNextLevel();
        }
    }

    public void ProcessDeath()
    {
        Lives--;
        HUDObserverManager.LivesChangedChannel(Lives);
        HUDObserverManager.PlayerDeath(false);
        
        if (Lives < 0)
        {
            LoadGameOver();
        }
        else
        {
            ResetCurrentLevel();
        }
    }

    public void LoadNextLevel()
    {
        if (Lives >= 0)
        {
            HUDObserverManager.PlayerVictory(false);
            if(SceneManager.GetActiveScene().name == levelName) LoadLevel2();
            if (SceneManager.GetActiveScene().name == "LevelNARIELY 2") LoadLevel1();
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
               LoadMainMenu();
               // Lives = 3;
               // HUDObserverManager.LivesChangedChannel(Lives);
            }
        }
        
    }

    public void ResetCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(levelName);
    }
    
    public void LoadLevel2()
    {
        SceneManager.LoadScene("LevelNARIELY 2");
    }

    public void LoadGameOver()
    {
        HUDObserverManager.ActivateHUD(false);
        SceneManager.LoadScene("GameOver");
    }

    public void AddLife(int value)
    {
        Lives += value;
        HUDObserverManager.LivesChangedChannel(Lives);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("Initialize").completed += (op) => { SceneManager.LoadScene("MainMenu"); };
       // SceneManager.LoadScene("MainMenu");
    }

    public void InitializeGame()
    {
        HUDObserverManager.ActivateHUD(true);
        HUDObserverManager.LivesChangedChannel(Lives);
        Lives = 3;
        HUDObserverManager.LivesChangedChannel(Lives);
    }
}
