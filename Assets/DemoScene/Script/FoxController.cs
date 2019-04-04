using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public float moveSpeed;
    public Transform cam;
    private Transform tf;
    public Animator animator;
    void Move(Vector2 wishdir)
    {
        Vector3 forward = new Vector3(cam.forward.x, 0, cam.forward.z);
        forward.Normalize();
        Vector3 left = new Vector3(forward.z, 0, -forward.x);
        left.Normalize();
        forward *= wishdir.y * Time.deltaTime * moveSpeed;
        left *= wishdir.x * Time.deltaTime * moveSpeed;
        tf.position += left + forward;
        tf.rotation = Quaternion.LookRotation(left + forward, Vector3.up);
    }


	// Use this for initialization
	void Start ()
    {
        tf = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
            //Debug.Log(Sign(Input.GetAxis("Vertical")));
            //Debug.Log(Sign(Input.GetAxis("Horizontal")));
            animator.SetFloat("DirX", Input.GetAxis("Vertical"));
            animator.SetFloat("DirY", Input.GetAxis("Horizontal"));
            Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        
       
    }

    static float Sign(float rhs)
    {
        
        return rhs / Mathf.Abs(rhs);
    }
}
