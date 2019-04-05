using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroSequence : MonoBehaviour {

    // Use this for initialization
    [SerializeField] GameObject player;
    [SerializeField] TextMeshPro playerText;
    [SerializeField] TextMeshPro dummyText;
    IKControl ik;
    Movement move;

    public bool startSecondIntroSequence = false;

    void Start () {
        ik = player.GetComponent<IKControl>();
        move = player.GetComponent<Movement>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.rotation.x < -.01f)
        {
            ik.ikActive = false;
            move.moving = false;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), 8 * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.time * .01f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            if (!player.GetComponent<Status>().enabled)
            {
                player.GetComponent<Status>().enabled = true;
                playerText.enabled = true;
                dummyText.enabled = true;
            }
        } 
	}
}
