using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    public float jumpHeight = 1f;
    public float movespeed = 1f;
    public float jumpTime = 1f;
    public int jumpcount;

    private Vector3 speed;
    private Vector3 jump;

    // Use this for initialization
    void Start () {
        speed = new Vector3(0, 0, movespeed);
        jump = new Vector3(0, jumpHeight, 0);
    }
	
	// Update is called once per frame
	void Update () {

        //Moves player forward
        transform.position += speed * Time.deltaTime;

        if(jumpcount == 1)
        {
            if(jumpTime >= 0f)
            {
                jumpTime -= Time.deltaTime;
            }
            else
            {
                //drop
                jumpcount--;
                transform.position -= jump;
            }
        }
        else if(jumpcount == 2)
        {
            if (jumpTime >= 0f)
            {
                jumpTime -= Time.deltaTime;
            }
            else
            {
                //drop
                jumpcount--;
                transform.position -= jump;
                jumpTime = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position += jump;
            jumpcount++;
            jumpTime = 1;
        }
       
    }

}
