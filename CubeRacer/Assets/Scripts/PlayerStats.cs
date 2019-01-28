using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public Text healthText;
    public Text timer;
    public int health = 6;
    public int boostTimer = 20;
    public bool boostedBonobo;
    public bool slowSally;
    public float currentSpeed;
    public float slowdownTimer = 10;
    public float rspeed;

    private float time = 0f;
    private GameObject go;
    private PlayerController pc;
    private BlinkyMats bm;

    void Awake()
    {
        go = GameObject.Find("Player");
        pc = go.GetComponent<PlayerController>();
        bm = go.GetComponent<BlinkyMats>();
    }

	// Use this for initialization
	void Start ()
    {
        rspeed = pc.forwardSpeed; //reset speed
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthText.text = health.ToString();
        time += Time.deltaTime;

        time = Mathf.Clamp(time, 0f, Mathf.Infinity);

        timer.text = string.Format("{00:00.00}", time);

        currentSpeed = pc.forwardSpeed;

        //start boostTimer
        if(boostedBonobo == true)
        {
            //pc.forwardSpeed += 1f;
            Time.timeScale = 2f;

            if (boostTimer > 0)
                boostTimer--;
            else
            {
                //slowdown
                Time.timeScale = 1f;
                //slowSally = true;
                boostedBonobo = false;
            }
        }

        //slowdownTimer
        //if(slowSally == true)
        //{
        //    if (slowdownTimer > 0)
        //    {
        //        slowdownTimer--;
        //        pc.forwardSpeed -= 1f;
        //    }
        //    else
        //    {
        //        slowSally = false;
        //        pc.forwardSpeed = rspeed;
        //    }
        //}

        //Game Over
        if(health < 1)
        {
            Time.timeScale = 0f;
            Debug.Log("You're dead!");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Obstacle")
        {
            health--;
            slowSally = true;
            bm.isBlinky = true;
        }
        else if (col.tag == "Boost")
        {
            boostedBonobo = true;
        }
    }
}
