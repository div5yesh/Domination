using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : UnityEngine.Networking.NetworkManager
{
    public static NetworkManager Instance;

    List<NetworkPlayer> players;

    Color[] playerSkins = { Color.red, Color.blue, Color.green, Color.yellow };

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        players = new List<NetworkPlayer>();
    }

    //    // Update is called once per frame
    void Update()
    {
        
    }

    //
    public void RegisterNetworkPlayer(NetworkPlayer player)
    {
        player.Setup(playerSkins[players.Count]);
        players.Add(player);
        //		player.SetPlayerId (players.Count);
        //		player.becameReady += SetPlayersReady;
        //		player.OnPlayerReady ();
    }

    public void DeregisterNetworkPlayer(NetworkPlayer player)
    {
        players.Remove(player);
    }

    //	public void SetPlayersReady(NetworkPlayer npl){
    //		if (players.Count == 2) {
    //			playersReady = true;
    //		}
    //	}
}
