using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinGoal : MonoBehaviour {

    public GameObject winMenu;

    void Awake()
    {
        winMenu.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Time.timeScale = 0f;
            winMenu.SetActive(true);
        }
            
    }
}
