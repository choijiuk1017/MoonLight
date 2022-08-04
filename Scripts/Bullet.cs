using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("destroy", 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Tree")
        {
            Destroy(gameObject, 3);
        }
        
     
    }

    private void destroy()
    {
        Destroy(gameObject);
    }
}
