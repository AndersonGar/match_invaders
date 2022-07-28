using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //Enemy instances
    public GameObject enemyPrefab;
    public List<Color> colors;
    //Projectile instance
    public GameObject projectilePrefab;
    GameObject projectileEnemy;
    //Ubication Variables
    public Vector2 initPos;
    public float offset_x, offset_y;
    GameObject[,] enemys = new GameObject[5, 10];

    // Start is called before the first frame update
    void Start()
    {
        Generate();
        GenerateProjectile();
        Shoot();
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
                enemys[i,j] = Instantiate(enemyPrefab,new Vector3(initPos.x + offset_x*j,initPos.y-offset_y*i),Quaternion.identity);
                enemys[i, j].GetComponent<EnemyBehaviour>().SetIndex(i, j);
                SetColor(enemys[i, j]);
                enemys[i, j].transform.parent = transform;
                //enemys[i, j].name = i + "," + j;
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

    void GenerateProjectile()
    {
        projectileEnemy = Instantiate(projectilePrefab, Vector2.zero, Quaternion.identity);
        projectileEnemy.transform.parent = gameObject.transform;
    }

    public void Shoot()
    {
        Vector2 pos = GetEnemyShooter();
        projectileEnemy.transform.position = pos + Vector2.down;
        projectileEnemy.GetComponent<ProjectileBehaviour>().StartMove();
    }

    Vector2 GetEnemyShooter()
    {
        Vector2 v2 = Vector2.zero;
        bool searching = true;
        int r = 0;
        while (searching)
        {
            r = Random.Range(0, 10);
            searching = !enemys[0, r].activeSelf;
        }
        for (int i = 4; i > 0; i--)
        {
            if (enemys[i,r].activeSelf)
            {
                v2 = enemys[i, r].transform.position;
                break;
            }
        }
        return v2;
    }

    public void EnemyCollide(int _i, int _j)
    {
        //Vertical Match 
        int verticalMatches = 1;
        for (int i = _i-1; i > 0; i--)
        {
            if (enemys[_i,_j].name != enemys[i,_j].name)
            {
                break;
            }
            else
            {
                enemys[i, _j].SetActive(false);
                verticalMatches++;
            }
        }

        //Horizontal Match
        int horizontalMatches = 1;
        //Right
        for (int j = _j+1; j < 10; j++)
        {
            if (enemys[_i, _j].name != enemys[_i, j].name)
            {
                break;
            }
            else
            {
                enemys[_i, j].SetActive(false);
                horizontalMatches++;
            }
        }
        //Left
        for (int j = _j-1; j >= 0; j--)
        {
            if (enemys[_i, _j].name != enemys[_i, j].name)
            {
                break;
            }
            else
            {
                enemys[_i, j].SetActive(false);
                horizontalMatches++;
            }
        }
        if (verticalMatches + horizontalMatches == 2)
        {
            CalculatePointUp(1);
        }
        else
        {
            CalculatePointUp(verticalMatches);
            CalculatePointUp(horizontalMatches);
        }
    }

    void CalculatePointUp(int enemies)
    {
        int prevNum = 0;
        int nowNum = 1;
        for (int i = 0; i < enemies; i++)
        {
            int temp = prevNum;
            prevNum = nowNum;
            nowNum = temp + prevNum;
        }
        Debug.Log(prevNum*10*enemies);
    }
}
