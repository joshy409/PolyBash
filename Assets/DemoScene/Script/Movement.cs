using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public ScoreManager scoreManager;
    private float sambaMultiplierTimer;
    private float multiDecayTimer;
    Animator anim;
    IKControl ikControl;
    [SerializeField] float speed = 1;
    [SerializeField] float rotationSpeed = 4;
    [SerializeField] TextMeshPro playerText;
    public float ringRadius = 8;

    public bool moving = false;

    //Quaternion lastRotation;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        ikControl = GetComponent<IKControl>();
        sambaMultiplierTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            Move();

            //limits play area to white ring
            Vector2 flattendVector = new Vector2(transform.position.x, transform.position.z);
            //print(flattendVector.magnitude);

            if (flattendVector.magnitude > ringRadius)
            {
                flattendVector.Normalize();
                flattendVector *= ringRadius;
                transform.position = new Vector3(flattendVector.x, transform.position.y, flattendVector.y);
            }
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            anim.SetBool("Samba", true);
            moving = false;
            ikControl.ikActive = false;
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            anim.SetBool("Samba", false);
            moving = true;
            ikControl.ikActive = true;
        }
            
        if(anim.GetBool("Samba"))
        {
            multiDecayTimer = 0;
            sambaMultiplierTimer += Time.deltaTime;
            if(sambaMultiplierTimer >= 2)
            {
                sambaMultiplierTimer = 0;
                scoreManager.AddMultiplier(1);
            }
            playerText.text = "Samba : " + ((float)ScoreManager.multiplier + (sambaMultiplierTimer / 2)).ToString("F2");
        }
        else
        {
            sambaMultiplierTimer = 0;
            multiDecayTimer += Time.deltaTime;
            if (multiDecayTimer >= 2)
            {
                multiDecayTimer = 0;
                scoreManager.AddMultiplier(-1);
            }
            playerText.text = "Samba : " + ((float)ScoreManager.multiplier + (multiDecayTimer / 2)).ToString("F2");
        }
    }

    private void Move()
    {
        var leftJoystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        var rightJoystick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        transform.Translate(Vector3.forward * leftJoystick.y * Time.deltaTime * speed, Space.World);
        transform.Translate(Vector3.right * leftJoystick.x * Time.deltaTime * speed, Space.World);
       
        Rotate(rightJoystick);

        var direction = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.forward)  * leftJoystick;
        direction = Vector3.Normalize(direction);

        UpdateAnimation(direction);
    }

    private void Rotate(Vector2 rightJoystick)
    {

        if (rightJoystick.x == 0 && rightJoystick.y == 0)
        {
            return;
            //transform.rotation = lastRotation; this prevents external effects from influencing rotation, however it also is slower than just returning. that is why i removed it
        }
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(rightJoystick.x, 0, rightJoystick.y));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //lastRotation = transform.rotation; //redundent
    }

    private void UpdateAnimation(Vector3 direction)
    {
        anim.SetFloat("DirX", direction.x, 1f, Time.deltaTime * 10f);
        anim.SetFloat("DirY", direction.y, 1f, Time.deltaTime * 10f);
        
    }



}