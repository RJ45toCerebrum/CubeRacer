﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float forwardSpeed;
    private Rigidbody playerRB;
    private Lane currentLane;


    // xy-movement time
    private Lane targetLane;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private float distanceTraveled;
    private float distanceToTravel;

    // perspective shift
    private Quaternion Qcw;
    private Quaternion qf;
    private Quaternion Qccw;
    public float rotationalSmoothing;
    private bool needsRotation = false;


    // jump 
    private float offsetLaneHeight;
    public float jumpHeight = 1f;
    public float jumpTime = 1f;
    private float jt = 0;
    public float doubleJumpTime = 1f;
    private float djt = 0;
    private int jumpCount;
    private bool startJumping = false;


    public Lane CurrentLane
    {
        get { return currentLane; }
        set { currentLane = value; }
    }

    public float OffsetLaneHeight
    {
        get
        {
            return offsetLaneHeight;
        }

        set
        {
            offsetLaneHeight = value;
        }
    }


    private void Awake() {
        playerRB = GetComponent<Rigidbody>();
        Qcw = Quaternion.AngleAxis(-90.0f, Vector3.forward);
        Qccw = Quaternion.AngleAxis(90.0f, Vector3.forward);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isMovingLeft)
            StartMoveLeft();
        else if (Input.GetKeyDown(KeyCode.D) && !isMovingRight)
            StartMoveRight();
        else if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            startJumping = true;
        else
            startJumping = false;
    }

    private void FixedUpdate()
    {
        // translation
        Vector3 lateralMovement = Vector3.zero;
        if (isMovingLeft)
            lateralMovement = SideMovement(true);
        else if(isMovingRight)
            lateralMovement = SideMovement(false);

        Vector3 up = Jump();
        transform.position += (up + lateralMovement + transform.forward * forwardSpeed * Time.fixedDeltaTime);

        // rotation
        if (needsRotation && Quaternion.Angle(transform.rotation, qf) > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, qf, rotationalSmoothing);
        else
            needsRotation = false;
    }

    private void StartMoveLeft()
    {
        if (!currentLane.leftLane)
            return;
        else if (!needsRotation && ShouldRotate(true))
        {
            if(jumpCount == 1)
                currentLane = currentLane.leftLane;
            else {
                currentLane = currentLane.leftLane.leftLane;
                jumpCount--;
            }

            qf = Qcw * transform.rotation;
            needsRotation = true;
            Debug.Log("Should Rotate Left...");
        }
        else if (LeftAbsDot() > 0.05f)
        {
            isMovingLeft = true;
            targetLane = currentLane.leftLane;
            distanceTraveled = 0;
            distanceToTravel = (currentLane.transform.position - targetLane.transform.position).magnitude;
        }
        else
            Debug.Log("Cant Move Left...");
    }

    private void StartMoveRight()
    {
        if (!currentLane.rightLane)
            return;
        else if (!needsRotation && ShouldRotate(false))
        {
            currentLane = currentLane.rightLane;
            qf = Qccw * transform.rotation;
            needsRotation = true;
            Debug.Log("Switch Panel Right....");
        }
        else if (RightAbsDot() > 0.05f)
        {
            isMovingRight = true;
            targetLane = currentLane.rightLane;
            distanceTraveled = 0;
            distanceToTravel = (currentLane.transform.position - targetLane.transform.position).magnitude;
        }
        else
            Debug.Log("Cant Move Right..");
    }

    private bool isClose() {
        return (distanceToTravel - distanceTraveled) <= 0.0;
    }

    private Vector3 SideMovement(bool left = true)
    {
        Vector3 planeTranslation = Vector3.zero;
        if (isMovingLeft || isMovingRight)
        {
            if (!isClose())
            {
                planeTranslation = transform.right * playerSpeed;
                if (left)
                    planeTranslation = -planeTranslation;
                distanceTraveled += playerSpeed;
            }
            else
            {
                isMovingLeft = isMovingRight =  false;
                currentLane = targetLane;
                distanceTraveled = 0;

                // reset the transform because its slightly off
                Vector2 lp = currentLane.transform.position;
                Vector2 p = transform.position;
                Vector2 v = lp - p;
                Vector2 u = -transform.up;
                float d = Vector2.Dot(v, u);
                Vector3 pf = currentLane.transform.position + d * transform.up;
                pf.z = transform.position.z;
                transform.position = pf;
            }
        }

        return planeTranslation;
    }

    private Vector3 Jump()
    {
        Vector3 up = Vector3.zero;
        if (startJumping)
        {
            up = transform.up * jumpHeight - (transform.up * offsetLaneHeight);
            
            if (jumpCount == 0)
                jt = jumpTime;
            else
                djt = doubleJumpTime;

            jumpCount++;
            startJumping = false;
        }
        else
        {
            if (jumpCount == 1)
            {
                if (jt >= 0f)
                    jt -= Time.deltaTime;
                else
                {
                    jumpCount--;
                    up = transform.up * jumpHeight - (transform.up * offsetLaneHeight);
                    up = -up;
                }
            }
            else if (jumpCount == 2)
            {
                if (djt >= 0f)
                    djt -= Time.deltaTime;
                else
                {
                    jumpCount--;
                    up = transform.up * jumpHeight - (transform.up * offsetLaneHeight);
                    up = -up;
                }
            }
        }

        return up;
    }

    private float LeftAbsDot(){
        return Mathf.Abs(Vector3.Dot(currentLane.transform.up, currentLane.leftLane.transform.up));
    }

    private float RightAbsDot(){
        return Mathf.Abs(Vector3.Dot(currentLane.transform.up, currentLane.rightLane.transform.up));
    }

    private bool ShouldRotate(bool left)
    {
        if (jumpCount == 0)
            return false;
        if(left)
            return LeftAbsDot() < 0.05f;
        return RightAbsDot() < 0.05f;
    }
}
