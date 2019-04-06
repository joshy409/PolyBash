using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour {

    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject punchingBag;
    [SerializeField] GameObject leftWireFrame;
    [SerializeField] GameObject rightWireFrame;
    [SerializeField] GameObject finishWireFrame;
    [SerializeField] TextMeshPro playerText;
    [SerializeField] TextMeshPro scoreText;
    [SerializeField] TextMeshPro highScoreText;



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            punchingBag.SetActive(false);
            leftWireFrame.SetActive(false);
            rightWireFrame.SetActive(false);
            finishWireFrame.SetActive(false);
            ScoreManager.Reset();
            playerText.enabled = false;
            enemySpawner.enabled = true;
            highScoreText.enabled = true;
            scoreText.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
