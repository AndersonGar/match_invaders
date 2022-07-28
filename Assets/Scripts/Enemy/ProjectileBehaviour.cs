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
        if (collision.gameObject.tag == "Enemy" && gameObject.tag =="Pj_Enemy")
        {
            return;
        }
        move = false;
        gameObject.SetActive(false);
        if (collision.gameObject.tag == "Wall")
        {
            if (gameObject.tag == "Pj_Enemy")
            {
                transform.parent.gameObject.GetComponent<EnemyGenerator>().Shoot();
            }
            else
            {
                owner.GetComponent<PlayerBehaviour>().AllowShoot();
            }
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
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
