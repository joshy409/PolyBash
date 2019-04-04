using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireFrameController : MonoBehaviour {


    [SerializeField] GameObject trigger;
    // Use this for initialization
    MeshRenderer mesh;
	void Start () {
        mesh = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        mesh.enabled = trigger.GetComponent<BoxCollider>().enabled;
        
	}
}
