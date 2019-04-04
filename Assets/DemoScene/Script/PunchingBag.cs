using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PunchingBag : MonoBehaviour {


    NavMeshAgent bagAI;
	// Use this for initialization
	void Start () {
        bagAI = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (bagAI.enabled)
        {
            bagAI.destination = new Vector3(0, 0, 0);
        } else
        {
            bagAI.enabled = true;
        }
	}
}
