using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordKill : MonoBehaviour {
    

    private bool triggered = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(triggered)
        {
            
        }
	}
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Bear")
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
