using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Color> colors;
    //Ubication Variables
    public Vector2 initPos;
    public float offset_x, offset_y;
    GameObject[,] enemys = new GameObject[5, 10];

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject enemy = Instantiate(enemyPrefab,new Vector3(initPos.x + offset_x*j,initPos.y-offset_y*i),Quaternion.identity);
                SetColor(enemy);
            }
        }
    }

    void SetColor(GameObject go)
    {
        SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
        int r = Random.Range(0, 4);
        spriteRenderer.color = colors[r];
        go.name = r.ToString();
    }
}
