using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Vector3 playerSpeed;
    private Rigidbody playerRB;

    private void Awake() {
        playerRB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        playerRB.MovePosition(playerRB.position + playerSpeed * Time.fixedDeltaTime);
    }
}
