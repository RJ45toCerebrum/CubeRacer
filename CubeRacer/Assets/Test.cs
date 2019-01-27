using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Test : MonoBehaviour
{
    public Transform T1;
    public Transform T2;

    void Start ()
    {
		if(T1 && T2) {
            Debug.Log(Quaternion.Angle(T1.rotation, T2.rotation));
        }
	}
}
