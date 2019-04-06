using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Movement : MonoBehaviour
{
    public ScoreManager scoreManager;
    private float multiDecayTimer;
    Animator anim;
    IKControl ikControl;
    [SerializeField] float speed = 1;
    [SerializeField] float rotationSpeed = 4;
    [SerializeField] TextMeshPro playerText;
    [SerializeField] IntroSequence introSequence;
    [SerializeField] TextMeshPro sambaText;
    [SerializeField] GameObject startGame;
    [SerializeField] Light sambaLight;
    [SerializeField] AudioSource horns;
    AudioMixerSnapshot sambaSnap;
    AudioMixerSnapshot noSamba;
    public float ringRadius = 8;

    public bool moving = false;

    //Quaternion lastRotation;
    // Use this for initialization
    void Start()
    {
        noSamba = horns.outputAudioMixerGroup.audioMixer.FindSnapshot("NotSamba");
        sambaSnap = horns.outputAudioMixerGroup.audioMixer.FindSnapshot("Samba");
        anim = GetComponent<Animator>();
        ikControl = GetComponent<IKControl>();
        multiDecayTimer = 1;
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

        if (introSequence.startSecondIntroSequence)
        {
            // samba dance
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
            {
                if(playerText.enabled)
                {
                    playerText.text = "Samba Dancing adds multiplier to Score";
                    if (!sambaText.enabled)
                    {
                        sambaText.enabled = true;
                    }
                }
                anim.SetBool("Samba", true);
                moving = false;
                ikControl.ikActive = false;
                sambaLight.enabled = true;
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if (playerText.enabled)
                {
                    playerText.text = "Samba multiplier decays";
                    StartCoroutine(PromptGameStartText());
                }
                anim.SetBool("Samba", false);
                moving = true;
                ikControl.ikActive = true;
                sambaLight.enabled = false;
            }
        
            if (anim.GetBool("Samba"))
            {
                //StartCoroutine(AudioController.FadeIn(horns, 0.3f));
                sambaSnap.TransitionTo(0.5f);
                multiDecayTimer += Time.deltaTime / 2;
                ScoreManager.SetMultiplier((int)multiDecayTimer);
                if (sambaText.enabled) { 
                    sambaText.text = "Samba : " + multiDecayTimer.ToString("F2");
                }
            }
            else
            {
                //StartCoroutine(AudioController.FadeOut(horns, 0.3f));
                noSamba.TransitionTo(0.3f);
                multiDecayTimer -= Time.deltaTime / 3;
                if (multiDecayTimer < 1)
                {
                    multiDecayTimer = 1;
                }
                ScoreManager.SetMultiplier((int)multiDecayTimer);
                if (sambaText.enabled)
                {
                    sambaText.text = "Samba : " + (multiDecayTimer).ToString("F2");
                }
            }
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

    IEnumerator PromptGameStartText()
    {
        yield return new WaitForSeconds(3f);
        playerText.text = "Survive and score as high as possible!";
        if (!startGame.activeSelf) { 
            startGame.SetActive(true);
        }
    }

}