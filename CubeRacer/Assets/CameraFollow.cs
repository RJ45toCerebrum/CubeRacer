using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    private Vector3 previousPosition;
    public Vector3 offset;
    public Vector3 rotation;
    public float smoothing = 1;

    private void Start() {
        previousPosition = player.transform.position;
    }

    protected void FixedUpdate()
    {
        Vector3 currentVelocity = transform.position - previousPosition;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref currentVelocity, smoothing);
        previousPosition = transform.position;
    }
}
