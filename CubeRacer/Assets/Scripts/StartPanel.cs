using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour {

    public GameObject startPanel;

    void Awake()
    {
        startPanel.SetActive(true);
        Time.timeScale = 0f; //Game paused
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClosePanel()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1f; //Unpause
    }
}
