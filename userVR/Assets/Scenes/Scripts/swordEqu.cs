using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordEqu : MonoBehaviour {
    //public float equAngle;
    public GameObject sword;
    public bool killable = false;
    
    private Vector3 userElbowRightPos = Vector3.zero;
    private Vector3 userShoulderRightPos = Vector3.zero;
    private Vector3 userHandRightPos = Vector3.zero;
    private Vector3 dir = Vector3.zero;
    private bool triggered = false;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        userHandRightPos = userHandCollider.rHandPos;
        userElbowRightPos = userHandCollider.rElbowPos;
        userShoulderRightPos = userHandCollider.rShoulderPos;

        dir = userShoulderRightPos + userHandRightPos - 2 * userElbowRightPos;// the direction of the sword (TBD)
        dir.Normalize();

        if (triggered)
        {
            sword.transform.position = userHandRightPos;
            sword.transform.LookAt(userHandRightPos + dir);
            sword.transform.Rotate(new Vector3(0,0,90));
            sword.transform.Translate(new Vector3(0,0,-0.3f));// held in hand
        }

    }

    
    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "HandRightCollider")
        {
            triggered = true;
            killable = true;
        }
    }
    void OnTriggerStay(Collider collision)
    {
    }
    void OnTriggerExit(Collider collision)
    {
    }
}
