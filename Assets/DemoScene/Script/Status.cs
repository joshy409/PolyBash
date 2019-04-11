using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Status : MonoBehaviour {

    [SerializeField] float health = 100;
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] TextMeshPro playerText;
    [SerializeField] GameObject rightHadukenTrigger;
    [SerializeField] Animator fadeOut;
    [SerializeField] GameObject startGame;
    public bool isDown = true;
    public bool gameOver = false;
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
        if (!gameOver) { 
		    if (health <= 0)
            {
                gameOver = true;
                anim.SetBool("Death", true);
                ik.ikActive = false;
                ik.ikLook = false;
                movement.moving = false;
                enemySpawner.enabled = false;
                fadeOut.SetBool("Fade", true);
            }
        }

        if (isDown)
        {
            if (OVRInput.Get(OVRInput.Button.One) || OVRInput.Get(OVRInput.Button.Two) 
                || OVRInput.Get(OVRInput.Button.Three) || OVRInput.Get(OVRInput.Button.Four))
            {
                health = 100;
                anim.SetBool("GetUp",true);
                anim.SetBool("Death", false);
                gameOver = false;
                movement.Reset();
            }
        }

	}
    //gets called when fade out animation finishes
    public void RestartGame()
    {
        transform.position = new Vector3(0,0,0);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("DestroyEnemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }
        fadeOut.SetBool("Fade",false);
        isDown = true;
        movement.moving = false;
        ScoreManager.Reset();
        startGame.SetActive(true);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Enemy"))
        {
            health -= 100;
        }
    }
    //gets called when getting up animation finished
    public void GettingUpAnimationEnded()
    {
        anim.SetBool("GetUp", false);
        isDown = false;
        ik.ikActive = true;
        ik.ikLook = true;
        movement.moving = true;
        if (playerText.enabled)
        {
            playerText.text = "Character Mirrors your hand movements!\nPut Both of Hand Inside the Trigger Box";

            rightHadukenTrigger.GetComponent<BoxCollider>().enabled = true;
        }
    }
}

