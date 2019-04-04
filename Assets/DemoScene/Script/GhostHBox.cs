using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class GhostHBox : MonoBehaviour
{
    [SerializeField]
    public int beginFrame = 0;
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
    public Transform fireball;
    // Use this for initialization
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
    }
    public void CreateReal()
    {
        //print("lmao.exe");
        GameObject go = new GameObject();

        go.transform.position = transform.parent.position;
        go.transform.localScale = transform.parent.localScale;
        go.transform.rotation = transform.parent.rotation;
        //go.par
        GameObject hbox = GameObject.CreatePrimitive(PrimitiveType.Cube);
        HBox hb = hbox.AddComponent<HBox>();
        hbox.GetComponent<Renderer>().enabled = false;
        hbox.transform.SetParent(go.transform);
        hb.duration = duration;
        hb.damage = damage;
        hb.hitStun = hitStun;
        hb.launchVector = launchVector;
        hb.linearVelocity = linearVelocity;
        hb.angularVelocity = angularVelocity;
        hb.scaleVelocity = scaleVelocity;
        hbox.transform.localPosition = transform.localPosition;
        hbox.transform.localRotation = transform.localRotation;
        hbox.transform.localScale = transform.localScale;
        if (linearVelocity.magnitude >= 0)
        {
            var fb = Instantiate(fireball, new Vector3(), Quaternion.identity);
            fb.SetParent(hbox.transform);
            fb.localPosition = new Vector3();

        }
    }
}
