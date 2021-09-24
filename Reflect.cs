using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    
    void Start()
    {

    }

    void FixedUpdate()
    {
        
        
        Debug.DrawRay(transform.position, new Vector3(20, 0, 0), new Color(0, 1, 0));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(20, 0, 0));
        
        if(hit.collider != null)
        {
            
            Debug.DrawRay(hit.point, Vector2.Reflect(hit.collider.transform.position*100, hit.normal), new Color(0,1,0));

        }
        
        
    }
    


}
