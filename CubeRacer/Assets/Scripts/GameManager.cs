using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tunnel))]
public class GameManager : MonoBehaviour
{
    public static Lane entryLane;
    public Lane playerStartLane;
    public PlayerController player;

    public GameManager Instance { get; private set; }

    [SerializeField]
    private Tunnel tunnel;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Not alloed to have multiple game managers....");

        // setup player one
        player.transform.position = playerStartLane.transform.position + playerStartLane.transform.up * tunnel.normalOffset;
        player.transform.rotation = playerStartLane.transform.rotation;
        player.CurrentLane = playerStartLane;
        player.OffsetLaneHeight = tunnel.normalOffset;
        player.XClamp = new Vector2(tunnel.xmin, tunnel.xmax);
        player.YClamp = new Vector2(tunnel.ymin, tunnel.ymax);
    }

}
