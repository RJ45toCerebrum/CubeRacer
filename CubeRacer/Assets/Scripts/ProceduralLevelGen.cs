using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralLevelGen : MonoBehaviour {

    public int numSections;     //This dictates the number of level sections spawned/level
    public Object[] level_sections;
    public float zSizeLevel;

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Z-Length of obj: " + obj.transform.localScale.z);

        //Finds sections of level based on their tag, then pools them into level_sections
        level_sections = Resources.LoadAll("Prefabs/LevelSectionsHard", typeof(GameObject));

        //GameObject go = (GameObject)level_sections[0];
        //Debug.Log("Z " + go.transform.localScale.z);

        //foreach (var v in level_sections)
        //{
        //    Debug.Log(v.name);
        //}

        OrganizeLevel();
    }

    //Places level sections in a [random] order
    void OrganizeLevel()
    {
        for(int i = 0; i < numSections; i++)
        {
            int r = Random.Range(0, level_sections.Length);
            Debug.Log("Random number " + r);

            Instantiate(level_sections[r], new Vector3(0, 0, zSizeLevel), Quaternion.identity);
            //zSizeLevel += level_sections[r].transform.localScale.z;
            zSizeLevel += 200;
        }
    }
}
