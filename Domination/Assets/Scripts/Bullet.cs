using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public GameObject player;

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        if (true)
        {
            var shooting = player.GetComponent<Shooting>();
            if (shooting != null)
            {
                shooting.UpdateScore();
            }
            
            //Destroy(gameObject);
        }
    }
}
