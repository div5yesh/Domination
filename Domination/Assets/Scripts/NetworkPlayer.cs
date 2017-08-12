using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityRandom = UnityEngine.Random;
using System;

public class NetworkPlayer : NetworkBehaviour
{
    //	public event Action<NetworkPlayer> becameReady;
    //
    //    private int playerId;

    Color Skin;

    [Client]
    public override void OnStartClient()
    {
        DontDestroyOnLoad(this);

        base.OnStartClient();
        Debug.Log("Client Network Player start");

        NetworkManager.Instance.RegisterNetworkPlayer(this);
    }

    public override void OnNetworkDestroy()
    {
        NetworkManager.Instance.DeregisterNetworkPlayer(this);
        base.OnNetworkDestroy();
    }

    //
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        SetPlayerReady();
    }
    //
    //    public void OnPlayerReady()
    //    {
    //		if (hasAuthority)
    //        {
    //            CmdClientReadyInScene();
    //        }
    //    }
    //
    //	public void SetPlayerId(int id){
    //		playerId = id;
    //	}
    //

    public void Setup(Color plrskin)
    {
        Skin = plrskin;
        GetComponent<MeshRenderer>().material.color = plrskin;
        GetComponent<Shooting>().playerSkin = plrskin;
    }

    public void SetPlayerReady()
    {
        if (isLocalPlayer)
        {
            Camera.main.transform.rotation = transform.rotation;
            Camera.main.transform.position = transform.position;
            //Camera.main.transform.position = transform.position - new Vector3(8.2f, 5.7f, 5.2f);
        }

        if (hasAuthority)
        {

            //			CmdSetPlayerReady ();
        }
    }
    //
    //	[Command]
    //	public void CmdSetPlayerReady(){
    //		if (becameReady != null) {
    //			becameReady (this);
    //		}
    //	}
}
