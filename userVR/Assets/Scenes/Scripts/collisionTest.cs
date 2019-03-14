using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collisionTest : MonoBehaviour {

    public Text UText;
    public GameObject swordEquip;
    
    private bool byHand = false;
    private bool bySword = false;
    public static bool byApple = false;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		if (bySword)
        {
            GetComponent<keepWalking>().killed = true;
        }
        else if(byHand)
        {
            GetComponent<keepWalking>().touched = true;
        }
        else if(byApple)
        {
            GetComponent<keepWalking>().eatApple = true;
        }
        else
        {

        }
	}
    
    void OnTriggerEnter(Collider collision)
    {
        if (swordEquip.GetComponent<swordEqu>().killable == true && collision.gameObject.name == "pCube1")
        {
            byHand = false;
            bySword = true;
            byApple = false;
        }
        else if (collision.gameObject.name == "HandRightCollider" || collision.gameObject.name == "HandLeftCollider")
        {
            byHand = true;
            bySword = false;
            byApple = false;
        }
        else if (collision.gameObject.name == "apple3")
        {
            byHand = false;
            bySword = false;
            byApple = true;
        }
        else
        {
            UText.GetComponent<Text>().text = "Collision Enter: " + collision.gameObject.name;
        }
    }
    void OnTriggerStay(Collider collision)
    {
        //UText.GetComponent<Text>().text = "Collision Enter: " + collision.gameObject.name;
    }
    void OnTriggerExit(Collider collision)
    {
        //UText.GetComponent<Text>().text = "Collision Enter: " + collision.gameObject.name;
    }
}
