using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    private Vector3 lookPoint;

    public bool ikActive = false;
    public Transform leftHandObj = null;
    public Transform rightHandObj = null;
    public Transform referenceObj = null; // VR camera transform
    public Transform baseObj = null; // head of the character
    public bool ikLook = true;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {

        if (animator)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {

                // Set the look target position, if one has been assigned
                if (ikLook)
                {
                    RaycastHit hit;
                    Physics.Raycast(referenceObj.transform.position, referenceObj.transform.forward, out hit, 100f);
                    Debug.DrawRay(referenceObj.transform.position, referenceObj.transform.forward * 1000, Color.yellow);

                    if (hit.point.magnitude < 1) {
                        lookPoint = referenceObj.transform.forward * 1000f;//look in the direction the user is looking in
                    } else
                    {
                        lookPoint = hit.point;//look at the point the user is looking it
                    }
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookPoint);
                } else
                {
                    animator.SetLookAtWeight(0);
                }

                float angle = transform.rotation.eulerAngles.y;

                // Set the right hand target position and rotation, if one has been assigned
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

                    Vector3 rightHandPos = Quaternion.AngleAxis(angle, Vector3.up) * (rightHandObj.position - referenceObj.position);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, baseObj.position + rightHandPos);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, transform.localRotation * rightHandObj.rotation);
                }

                // Set the left hand target position and rotation, if one has been assigned
                if (leftHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                   
                    Vector3 leftHandPos = Quaternion.AngleAxis(angle, Vector3.up) * (leftHandObj.position - referenceObj.position);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, baseObj.position + leftHandPos);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, transform.localRotation * leftHandObj.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}
