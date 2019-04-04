using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchTrigger : MonoBehaviour {

    bool leftHand;
    bool rightHand;
    BoxCollider finishCollider;
    [SerializeField] Animator anim;
    [SerializeField] IKControl ik;


    void Start()
    {
        finishCollider = GetComponent<BoxCollider>();
    }

    public void ActivateTrigger()
    {
        finishCollider.enabled = true;
        ik.ikLook = false;
    }

    public void DeactivateTrigger()
    {
        finishCollider.enabled = false;
        leftHand = false;
        rightHand = false;
        ik.ikLook = true;
        SetPunchBool();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("LeftHand"))
        {
            Debug.Log("Left punch");
            leftHand = true;
        }
        if (col.CompareTag("RightHand"))
        {
            Debug.Log("Right punch");
            rightHand = true;
        }
        SetPunchBool();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("LeftHand"))
        {
            leftHand = false;
        }
        if (col.CompareTag("RightHand"))
        {
            rightHand = false;
        }
        SetPunchBool();
    }
    private void SetPunchBool()
    {
        anim.SetBool("LPunch", leftHand);
        anim.SetBool("RPunch", rightHand);
    }
}
