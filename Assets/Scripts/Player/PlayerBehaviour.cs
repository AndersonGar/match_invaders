using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public GameObject projectile;
    bool may_shoot = true;
    // Start is called before the first frame update
    void Start()
    {
        projectile.GetComponent<ProjectileBehaviour>().SetOwner(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shoot();
    }



    void Movement()
    {
        float h_axis = Input.GetAxis("Horizontal");
        Vector2 vDir = new Vector2(h_axis,0f);
        transform.Translate(vDir * speed * Time.deltaTime);
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && may_shoot)
        {
            may_shoot = false;
            projectile.transform.position = gameObject.transform.position + Vector3.up;
            projectile.SetActive(true);
            projectile.GetComponent<ProjectileBehaviour>().StartMove();
        }
    }

    public void AllowShoot()
    {
        may_shoot = true;
    }

}
