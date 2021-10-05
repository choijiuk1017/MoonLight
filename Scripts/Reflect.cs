using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public GameObject firstreflector;
    public GameObject Light1;
    public  float maxdistance = 10f;
   

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if(firstreflector.transform.position.x > -3.5 && firstreflector.transform.position.x <3.5)
        {

            Debug.DrawRay(transform.position, new Vector3(10, 0, 0), new Color(0, 1, 0));

            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(10, 0, 0), maxdistance);


            if (hit.collider != null)
            {

                Debug.DrawRay(hit.point, Vector2.Reflect(hit.collider.transform.position, hit.normal), new Color(0, 1, 0));
                Light1.SetActive(true);

            }
            else
            {
                Light1.SetActive(false);
            }
        }
        else
        {
            Light1.SetActive(false);
        }
        

    }
}
