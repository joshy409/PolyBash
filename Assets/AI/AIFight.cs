using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIFight : MonoBehaviour {
    [SerializeField] Animator anim;
	// Use this for initialization
	public void Attack()
    {
        anim.SetBool("Swipe", true);
        StartCoroutine(SwipeAttack());
    }

    IEnumerator SwipeAttack()
    {
        while (anim.GetBool("Swipe"))
        {
            yield return null;
        }

        anim.SetBool("Punch", true);
        StartCoroutine(PunchAttack());

        // this will get here when Swipe is false
        //GetComponent<NavMeshAgent>().isStopped = false; NEED TO BE HANDLED LATER
        GetComponent<AIController>().isAttacking = false;
    }

    IEnumerator PunchAttack()
    {
        while (anim.GetBool("Punch"))
        {
            //anim.SetBool("Swipe", false);
            yield return null;
        }
        // this will get here when Punch is false
        //GetComponent<NavMeshAgent>().isStopped = false;
        //GetComponent<AIController>().isAttacking = false;
    }

    public void SwipeAnimationEnded()
    {
        anim.SetBool("Swipe", false);
    }

    public void PunchAnimationEnded()
    {
        anim.SetBool("Punch", false);
    }
}
