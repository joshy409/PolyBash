using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireFrameController : MonoBehaviour {

    // Use this for initialization

    [SerializeField] GameObject trigger;
    MeshRenderer mesh;

	void Start () {
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        mesh.enabled = trigger.GetComponent<BoxCollider>().enabled;
        
	}
}
