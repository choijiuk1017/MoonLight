using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    public GameObject Monster;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            biteStart();
            Invoke("biteEnd", 1f);
        }
    }


    void biteStart()
    {
        Monster.GetComponent<Animator>().SetTrigger("isBite");
        Monster.GetComponent<Animator>().SetBool("isWalk", false);
    }

    void biteEnd()
    {
        Monster.GetComponent<Animator>().SetBool("isWalk", true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
