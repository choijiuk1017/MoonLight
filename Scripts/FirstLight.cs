using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLight : MonoBehaviour
{
    public GameObject FirstReflector;
    public GameObject light1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FirstReflector.transform.position.x > -1*3.5 && FirstReflector.transform.position.x < 3.5)
        {
            light1.SetActive(true);
        }
        else
        {
            light1.SetActive(false);

        }
    }
}
