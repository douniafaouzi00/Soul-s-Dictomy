using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int currScore;
    [SerializeField] private int scoreToAddWhenDie;
    public static ScoreManager instance;

    private void Awake()
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
        currScore = 0;
    }

    internal void StartLevel()
    {
        StopAllCoroutines();
        StartCoroutine("IncreaseScore");
    }

    private IEnumerator IncreaseScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            ++currScore;
            UIManager.instance.SetScoreText(currScore);
        }
    }

    internal void StopScore()
    {
        StopAllCoroutines();
    }

    internal void SaveScoreForLevel(int level)
    {
        if (PlayerPrefs.HasKey("ScoreLevel" + level))
        {
            if(PlayerPrefs.GetInt("ScoreLevel"+ level)>currScore)
            {
                PlayerPrefs.SetInt("ScoreLevel" + level, currScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("ScoreLevel" + level, currScore);
        }
    }

    internal void LoadScoreForLevel(int level)
    {
        int l = level - 1;
        if (PlayerPrefs.HasKey("ScoreLevel" + l))
        {
            currScore = PlayerPrefs.GetInt("ScoreLevel" + l);
            UIManager.instance.SetScoreText(currScore);
        }
        if (l == 0)
        {
            currScore = 0;
        }
    }

    internal void DiePenality()
    {
        currScore += scoreToAddWhenDie;
        UIManager.instance.SetScoreText(currScore);
    }

    public int GetDieValue()
    {
        return scoreToAddWhenDie;
    }
}
