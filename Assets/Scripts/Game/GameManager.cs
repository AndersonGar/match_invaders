using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public List<WallBehaviour> listWalls;
    int score = 0;
    int record = 0;
    int level = 1;
    int live = 3;
    int walls = 4;
    bool runGame = true;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Record"))
        {
            PlayerPrefs.SetInt("Record", 0);
            PlayerPrefs.Save();
        }
        record = PlayerPrefs.GetInt("Record");
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

    public IEnumerator NextLevel()
    {
        level++;
        walls = 0;
        runGame = false;
        yield return new WaitForSeconds(3);
        foreach (var wall in listWalls)
        {
            wall.GetComponent<WallBehaviour>().Regenerate();
        }
        SendMessage("RelocateEnemies");
        runGame = true;
    }

    public bool GameRunning()
    {
        return runGame;
    }

    public void GameOver()
    {
        runGame = false;
        SendMessage("StopEnemies");
        if (score > record)
        {
            uiManager.ShowGameOver(true);
            PlayerPrefs.SetInt("Record", score);
            PlayerPrefs.Save();
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
