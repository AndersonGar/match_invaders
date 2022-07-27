using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float speed;
    bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            move = false;
            gameObject.SetActive(false);
            transform.parent.gameObject.GetComponent<EnemyGenerator>().Shoot();
        }
    }

    public void StartMove()
    {
        gameObject.SetActive(true);
        move = true;
    }
}
