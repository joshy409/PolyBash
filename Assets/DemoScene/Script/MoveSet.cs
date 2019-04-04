using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
using TMPro;

public class MoveJS
{
    public int duration { get; set; }
    public HitboxJS[] hitboxes { get; set; }
}

public class HitboxJS
{
    public string name { get; set; }
    public int beginFrame { get; set; }
    public int duration { get; set; }
    public float damage { get; set; }
    public int hitStun { get; set; }
    public float[] launchVector { get; set; }
    public float[] rotation { get; set; }
    public float[] scale { get; set; }
    public float[] offset { get; set; }
    public float[] linearVelocity { get; set; }
    public float[] angularVelocity { get; set; }
    public float[] scaleVelocity { get; set; }
}

public class Move
{
    public int duration;
    public GhostHBox[] hitboxes;
    public void Execute(int frame)
    {
        foreach( GhostHBox ghb in hitboxes) //ghb = ghost hit box
        {
            if(ghb.beginFrame==frame) ghb.CreateReal();
        }
    }
}
public class MoveSet : MonoBehaviour
{
    public Transform prefab;
    [SerializeField]
    string[] moveFiles;
    Move[] moves;
    int currentMove = -1;
    int currentFrame = -1;

    [SerializeField] Movement movement;
    [SerializeField] GameObject rightHadukenTrigger;
    [SerializeField] GameObject leftHadukenTrigger;
    [SerializeField] TextMeshPro playerText;

    public bool isLeft = false;
    public bool isRight = false;

    Animator anim;
    IKControl ikContorl;
    BoxCollider rightTrigger;
    BoxCollider leftTrigger;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        ikContorl = GetComponent<IKControl>();
        rightTrigger = rightHadukenTrigger.GetComponent<BoxCollider>();
        leftTrigger = leftHadukenTrigger.GetComponent<BoxCollider>();

        moves = new Move[moveFiles.Length];
        for(int i = 0; i<moves.Length; i++)
        {
            moves[i]=new Move();
        }
        /*for (int i = 0; i < moveFiles.Length; i++ )
        {
            var hitboxes = importAttack(moveFiles[i]).hitboxes;
            foreach (HitboxJS  hbox in hitboxes)
            {
                //GameObject go = new GameObject();
                //go.AddComponent<MeshFilter>(
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.SetParent(transform);
                Vector3 offset = floatVectorToVector(hbox.offset);
                offset.Scale(transform.localScale);
                go.transform.localPosition = offset;
                go.transform.eulerAngles = floatVectorToVector(hbox.rotation);
                Vector3 scale = floatVectorToVector(hbox.scale);
                scale.Scale(transform.localScale);
                go.transform.localScale = scale;

            }
        }*/
        for (int i = 0; i < moveFiles.Length; i++)
        {
            MoveJS move = importAttack(moveFiles[i]);
            var hitboxes = move.hitboxes;
            moves[i].hitboxes = new GhostHBox[hitboxes.Length];
            moves[i].duration = move.duration;
            for (int j = 0; j<hitboxes.Length;j++)
            {
                Transform temp = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                GhostHBox g = temp.GetComponent<GhostHBox>();
                temp.SetParent(transform);
                temp.localScale = floatVectorToVector(hitboxes[j].scale);
                temp.localEulerAngles = floatVectorToVector(hitboxes[j].rotation);
                temp.localPosition = floatVectorToVector(hitboxes[j].offset);
                temp.name = hitboxes[j].name;
                g.beginFrame= hitboxes[j].beginFrame;
                g.duration= hitboxes[j].duration;
                g.damage= hitboxes[j].damage;
                g.hitStun= hitboxes[j].hitStun;
                g.launchVector=floatVectorToVector(hitboxes[j].launchVector);
                g.linearVelocity=floatVectorToVector(hitboxes[j].linearVelocity);
                g.angularVelocity=floatVectorToVector(hitboxes[j].angularVelocity);
                g.scaleVelocity=floatVectorToVector(hitboxes[j].scaleVelocity);
                moves[i].hitboxes[j]=g;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        //if ((OVRInput.Get(OVRInput.RawButton.LIndexTrigger) || OVRInput.Get(OVRInput.RawButton.RIndexTrigger)) && currentMove == -1)
        //{
        //    startAttack(0);
        //}
        itterateAttack();
    }
    MoveJS importAttack(string pathField)
    {
        string json = File.ReadAllText(pathField);
        MoveJS root = JsonConvert.DeserializeObject<MoveJS>(json);
        return root;
    }
    Vector3 floatVectorToVector(float[] floatarr)
    {
        return new Vector3(floatarr[0], floatarr[1], floatarr[2]);
    }
    public void startAttack(int attackNumber)
    {
        ikContorl.ikActive = false;
        movement.moving = false;
        rightTrigger.enabled = false;
        leftTrigger.enabled = false;
        //print("attack " + attackNumber + " Started");
        currentMove = attackNumber;
        currentFrame = 0;
    }
    void itterateAttack()
    {
        if(currentMove == -1) return;
        //print("attackItterated");
        moves[currentMove].Execute(currentFrame);
        currentFrame++;

        if (isRight)
        {
            anim.Play("RightHaduken", 0, (float)currentFrame / moves[currentMove].duration);
        }
        else if (isLeft)
        {
            anim.Play("LeftHaduken", 0, (float)currentFrame / moves[currentMove].duration);
        }


        if (currentFrame > moves[currentMove].duration)
        {
            currentMove = -1;
            currentFrame = -1;
            ikContorl.ikActive = true;
            movement.moving = true;
            rightTrigger.enabled = true;
            leftTrigger.enabled = true;
            isLeft = false;
            isRight = false;
            playerText.text = "Haduken!!!!!!!!!!!!";
        }
    }
}