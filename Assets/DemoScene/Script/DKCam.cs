using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DKCam : MonoBehaviour
{
    private float rot;
    public Transform rotFocus;
    public float camVelocity;
    public float rotVelocity;
    private Transform tf;
    
	// Use this for initialization
	void Start ()
    {
        tf = GetComponent<Transform>();
        tf.position = rotFocus.position;
        rot = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) { }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rot += rotVelocity * Time.deltaTime;
            tf.rotation = Quaternion.Euler(0, rot, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rot -= rotVelocity * Time.deltaTime;
            tf.rotation = Quaternion.Euler(0, rot, 0);
        }

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            //Debug.Log(Sign(Input.GetAxis("Vertical")));
            //Debug.Log(Sign(Input.GetAxis("Horizontal")));
            
            Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        }
        
    }

    void Move(Vector2 wishdir)
    {
        Vector3 forward = new Vector3(tf.forward.x, 0, tf.forward.z);
        forward.Normalize();
        Vector3 left = new Vector3(forward.z, 0, -forward.x);
        left.Normalize();
        forward *= wishdir.y * Time.deltaTime * camVelocity;
        left *= wishdir.x * Time.deltaTime * camVelocity;
        tf.position += left + forward;

    }
}
