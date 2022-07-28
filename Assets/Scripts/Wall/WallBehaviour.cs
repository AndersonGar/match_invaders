using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    public GameManager gameManager;
    public int live = 5;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pj_Enemy")
        {
            live--;
            float alfa = (float)live * 20/ 100;
            GetComponent<SpriteRenderer>().color = new Color(color.r,color.g,color.b,alfa);
            if (live <= 0)
            {
                gameManager.WallDown();
                gameObject.SetActive(false);
            }
        }
    }

    public void Regenerate()
    {
        live = 5;
        float alfa = (float)live * 20 / 100;
        gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, alfa);
    }
}
