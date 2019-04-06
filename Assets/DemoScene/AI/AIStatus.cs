using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStatus : MonoBehaviour {

    //[SerializeField] public ScoreManager scoreManager;
    [SerializeField] float health = 100;
    [SerializeField] float sinkSpeed = 1f;
    private ScoreManager scoreManager;
    Animator anim;
    NavMeshAgent navi;
    public bool isSinking = false;
    private bool isAlive = true;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        navi = GetComponent<NavMeshAgent>();
        scoreManager = GameObject.Find("Player").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) { 
            if (health <= 0)
            {
                scoreManager.AddScore(100);
                anim.SetBool("Death", true);
                isAlive = false;
                navi.enabled = false;
            }
        }
        if (isSinking)
        {
            navi.enabled = false;
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime, Space.World);
        }
    }
    public void AIDeath()
    {
        isSinking = true;
        StartCoroutine(SinkAndDestroy());
    }
    IEnumerator SinkAndDestroy()
    {
        print("AI Died");
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject,1f);
    }


    public void TakeDamage(float damage)
    { 
        health -= damage;
    }

    void OnTriggerEnter(Collider col)
    {
        //if (col.CompareTag("LeftHand") || col.CompareTag("RightHand"))
        //{
        //    health -= 100;
        //}
    }

    public void MutantDeathEnd()
    {
        anim.SetBool("Death", false);
        anim.SetBool("Punch", false);
        anim.SetBool("Swipe", false);
    }
}
                                       