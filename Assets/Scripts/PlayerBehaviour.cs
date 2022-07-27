using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();   
    }

    void Movement()
    {
        float h_axis = Input.GetAxis("Horizontal");
        Vector2 vDir = new Vector2(h_axis,0f);
        transform.Translate(vDir * speed * Time.deltaTime);
    }
}
