using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    int score = 0;
    int record = 0;
    int level = 0;
    int live = 3;
    int walls = 4;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("record"))
        {
            PlayerPrefs.SetInt("record", 0);
            PlayerPrefs.Save();
        }
        record = PlayerPrefs.GetInt("record");
    }
    // Start is called before the first frame update
    void Start()
    {
        ScoreUp(score);
        SetRecord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetLevel()
    {
        return level;
    }

    public void ScoreUp(int points)
    {
        score += points;
        uiManager.UpdateScore(score);
    }

    public void SetRecord()
    {
        uiManager.UpdateRecord(record);
    }

    public void LiveDown()
    {
        live--;
        uiManager.UpdateLive(live);
        if (live <= 0)
        {
            GameOver();
        }
    }

    public void WallDown()
    {
        walls--;
        if (walls <= 0)
        {
            GameOver();
        }
    }

    public void NextLevel()
    {
        level++;
        SendMessage("RelocateEnemies");
    }

    public void GameOver()
    {
        if (score > record)
        {
            uiManager.ShowGameOver(true);
        }
        else
        {
            uiManager.ShowGameOver(false);
        }
        float resetTimer = uiManager.GetTimer();
        Invoke("ResetGame", resetTimer);
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}
