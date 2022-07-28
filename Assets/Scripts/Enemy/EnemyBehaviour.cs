using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speedVertical, speedHorizontal;
    public int secondsMovement;
    float time;
    int i, j;
    bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        //time = secondsMovement;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            VerticalMovement();
            HorizontalMovement();
        }
    }

    public void AllowMove(bool allow)
    {
        move = allow;
        speedHorizontal = Mathf.Abs(speedHorizontal);
        time = secondsMovement;
    }

    public void SetIndex(int x, int y)
    {
        i = x;
        j = y;
    }

    public void SetSpeed(float increment)
    {
        speedVertical *= increment;
    }


    void VerticalMovement()
    {
        transform.Translate(Vector2.down * speedVertical * Time.deltaTime);
    }

    void HorizontalMovement()
    {
        Timer();
        transform.Translate(Vector2.right * speedHorizontal * Time.deltaTime);
    }

    void Timer()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            speedHorizontal *= -1;
            time = secondsMovement;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pj_Player")
        {
            gameObject.SetActive(false);
            transform.parent.GetComponent<EnemyGenerator>().EnemyCollide(i, j);
        }

        if (collision.tag == "Wall")
        {
            transform.parent.GetComponent<GameManager>().GameOver();
        }
    }

    
}
