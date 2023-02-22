using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    [SerializeField]private string[] levels;

    [SerializeField]private string mainMenu;

    [SerializeField] private string introVideoScene;

    private int currLevel;
    public int CurrentLevel { get { return currLevel; } }
    public static LevelManager instance;

    public delegate void OnChangeScene();
    public static OnChangeScene changeScene;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        /*
        currLevel = 0;
        LoadLevel(currLevel);
        */
        //NextLevel();
    }


    public void NextLevel()
    {
        currLevel++;
        if (levels.Length==currLevel)
        {
            GameOverMenu.instance.EndGame();
            GameOver();
            currLevel = 0;
        }
        else 
        {
            LoadNewLevel(currLevel);
            if (currLevel != 0)
                SkillManager.instance.NextLevelToLoad(currLevel + 1);
        }
    }

    public void LoadLevel(int index)
    {
        currLevel = index;
        LoadNewLevel(index);
        StartCoroutine("WaitSkillManager");
    }

    private IEnumerator WaitSkillManager()
    {
        LoadCanvas.instance.Open();
        yield return new WaitUntil(() => GameManager.instance != null);
        yield return new WaitUntil(() => SkillManager.instance.AmIReady());
        yield return new WaitUntil(() => ScoreManager.instance != null);
        SkillManager.instance.LoadLevelFromScratch(currLevel + 1);
        ScoreManager.instance.LoadScoreForLevel(currLevel + 1);
        LoadCanvas.instance.Close();
    }

    public void LoadIntroVideo()
    {
        SceneManager.LoadScene(introVideoScene);
    }

    /// <summary>
    /// Only load the scene from the scenemanager
    /// </summary>
    /// <param name="index">index of level to load</param>
    private void LoadNewLevel(int index)
    {
        AudioManager.instance.PlayMusic("Main");
        if (changeScene != null) 
        {
            changeScene();
        }
        SceneManager.LoadScene(levels[index]);
        StartCoroutine("WaitUI");
        ScoreManager.instance.StartLevel();
    }

    private IEnumerator WaitUI()
    {
        yield return new WaitUntil(() => UIManager.instance != null);
        UIManager.instance.SetLevelText(currLevel);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        MenuManager.instance.LoadMainScene();
    }

    internal void GameOver()
    {
        MenuManager.instance.GameOver();
    }

    public void TryAgainLevel()
    {
        LoadNewLevel(currLevel);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    internal void SetCurrentAsDone()
    {
        int level = currLevel + 2;
        ScoreManager.instance.SaveScoreForLevel(currLevel + 1);
        if (PlayerPrefs.HasKey("Level" + level))
        {
            if (PlayerPrefs.GetInt("Level" + level) != 1)
            {
                PlayerPrefs.SetInt("Level" + level, 1);
            }
        }
        else
        {
            PlayerPrefs.SetInt("Level" + level, 1);
        }
    }

}
