using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HadukenTrigger : MonoBehaviour {

    [SerializeField] MoveSet haduken;
    [SerializeField] HadukenFinishTrigger hadukenFinish;
    [SerializeField] TextMeshPro playerText;
    [SerializeField] GameObject leftWireFrame;
    [SerializeField] GameObject rightWireFrame;

    //booleans that check if each hand is touching the trigger
    bool leftHand;
    bool rightHand;

    Quaternion rotation;

    BoxCollider hadukenTriggerCollider;

    void Start()
    {
        hadukenTriggerCollider = GetComponent<BoxCollider>();
    }
    public void ActivateHadukenTrigger()
    {
        print("Haduken Trigger Enabled");
        hadukenTriggerCollider.enabled = true;
    }

    public void DeactivateHadukenTrigger()
    {
        hadukenTriggerCollider.enabled = false;
    }

	void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("LeftHand"))
        {
            leftHand = true;
        }  else if (col.CompareTag("RightHand")) {
            rightHand = true;
        }
        if (leftHand && rightHand)
        {
            hadukenFinish.ActivateTrigger();
            leftHand = false;
            rightHand = false;
            if (playerText.enabled)
            {
                playerText.text = "Now Put Both Hand Forward!";
            }
            if (gameObject.name == "RightHadukenTrigger")
            {
                haduken.isRight = true;
            }
            if (gameObject.name == "LeftHadukenTrigger")
            {
                haduken.isLeft = true;
            }
        }

    }


    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("LeftHand"))
        {
            leftHand = false;
        } else if (col.CompareTag("RightHand"))
        {
            rightHand = false;
        }
        
    }

}
