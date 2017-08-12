using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

    public float mouseSensitivity = 100.0f;
    public float force = 10;
    public GameObject bulletPrefab;

    public GameObject ScoreCube;

    [SyncVar(hook = "OnScoreUpdate")]
    public int score = 0;

    Vector3 rot;

    public Color playerSkin;

    void Start () {
        rot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        //Vector3 rot = transform.rotation.eulerAngles;

        Debug.Log("IRX:" + rot.x + "IRY:" + rot.y);

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        Debug.Log("X:" + mouseX + "Y:" + mouseY);

        rot.y += mouseX * mouseSensitivity * Time.deltaTime;
        rot.x += mouseY * mouseSensitivity * Time.deltaTime;

        Debug.Log("RX:" + rot.x + "RY:" + rot.y);

        //rot.x = Mathf.Clamp(rot.x, -100, 80);

        Quaternion rotation = Quaternion.Euler(rot.x, rot.y, 0.0f);
        gameObject.transform.rotation = rotation;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate( bulletPrefab, transform.position, transform.rotation);

        bullet.GetComponent<Bullet>().player = gameObject;

        // Set the bullet color
        bullet.GetComponent<MeshRenderer>().material.color = playerSkin;

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * force;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 3.0f);
    }

    public void UpdateScore()
    {
        if (!isServer)
            return;

        score++;
    }

    public void OnScoreUpdate(int currentScore)
    {
        GameObject.FindGameObjectWithTag("ScoreCube").GetComponent<ScoreCube>().TargetColor = playerSkin;
    }
}
