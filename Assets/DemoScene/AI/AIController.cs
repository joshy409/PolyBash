using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {
    [SerializeField] float attackDistance = 2f;

    GameObject player;
    NavMeshAgent navi;
    Animator anim;
    AIFight fight;
    public bool isAttacking = false;
    [SerializeField] CapsuleCollider leftHandTrigger;
    [SerializeField] CapsuleCollider rightHandTrigger;

    void Start()
    {
        navi = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fight = GetComponent<AIFight>();
        leftHandTrigger.enabled = false;
        rightHandTrigger.enabled = false;
    }

    void Update()
    {
        if (DistanceToPlayer() <= attackDistance)
        {
            if (!isAttacking)
            {
                //navi.isStopped = true; NEEDS TO BE HANDLED
                isAttacking = true;
                fight.Attack();
            }
        }
        else
        {
            if(navi.enabled && !player.GetComponent<Status>().isDown)navi.destination = player.transform.position;
            anim.SetFloat("forwardSpeed", transform.InverseTransformDirection(navi.velocity).z);
        }

        if (player.GetComponent<Status>().gameOver)
        {
            navi.enabled = false;

        } else
        {
            //navi.enabled = true;
        }

        if (isAttacking)
        {
            leftHandTrigger.enabled = true;
            rightHandTrigger.enabled = true;
        } else
        {
            leftHandTrigger.enabled = false;
            rightHandTrigger.enabled = false;
        }

    }

    float DistanceToPlayer()
    {
        player = GameObject.FindWithTag("Player");
        return Vector3.Distance(player.transform.position, transform.position);
    }
    public bool WantsNavAgentEnabled()
    {
        return !(player.GetComponent<Status>().isDown || DistanceToPlayer() <= attackDistance);
    }
}
