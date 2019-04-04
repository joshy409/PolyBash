using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform enemy;
    public float spawnTimerDefault = 5;
    private float spawnTimer = 0;
    //public ScoreManager scoreMan;
    // Update is called once per frame
    void Update()
    {
        spawnTimer-=Time.deltaTime;
        if (spawnTimer <= 0) {
            Vector2 spawnpoint = Random.insideUnitCircle*6;
            GameObject g = Instantiate(enemy.gameObject, new Vector3(spawnpoint.x, 0, spawnpoint.y), Quaternion.identity);
            //g.GetComponent<AIStatus>().scoreManager = scoreMan;
            spawnTimer = spawnTimerDefault;
        }
    }
}
