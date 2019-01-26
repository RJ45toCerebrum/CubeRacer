using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Lane entryLane;
    public Lane player1Start, player2Start;
    public PlayerController player1, player2;

    public GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Not alloed to have multiple game managers....");

        // setup player one
        player1.transform.position = player1Start.transform.position;
        player1.transform.rotation = player1Start.transform.rotation;
        player1.CurrentLane = player1Start;
    }


}
