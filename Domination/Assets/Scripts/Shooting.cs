using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

    /// <summary>
    /// Player's mouse sensitivity
    /// </summary>
    public float mouseSensitivity = 100.0f;

    /// <summary>
    /// bullet's shooting velocity 
    /// </summary>
    public float force = 40;

    public GameObject bulletPrefab;

    /// <summary>
    /// Bullet's spawn point - tip of gun barrel
    /// </summary>
    public Transform bulletSpawnPoint;

    [SyncVar(hook = "OnScoreUpdate")]
    public int score = 0;

    /// <summary>
    /// Player's current rotation
    /// </summary>
    Vector3 rot;

    /// <summary>
    /// Player's color
    /// </summary>
    public Color playerSkin;

    void Start () {
        // get current rotation
        rot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        // get mouse input for horizontal and vertical axes
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rot.y += mouseX * mouseSensitivity * Time.deltaTime;
        rot.x += mouseY * mouseSensitivity * Time.deltaTime;

        Quaternion rotation = Quaternion.Euler(rot.x, rot.y, 0.0f);
        gameObject.transform.rotation = rotation;

        // get spacebar button input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire(gameObject);
        }
    }

    /// <summary>
    /// Send network command to server to spawn a bullet and shoot
    /// </summary>
    [Command]
    void CmdFire(GameObject player)
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = Instantiate( bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        // Set the shooting player
        bullet.GetComponent<Bullet>().SetPlayer(player);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * force;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 3.0f);
    }

    /// <summary>
    /// Update player's score
    /// Called on server only
    /// </summary>
    public void UpdateScore()
    {
        if (!isServer)
            return;

        score++;
    }

    /// <summary>
    /// Update target score color to the player's skin color
    /// </summary>
    /// <param name="currentScore"></param>
    public void OnScoreUpdate(int currentScore)
    {
        GameObject.FindGameObjectWithTag("ScoreCube").GetComponent<ScoreCube>().UpdateScore(currentScore, playerSkin);
    }
}
