using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed;
    public ProjectileDirection direction;
    Vector2 vecDirection;
    bool move = false;
    GameObject owner;

    public enum ProjectileDirection
    {
        Up,
        Down

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.Translate(vecDirection * speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag=="Pj_Enemy")
        {
            PjEnemy_Collisions(collision);
        }
        else if (gameObject.tag == "Pj_Player")
        {
            PjPlayer_Collisions(collision);
        }
    }

    void PjEnemy_Collisions(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            move = false;
            gameObject.SetActive(false);
            if (transform.parent.gameObject.GetComponent<GameManager>().GameRunning())
            {
                transform.parent.gameObject.GetComponent<EnemyGenerator>().Shoot();
            }
        }
    }

    void PjPlayer_Collisions(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            move = false;
            gameObject.SetActive(false);
            owner.GetComponent<PlayerBehaviour>().AllowShoot();
        }
    }

    public void StartMove()
    {
        SetDirection();
        gameObject.SetActive(true);
        move = true;
    }

    void SetDirection()
    {
        switch (direction)
        {
            case ProjectileDirection.Up:
                vecDirection = Vector2.up;
                break;
            case ProjectileDirection.Down:
                vecDirection = Vector2.down;
                break;
            default:
                break;
        }
    }

    public void SetOwner(GameObject go)
    {
        owner = go;
    }
}
