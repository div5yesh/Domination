using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ScoreCube : NetworkBehaviour
{

    /// <summary>
    /// Target score color
    /// </summary>
    public Color TargetColor = Color.white;

    /// <summary>
    /// Current score color
    /// </summary>
    Color scoreColor = Color.white;

    /// <summary>
    /// highest score
    /// </summary>
    int TargetScore = 0;

    Vector3 screenPoint;
    Vector3 offset;

    public GameObject player;

    /// <summary>
    /// Update current score and target color to color of highest scorer
    /// </summary>
    /// <param name="score">player score</param>
    /// <param name="color">player color</param>
    public void UpdateScore(int score, Color color)
    {
        if (score > TargetScore)
        {
            TargetScore = score;
            TargetColor = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // slowly change score color to target color - fade out effect
        scoreColor = Color.Lerp(scoreColor, TargetColor, 0.02f);
        GetComponent<MeshRenderer>().material.color = scoreColor;
    }

    /// <summary>
    /// handle mouse down input
    /// save current position of score cube and mouse click position
    /// </summary>
    private void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    /// <summary>
    /// handle mouse drag event
    /// move score cube to current mouse position by sending
    /// command to server from network player
    /// </summary>
    private void OnMouseDrag()
    {
        Vector3 currentSP = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curP = Camera.main.ScreenToWorldPoint(currentSP) + offset;

        //send command to server to move the cube
        player.GetComponent<NetworkPlayer>().CmdMoveScoreCube(curP);
    }
}
