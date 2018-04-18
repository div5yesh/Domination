using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour {

    public GameObject player;

    /// <summary>
    /// set reference to shooting player
    /// </summary>
    /// <param name="shooter">player object</param>
    public void SetPlayer(GameObject shooter)
    {
        player = shooter;
        SetBulletColor(player.GetComponent<Shooting>().playerSkin);
    }

    /// <summary>
    /// Set bullet color to player color
    /// </summary>
    /// <param name="color">player color</param>
    public void SetBulletColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    /// <summary>
    /// Handle collision of player with bullet object
    /// </summary>
    /// <param name="other">collider object</param>
    private void OnTriggerEnter(Collider other)
    {
        var hit = other.gameObject;

        // collision object is a player and is not the shooter himself
        if (hit != player && hit.tag == "Player")
        {
            var shooting = player.GetComponent<Shooting>();
            if (shooting != null)
            {
                shooting.UpdateScore();
                Destroy(gameObject);
            }

        }
    }
}
