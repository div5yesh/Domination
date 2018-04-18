using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : UnityEngine.Networking.NetworkManager
{
    public static NetworkManager Instance;

    List<NetworkPlayer> players;

    /// <summary>
    /// list of player colors
    /// </summary>
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

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Add player instance to network manager list and init player skin and settings
    /// </summary>
    /// <param name="player">Network Player instance </param>
    public void RegisterNetworkPlayer(NetworkPlayer player)
    {
        player.Setup(playerSkins[players.Count]);
        players.Add(player);
    }

    /// <summary>
    /// Remove player instance from network manager list
    /// </summary>
    /// <param name="player">Network Player instance</param>
    public void DeregisterNetworkPlayer(NetworkPlayer player)
    {
        players.Remove(player);
    }
}
