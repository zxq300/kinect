using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wayPoints : MonoBehaviour {

    public static Transform[] Points;
    public static Vector3 wayOffset;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        int count = transform.childCount;
        Points = new Transform[count];
        wayOffset = new Vector3();

        wayOffset = transform.position;

        for(int i = 0; i < count; i++)
        {
            Points[i] = transform.GetChild(i);
        }
    }
}
