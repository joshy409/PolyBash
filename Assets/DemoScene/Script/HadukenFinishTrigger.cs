using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadukenFinishTrigger : MonoBehaviour {

	[SerializeField] MoveSet haduken;
    bool leftHand;
    bool rightHand;
    BoxCollider finishCollider;

    void Start()
    {
        finishCollider = GetComponent<BoxCollider>();
    }
    public void ActivateTrigger() // starts a Corutine that turns off the collider after 1 seconds
    {
        finishCollider.enabled = true;
        StartCoroutine(DisableTriggerAfterWait());
    }

    IEnumerator DisableTriggerAfterWait()
    {
        yield return new WaitForSeconds(1f);
        finishCollider.enabled = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("LeftHand"))
        {
            leftHand = true;
        }
        else if (col.CompareTag("RightHand"))
        {
            rightHand = true;
        }
        if (leftHand && rightHand) // if the player finishs the haduken motion finish the haduken animation
        {

            finishCollider.enabled = false;

            leftHand = false;
            rightHand = false;

            haduken.startAttack(0);
        }

    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("LeftHand"))
        {
            leftHand = false;
        }
        else if (col.CompareTag("RightHand"))
        {
            rightHand = false;
        }

    }
}
