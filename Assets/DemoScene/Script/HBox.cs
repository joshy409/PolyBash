using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class HBox : MonoBehaviour
{
    [SerializeField]
    public int duration = 0;
    [SerializeField]
    public float damage = 0;
    [SerializeField]
    public float hitStun = 0;
    public Vector3 launchVector = new Vector3();
    [SerializeField]
    public Vector3 linearVelocity;
    [SerializeField]
    public Vector3 angularVelocity;
    [SerializeField]
    public Vector3 scaleVelocity;
    // Use this for initialization
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (duration <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
        duration--;
        transform.localPosition+=linearVelocity * Time.deltaTime;
        transform.localEulerAngles+=angularVelocity * Time.deltaTime;
        transform.localScale+=angularVelocity * Time.deltaTime;
    }
    void OnCollisionEnter(Collision col)
    {
        Destroy(transform.parent.gameObject);

    }
}
