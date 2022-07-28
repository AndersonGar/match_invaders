using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speedVertical, speedHorizontal;
    public int secondsMovement;
    float time;
    int i, j;
    GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        time = secondsMovement;
    }

    // Update is called once per frame
    void Update()
    {
        VerticalMovement();
        HorizontalMovement();
    }

    public void SetIndex(int x, int y)
    {
        i = x;
        j = y;
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
}
