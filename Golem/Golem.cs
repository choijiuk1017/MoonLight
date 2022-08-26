using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem : MonoBehaviour
{
    public enum State { Idle, Tracking, TheRock, BoongBoong, ShotGun, DameDame}

    public State state = State.Idle;

    public float traceDist = 150f;

    private NavMeshAgent agent;

    public Animator anim;

    public GameObject player;

    private Transform transform;

    int randomNum;

    private bool isDead = false;

    
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();

        anim = GetComponentInChildren<Animator>();

        StartCoroutine("CheckState");

        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());
    }

    // Update is called once per frame
    void Update()
    {
    }

    
    IEnumerator CheckState()
    {
        while(!isDead)
        {

            float distance = Vector3.Distance(player.transform.position, transform.position);
            randomNum = Random.Range(1, 11);
            Debug.Log(randomNum);

            yield return new WaitForSeconds(1f);

            if (distance <= 190f && distance > 130f)
            {
                if (randomNum <= 4)
                {
                    state = State.TheRock;
                }
                else
                {
                    state = State.Tracking;
                }
            }
            else if (distance <= 250f)
            {
                state = State.Tracking;
            }
            else
            {
                state = State.Idle;
            }

            if (distance <= 100f)
            {
                if (randomNum == 5 || randomNum == 7)
                {
                    state = State.BoongBoong;
                }
                else if(randomNum == 6 || randomNum == 9)
                {
                    state = State.ShotGun;
                }
                else if(randomNum == 8 || randomNum == 10)
                {
                    state = State.DameDame;
                }
                else
                {
                    state = State.Tracking;
                }
                
            }
        }
    }

    IEnumerator CheckStateForAction()
    {
        while(!isDead)
        {
            switch (state)
            {
                case State.Idle:
                    Idle();
                    break;

                case State.Tracking:
                    Trace();
                    agent.Resume();
                    break;

                case State.TheRock:
                    Rock();
                    Invoke("StopRock", 1f);
                    break;

                case State.BoongBoong:
                    BoongBoong();
                    Invoke("StopBoong", 1f);
                    break;

                case State.ShotGun:
                    ShotGun();
                    Invoke("StopShotGun", 1f);
                    break;
            }

            yield return null;
        }
    }

    public void Idle()
    {
        agent.Stop();
        anim.SetBool("isWalk", false);
    }

    void BoongBoong()
    {
        anim.SetTrigger("isBoong");
        agent.ResetPath();
    }

    void StopBoong()
    {
        anim.ResetTrigger("isBoong");
        agent.SetDestination(player.transform.position);
    }

    void DameDame()
    {
        anim.SetTrigger("isDame");
        agent.ResetPath();
    }

    void ShotGun()
    {
        anim.SetTrigger("isShot");
        agent.ResetPath();
    }

    void StopShotGun()
    {
        anim.ResetTrigger("isShot");
        agent.SetDestination(player.transform.position);
    }

    void Trace()
    {
        agent.SetDestination(player.transform.position);
        anim.SetBool("isWalk", true);
    }

    void Rock()
    {
        anim.SetTrigger("isRock");
        anim.SetBool("isWalk", false);
        agent.ResetPath();
    }

    void StopRock()
    {
        anim.ResetTrigger("isRock");
        agent.SetDestination(player.transform.position);
    }

}
