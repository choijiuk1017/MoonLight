using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheRock : Golem
{
    public float overlapRadius2;
    public Collider[] target2;

    void Start()
    {
        
    }

    void Update()
    {
        if(theRock)
        {
            Rock();
            Invoke("StopRock", 1.5f);
        }


        target2 = Physics.OverlapSphere(transform.position, overlapRadius2, whatIsLayer);

        if (target2.Length > 0)
        {
            state = State.TheRock;
        }
        else
        {
            state = (State)Random.Range(0,1);
            stateChange = false;
            return;
        }
    }

    void Rock()
    {
        anim.SetTrigger("isRock");
        
    }

    void StopRock()
    {
        anim.ResetTrigger("isRock");
    }

    void OnDrawGizmosSelected()
    {
        //충돌범위 기즈모
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, overlapRadius2);
    }
}
