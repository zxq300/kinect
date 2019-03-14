using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keepWalking : MonoBehaviour {

    public float speed = 1;
    public float damping = 2.0f;
    public bool killed = false;
    public bool eatApple = false;
    public bool touched = false;

    string currentAnimation = "";

    private Transform[] ways;
    private int index;

    // Use this for initialization
    void Start()
    {
        ways = wayPoints.Points;//////////////////////////
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(killed)
        {
            SetAnimation("isDead");
            StartCoroutine(FadeToGround(4.0f));
        }
        else if(touched)
        {
            ways = wayPoints.Points;//////////////////////////////
            Vector3 dir = new Vector3(userHandCollider.rHandPos.x, wayPoints.wayOffset.y, userHandCollider.rHandPos.z);// go back to right hand(TBD)//////////////////
            ways[ways.Length - 1].transform.position = dir;
        }
        else if(eatApple)
        {
            Vector3 dir = new Vector3(appleEqu.ApplePos.x, transform.position.y, appleEqu.ApplePos.z);

            Quaternion rotate = Quaternion.LookRotation(dir - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damping);
            transform.position = Vector3.MoveTowards(transform.position, dir, Time.deltaTime * speed);
            
            if(Vector3.Distance(dir, transform.position) < 0.2f)
            {
                SetAnimationIdle();
            }

            StartCoroutine(eatingApple(6.0f));

        }
        else
        {
            MoveTo();
        }
    }

    public void SetAnimation(string animationName)
    {

        if (currentAnimation != "")
        {
            GetComponent<Animator>().SetBool(currentAnimation, false);
        }
        GetComponent<Animator>().SetBool(animationName, true);
        currentAnimation = animationName;
    }

    public void SetAnimationIdle()
    {

        if (currentAnimation != "")
        {
            GetComponent<Animator>().SetBool(currentAnimation, false);
        }


    }
    void MoveTo()
    {
        if (index > ways.Length - 1)
            return;

        //transform.LookAt(ways[index].position);

        // smooth look at
        Quaternion rotate = Quaternion.LookRotation(ways[index].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damping);
        transform.position = Vector3.MoveTowards(transform.position, ways[index].position, Time.deltaTime * speed);
        

        if (Vector3.Distance(ways[index].position, transform.position) < 0.2f)
        {
            index++;
            if (index >= ways.Length)
            {
                transform.position = ways[ways.Length - 1].position;
                SetAnimationIdle();// stay in place
            }
            else
            {
            }
        }
        else
        {
            SetAnimation("isWalking");
        }
    }

    IEnumerator FadeToGround(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);// wait for seconds

        Vector3 faded = transform.position + new Vector3(0, -4, 0);
        transform.position = Vector3.MoveTowards(transform.position, faded, Time.deltaTime * 0.5f);

        yield return new WaitForSeconds(waitTime);// wait for seconds
        gameObject.SetActive(false);
    }

    IEnumerator eatingApple(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);// wait for seconds
        appleEqu.eaten = true;
        eatApple = false;
        collisionTest.byApple = false;
    }
}
