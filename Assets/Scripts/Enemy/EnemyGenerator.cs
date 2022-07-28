using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //Enemy instances
    public GameObject enemyPrefab;
    public List<Color> colors;
    GameObject[,] enemys = new GameObject[5, 10];
    int activedEnemies = 50;
    public float incrementSpeed;
    //Projectile instance
    public GameObject projectilePrefab;
    GameObject projectileEnemy;
    //Ubication Variables
    public Vector2 initPos;
    public float offset_x, offset_y;

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
                enemys[i,j] = Instantiate(enemyPrefab,transform);
                enemys[i, j].GetComponent<EnemyBehaviour>().SetIndex(i, j);
                SetupEnemy(enemys[i, j],i,j);
            }
        }
    }

    public void StopEnemies()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                enemys[i, j].GetComponent<EnemyBehaviour>().AllowMove(false);
            }
        }
    }

    public void RelocateEnemies()
    {
        activedEnemies = 50;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                SetupEnemy(enemys[i, j], i, j);
            }
        }
        Shoot();
    }

    void SetupEnemy(GameObject enemy,int _i,int _j)
    {
        enemy.SetActive(true);
        enemy.transform.position = new Vector3(initPos.x + offset_x * _j, initPos.y - offset_y * _i);
        int level = GetComponent<GameManager>().GetLevel();
        enemy.GetComponent<EnemyBehaviour>().SetSpeed(incrementSpeed * level);
        SetColor(enemy);
        enemy.GetComponent<EnemyBehaviour>().AllowMove(true);
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
        int r = 0;
        List<int> rows = new List<int>();
        for (int i = 0; i < 10; i++)
        {
            if (enemys[0, i].activeSelf)
            {
                rows.Add(i);
            }
        }
        r = Random.Range(0, rows.Count);
        int index = rows[r];
        for (int i = 4; i > 0; i--)
        {
            if (enemys[i,index].activeSelf)
            {
                v2 = enemys[i, index].transform.position;
                break;
            }
        }
        return v2;
    }

    public void EnemyCollide(int _i, int _j)
    {
        //Vertical Match 
        int verticalMatches = 1;
        for (int i = _i-1; i >= 0; i--)
        {
            if (enemys[_i,_j].name != enemys[i,_j].name || !enemys[i, _j].activeSelf)
            {
                break;
            }
            else
            {
                enemys[i, _j].SetActive(false);
                verticalMatches++;
            }
        }
        if (verticalMatches > 1)
        {
            CalculatePointUp(verticalMatches);
        }
        //Horizontal Match
        int horizontalMatches = 1;
        //Right
        for (int j = _j+1; j < 10; j++)
        {
            if (enemys[_i, _j].name != enemys[_i, j].name || !enemys[_i, j].activeSelf)
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
            if (enemys[_i, _j].name != enemys[_i, j].name || !enemys[_i, j].activeSelf)
            {
                break;
            }
            else
            {
                enemys[_i, j].SetActive(false);
                horizontalMatches++;
            }
        }
        if (horizontalMatches > 1)
        {
            CalculatePointUp(horizontalMatches);
        }
        //If there isn't matches
        if (horizontalMatches+verticalMatches==2)
        {
            CalculatePointUp(horizontalMatches);
        }
        print("Prev: " + activedEnemies);
        activedEnemies -=  ((horizontalMatches - 1) + (verticalMatches - 1) + 1);
        print("Now: "+activedEnemies);
        if (activedEnemies <= 0)
        {
            StartCoroutine(GetComponent<GameManager>().NextLevel());
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
        int totalPoint = nowNum * 10 * enemies;
        SendMessage("ScoreUp", totalPoint);
    }
}
