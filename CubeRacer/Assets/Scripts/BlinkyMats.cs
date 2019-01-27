using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyMats : MonoBehaviour {

    public Color startColor;
    public Color endColor;
    public bool isBlinky;
    public float blinkTime;
    public float speed;

    private Color lerpedColor = Color.white;
    private IEnumerator coroutine;

    // Use this for initialization
    void Start () {
        startColor = Color.red;
        endColor = Color.blue;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isBlinky)
        {
            float t = (Time.time - blinkTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }
        else
        {
            float t = (Mathf.Sin(Time.time - blinkTime) * speed);
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
        }

	}

}
