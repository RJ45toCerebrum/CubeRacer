using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Vector3 playerSpeed;
    private Rigidbody playerRB;
    private Lane currentLane;
    public float laneSwitchTime = 0.3f;

    // movement time
    private Lane targetLane;
    private Vector2 movementDirection;
    private float sideMovementTime;


    private bool isMoving = false;

    public Lane CurrentLane
    {
        get { return currentLane; }
        set { currentLane = value; }
    }

    private void Awake() {
        playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 PF = Vector3.zero;
        if (isMoving)
        {
            if (!isClose())
            {
                Vector2 S = currentLane.transform.position;
                Vector3 w = (S + (sideMovementTime / laneSwitchTime) * movementDirection);
                PF += w;
                sideMovementTime += Time.deltaTime;
                PF.z += transform.position.z + playerSpeed.z * Time.deltaTime;
                transform.position = PF;
            }
            else
            {
                isMoving = false;
                sideMovementTime = 0;
                transform.position = transform.position + playerSpeed * Time.fixedDeltaTime;
                currentLane = targetLane;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isMoving)
            StartMoveLeft();
        else if (Input.GetKeyDown(KeyCode.D) && !isMoving)
            StartMoveRight();
        else
            transform.position = transform.position + playerSpeed * Time.fixedDeltaTime;
    }

    private void StartMoveLeft()
    {
        isMoving = true;
        targetLane = currentLane.leftLane;
        movementDirection = targetLane.transform.position - currentLane.transform.position;
        sideMovementTime = 0;
    }

    private void StartMoveRight()
    {
        isMoving = true;
        targetLane = currentLane.rightLane;
        movementDirection = targetLane.transform.position - currentLane.transform.position;
        sideMovementTime = 0;
    }

    private bool isClose() {
        Vector2 s = transform.position;
        Vector2 f = targetLane.transform.position;
        float sqrd = (s - f).sqrMagnitude;
        return sqrd < 0.01f;
    }
}
