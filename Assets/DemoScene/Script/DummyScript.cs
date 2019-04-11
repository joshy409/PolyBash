using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyScript : MonoBehaviour
{

    private float grav;
    public float drag;
    private float bounceMod;
    private Vector3 velocity;
    private Transform tf;
    //private Vector3 startingPosition;
    public float floorHeight;
    private float bounce;
    private float toGround;
    private float stunTimer;
    private float _hitStun;
    public float hitDmg;
    public Vector3 hitDir;
    const float ONE_SIXTIETH = 0.16666f;
    public float ringRadius = 8;
    NavMeshAgent nav;
    private AIStatus aiStatus;
    private AIController aiController;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        aiStatus = GetComponent<AIStatus>();
        aiController = GetComponent<AIController>();
        tf = GetComponent<Transform>();
        velocity = new Vector3(0, 0, 0);
        //startingPosition = tf.position;
        nav = transform.GetComponent<NavMeshAgent>();
        grav = 7.84f;
        bounceMod = 0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        stunTimer--;
        if (stunTimer < 0)
        {
            stunTimer = 0;
            if(aiController != null) { 
                if (nav && tf.position.y<=floorHeight+Mathf.Epsilon && aiController.WantsNavAgentEnabled())
                {
                    nav.enabled = true;
                }    
            }   
            //tf.position = startingPosition;
            //velocity = new Vector3(0, 0, 0);
            //TODO: re enable AI
        } 
        
        launch();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit(hitDmg, hitDir, 60.0f);
        }
        Vector2 flattendVector = new Vector2(tf.position.x, tf.position.z);
        //print(flattendVector.magnitude);

        if (flattendVector.magnitude > ringRadius)
        {
            flattendVector.Normalize();
            flattendVector *= ringRadius;
            tf.position = new Vector3(flattendVector.x, tf.position.y, flattendVector.y);
        }


    }

    /*void OnCollisonEnter(Collider other) //TODO: move this code to HBox
    {
        HBox hbox = other.gameObject.GetComponent<HBox>();
        if (hbox)
        {
            Vector3 transformedLaunchVector = hbox.transform.localToWorldMatrix.MultiplyVector(hbox.launchVector);
            Hit(hbox.damage, transformedLaunchVector, hbox.hitStun);
            Debug.Log("trigger");
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        HBox hbox = other.gameObject.GetComponent<HBox>();
        if (hbox)
        {
            Vector3 transformedLaunchVector = hbox.transform.localToWorldMatrix.MultiplyVector(hbox.launchVector);
            Hit(hbox.damage, transformedLaunchVector, hbox.hitStun);
        }
    }

    public void Hit(float damage, Vector3 launchVector, float hitStun)
    {
        velocity = launchVector;// * damage;
        stunTimer = hitStun;
        _hitStun = hitStun;
        nav.enabled = false;
        //  sm.score += (int)launchVector.magnitude;
        aiStatus.TakeDamage(damage);
        audioSource.Play();
    }

    void launch()
    {
        velocity = new Vector3(velocity.x * ((_hitStun>0)?(stunTimer * 1.01f / _hitStun):(1))/*- (velocity.x * drag * Time.deltaTime)*/, velocity.y - (grav * Time.deltaTime), velocity.z * ((_hitStun > 0) ? (stunTimer * 1.01f / _hitStun) : (1)) /*- (velocity.z *  drag * Time.deltaTime)*/);
        //print(velocity);
        //Debug.Log(velocity);
        //Starting positiong based floor detection
        //if (tf.position.y + velocity.y < 0)
        //{
        //    tf.position = new Vector3(tf.position.x, 0, tf.position.z);
        //    velocity.y = -velocity.y * bounceMod;
        //    if (velocity.y < grav * Time.deltaTime)
        //    {
        //        velocity.y = 0;
        //    }
        //}
        if (tf.position.y <= floorHeight || tf.position.y + (velocity.y * Time.deltaTime) <= floorHeight)//the or is to prevent a charecter from going under to floor because it's velocity is positive
        {
            grav = 7.84f;
            if (Mathf.Abs(velocity.y) < grav * ONE_SIXTIETH || stunTimer <= 0)//not based on delta time, this is to prevent frame rate changes from changing the number of jumps
            {
                velocity.y = 0;
                //if(!tf.GetComponent<AIStatus>().isSinking)tf.position = new Vector3(tf.position.x, floorHeight, tf.position.z);
                //grav = 7.84f;
            } else
            {
                velocity.y = Mathf.Abs(velocity.y) * bounceMod;
            }

        }

        if (velocity.x < drag * ONE_SIXTIETH && velocity.x > -drag * ONE_SIXTIETH || (stunTimer <= 0 && tf.position.y <= floorHeight+Mathf.Epsilon))//removed reference to deltatime, this is to prevent high framerates from taking longer to stop and low frame rates from being stopped to quickly
        {
            velocity.x = 0;
        }
        if (velocity.z < drag * ONE_SIXTIETH && velocity.z > -drag * ONE_SIXTIETH || (stunTimer <= 0 && tf.position.y <= floorHeight + Mathf.Epsilon))
        {
            velocity.z = 0;
        }

        tf.position += velocity * Time.deltaTime;

        if (!aiStatus.isSinking) tf.position = new Vector3(tf.position.x, Mathf.Max(tf.position.y, floorHeight), tf.position.z);
        
        if (stunTimer != 0)
        {
            grav += (_hitStun * 1.01f / stunTimer);
        }
    }

    public float Sign(float rhs)
    {

        return rhs / Mathf.Abs(rhs);
    }
}
