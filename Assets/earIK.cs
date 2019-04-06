using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class earIK : MonoBehaviour {
    public Transform earBaseBone;

    public Transform earMiddleBone;

    public Transform earTipBone;

    public Transform earBase;
    
    public Transform earMiddle;
    
    public Transform earTip;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

        //earBaseBone.LookAt(earBase.position);

        //earMiddleBone.LookAt(earMiddle.position);
        //earTipBone.LookAt(earTip.position);
        earBaseBone.up = earMiddle.position - earBase.position;
        //if (Vector3.Dot(earBaseBone.right, transform.forward) > 0)
        //{
        //    //bad rotation flip it around
        //    earBaseBone.Rotate(earBaseBone.up, 180,Space.World);

        //    //earBaseBone.Rotate(earBaseBone.up, 180, Space.Self);
        //    //earBaseBone.RotateAroundLocal();
        //}
        //earBaseBone.right = 
        Debug.DrawRay(earBaseBone.position, Vector3.ProjectOnPlane(-transform.forward, earBaseBone.up).normalized, Color.yellow);
        earBaseBone.Rotate(earBaseBone.up,Vector3.Angle(earBaseBone.right, Vector3.ProjectOnPlane(-transform.forward, earBaseBone.up).normalized),Space.World);
        
        Debug.DrawRay(earBaseBone.position, earBaseBone.forward, Color.red);
        Debug.DrawRay(earBaseBone.position, earBaseBone.up, Color.green);
        Debug.DrawRay(earBaseBone.position, earBaseBone.right, Color.blue);
        
    }
}
