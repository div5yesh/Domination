using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetworkPlayer : NetworkBehaviour
{
    /// <summary>
    /// player skin color
    /// </summary>
    public Color Skin;

    /// <summary>
    /// Starting client - register player instance with network manager
    /// </summary>
    [Client]
    public override void OnStartClient()
    {
        // Dont destroy object when new scene loaded
        DontDestroyOnLoad(this);

        base.OnStartClient();
        Debug.Log("Client Network Player start");

        NetworkManager.Instance.RegisterNetworkPlayer(this);
    }

    /// <summary>
    /// remove network player from network manager
    /// </summary>
    public override void OnNetworkDestroy()
    {
        NetworkManager.Instance.DeregisterNetworkPlayer(this);
        base.OnNetworkDestroy();
    }

    /// <summary>
    /// Starting local client - isLocalPlayer and hasAuthority
    /// Set up player params and settings
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        SetPlayerReady();
        GameObject.FindGameObjectWithTag("ScoreCube").GetComponent<ScoreCube>().player = gameObject;
    }

    /// <summary>
    /// Set skin color to various scripts
    /// </summary>
    /// <param name="plrskin">Player's skin color</param>
    public void Setup(Color plrskin)
    {
        Skin = plrskin;
        GetComponent<MeshRenderer>().material.color = plrskin;
        GetComponent<Shooting>().playerSkin = plrskin;
    }

    /// <summary>
    /// After player spawn, set camera position and rotation to player's viewport
    /// Called only for local players
    /// </summary>
    public void SetPlayerReady()
    {
        if (isLocalPlayer)
        {
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = transform.rotation;

            Camera.main.transform.parent = transform;
        }
    }

    /// <summary>
    /// Call on server to change score cube position
    /// </summary>
    /// <param name="pos">current cube position</param>
    [Command]
    public void CmdMoveScoreCube(Vector3 pos)
    {
        GameObject.FindGameObjectWithTag("ScoreCube").transform.position = pos;
        RpcMoveScoreCube(pos);
    }

    /// <summary>
    /// Send score cube position to the client
    /// </summary>
    /// <param name="pos">current cube position</param>
    [ClientRpc]
    public void RpcMoveScoreCube(Vector3 pos)
    {
        GameObject.FindGameObjectWithTag("ScoreCube").transform.position = pos;
    }
}
