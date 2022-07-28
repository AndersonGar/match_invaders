using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreLabel, recordLabel, liveLabel;
    GameObject gameOver;
    Text finalMessage, resetText;
    bool resetGame = false;
    float time;
    public float resetTimer = 5;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = transform.GetChild(2).gameObject;
        finalMessage = gameOver.transform.GetChild(0).GetComponent<Text>();
        resetText = gameOver.transform.GetChild(1).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (resetGame)
        {
            Timer();
        }
    }

    public void UpdateScore(int value)
    {
        scoreLabel.text = "SCORE: " + value;
    }

    public void UpdateRecord(int value)
    {
        recordLabel.text = "RECORD: " + value;
    }

    public void UpdateLive(int value)
    {
        liveLabel.text = "LIVES: " + value;
    }

    public void ShowGameOver(bool new_record)
    {
        gameOver.SetActive(true);
        string message = "¡YOU LOSE!";
        if (new_record)
        {
            message = "¡NEW RECORD!";
        }
        finalMessage.text = message;
        time = resetTimer;
        resetGame = true;
    }

    public float GetTimer()
    {
        return resetTimer;
    }

    void Timer()
    {
        time -= Time.deltaTime;
        resetText.text = "PLAY AGAIN IN "+(int)time+" SECONDS";
        if (time <= 0)
        {
            Debug.Log("Perdiste, el tiempo se acabó");
        }
    }
}
