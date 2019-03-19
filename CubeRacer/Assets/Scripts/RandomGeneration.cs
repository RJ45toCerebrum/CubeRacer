using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour {

    /*This script generates a random placement of obstacles on an N x 0 x 5 grid [X x Y x Z]
     After obstacle is placed, increment X value by 10*/

    public GameObject obstacle;
    public GameObject blank;

    public int sizeX;
    public int difficulty;

    // Use this for initialization
    void Start () {
        //GameObject[,] lvl = new GameObject[sizeX, 3];
        //InitGridGen(lvl);
        RandomGen();
    }

	// Update is called once per frame
	void Update () {
		
	}

    //void InitGridGen(GameObject[,] lvl)
    //{
    //    for (int z = 0; z < sizeX; z++)      //first we start at X pos = 0
    //    {
    //        for (int x = 0; x < 3; x++)     //Here we instantiate an object for each Y pos at the current X pos
    //        {
    //            GameObject go = Instantiate(blank, new Vector3(x * 10f, 0, z * 10f), Quaternion.identity);
    //            go.transform.SetParent(this.transform, false);
    //            lvl[z,x] = go;
    //        }
    //    }
    //    //Display(lvl);
    //}

    //void Display(GameObject[,] lvl)
    //{
    //    for(int i = 0; i < sizeX; i++)
    //    {
    //        for(int j = 0; j < 3; j++)
    //        {
    //            Debug.Log("obstacle at X: " + lvl[i,j].transform.position.x + " and Z: " + lvl[i, j].transform.position.z);
    //        }
    //    }
    //}

    void RandomGen()
    {
        for(int z = 0; z < sizeX; z++)
        {
            int r = Random.Range(1, 100);
            Debug.Log("1st Random number chosen: " + r);
            if(r < difficulty)  //First random check
            {
                int r2 = Random.Range(0, 3); //decides number of obstacles to spawn at this Z pos
                Debug.Log("2nd Random number chosen: " + r2);

                for (int i = 0; i < r2; i++)
                {
                    int r3 = Random.Range(0, 3); //decides where to place obstacles
                    Debug.Log("2nd Random number chosen: " + r3);

                    for (int x = 0; x < 3; x++)  //Second random check
                    {
                        if (r3 == x)
                        {
                            GameObject go = Instantiate(obstacle, new Vector3(x * 10f, 0, z * 10f), Quaternion.identity);
                            go.transform.SetParent(this.transform, false);
                            //lvl[z, x] = go;
                        }
                        else
                        {
                            GameObject go = Instantiate(blank, new Vector3(x * 10f, 0, z * 10f), Quaternion.identity);
                            go.transform.SetParent(this.transform, false);
                        }
                    }
                }
            }
        }
    }
}
