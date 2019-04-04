using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Status : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TextMeshPro playerText;
    public bool isDown = true;

    Animator anim;
    Movement movement;
    IKControl ik;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        ik = GetComponent<IKControl>();
    }
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            anim.SetBool("Death", true);
            isDown = true;
            ik.ikActive = false;
            ik.ikLook = false;
            movement.moving = false;

            //StartCoroutine(RestartScene());
        }

        if (isDown)
        {
            if (OVRInput.Get(OVRInput.Button.One))
            {
                health = 100;
                anim.SetBool("GetUp",true);
                anim.SetBool("Death", false);

                if (enemySpawner.enabled == false)
                {
                    enemySpawner.enabled = true;
                }
            }
        }

	}

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            health -= 100;
        }
    }
    public void GettingUpAnimationEnded()
    {
        anim.SetBool("GetUp", false);
        isDown = false;
        ik.ikActive = true;
        ik.ikLook = true;
        movement.moving = true;
        if (playerText.enabled)
        {
            playerText.text = "Put Both of Hand Inside the Trigger Box";
        }
    }
}

