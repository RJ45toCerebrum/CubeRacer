using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float forwardSpeed;
    private Rigidbody playerRB;
    private Lane currentLane;


    // side-movement time
    private Lane targetLane;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;
    private float distanceTraveled;
    private float distanceToTravel;

    // jump stuff; ben
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


    private void Awake() {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isMovingLeft)
            StartMoveLeft();
        else if (Input.GetKeyDown(KeyCode.D) && !isMovingRight)
            StartMoveRight();

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            startJumping = true;
        else
            startJumping = false;
    }

    private void FixedUpdate()
    {
        Vector3 lateralMovement = Vector3.zero;
        if (isMovingLeft)
            lateralMovement = SideMovement(true);
        else if(isMovingRight)
            lateralMovement = SideMovement(false);

        Vector3 up = Jump();

        transform.position += (up + lateralMovement + transform.forward * forwardSpeed * Time.fixedDeltaTime);
    }

    private void StartMoveLeft()
    {
        if (!currentLane.leftLane)
            return;

        isMovingLeft = true;
        targetLane = currentLane.leftLane;
        distanceTraveled = 0;
        distanceToTravel = (currentLane.transform.position - targetLane.transform.position).magnitude;
    }

    private void StartMoveRight()
    {
        if (!currentLane.rightLane)
            return;

        isMovingRight = true;
        targetLane = currentLane.rightLane;
        distanceTraveled = 0;
        distanceToTravel = (currentLane.transform.position - targetLane.transform.position).magnitude;
    }

    private bool isClose() {
        return (distanceToTravel - distanceTraveled) <= 0.01;
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
            }
        }

        return planeTranslation;
    }

    private Vector3 Jump()
    {
        Vector3 up = Vector3.zero;
        if (startJumping)
        {
            up = transform.up * jumpHeight;
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
                    // drop
                    jumpCount--;
                    up = -transform.up * jumpHeight;
                }
            }
            else if (jumpCount == 2)
            {
                if (djt >= 0f)
                    djt -= Time.deltaTime;
                else
                {
                    // drop
                    jumpCount--;
                    up = -transform.up * jumpHeight;
                }
            }
        }

        return up;
    }
}
