using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveAround : MonoBehaviour {

    public float speed = 5;
    private Transform[] ways;
    private int index;

	// Use this for initialization
	void Start () {
        ways = wayPoints.Points;
        index = 0;
	}
	
	// Update is called once per frame
	void Update () {
        MoveTo();
	}

    void MoveTo()
    {
        if (index > ways.Length - 1)
            return;

        transform.LookAt(ways[index].position);
        transform.position = Vector3.MoveTowards(transform.position, ways[index].position, Time.deltaTime * speed);

        if (Vector3.Distance(ways[index].position, transform.position) < 0.2f)
        {
            index++;
            if(index == ways.Length)
            {
                transform.position = ways[index - 1].position;
            }
        }
    }
}
