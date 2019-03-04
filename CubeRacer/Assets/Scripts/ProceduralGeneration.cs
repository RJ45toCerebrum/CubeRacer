using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour {

    /*This script generates a random placement of obstacles on an N x 0 x 5 grid [X x Y x Z]
     After obstacle is placed, increment X value by 10*/

    public GameObject obstacle;
    public GameObject blank;

    public int sizeX;

	// Use this for initialization
	void Start () {

        this.transform.localPosition = new Vector3(-10f, 0f, 0f);

        //This creates an Nx0x5 grid
        int posZ = 0;
        for(int x = 0; x < sizeX; x++)      //first we start at X pos = 0
        {
            int r = Random.Range(-1, 2); //I have the range and init i val set to -1 bc the spawned grid wouldn't line up cleanly with the tunnel-lanes.

            for (int i = -1; i < 2; i++)     //Here we instantiate an object for each Y pos at the current X pos
            {
                if(i != r)
                    Instantiate(blank, new Vector3(i * 10f, 0, posZ), Quaternion.identity);
                else
                    Instantiate(obstacle, new Vector3(i * 10f, 0, posZ), Quaternion.identity);
            }
            posZ += 10;     //After instantiating a blank/obstacle object, we shift X pos by 10.
        }
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
