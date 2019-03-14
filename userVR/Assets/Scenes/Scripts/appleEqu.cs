using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appleEqu : MonoBehaviour {

    public GameObject apple;
    public static Vector3 ApplePos;
    public static bool eaten = false;

    private Vector3 userHandRightPos = Vector3.zero;
    private bool triggered = false;
    private Transform[] ways;

    // Use this for initialization
    void Start()
    {
        //ways = wayPoints.Points;///////////////////////
    }

    // Update is called once per frame
    void Update()
    {
        ApplePos = apple.transform.position;
        userHandRightPos = userHandCollider.rHandPos;
        
        if (triggered)
        {
            apple.transform.position = userHandRightPos;
        }

        if (eaten)
        {
            apple.SetActive(false);
        }
    }


    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.name == "HandRightCollider" || collision.gameObject.name == "HandLeftCollider")
        {
            triggered = true;
        }
    }
    void OnTriggerStay(Collider collision)
    {
    }
    void OnTriggerExit(Collider collision)
    {
    }
}
