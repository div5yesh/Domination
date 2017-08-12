using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCube : MonoBehaviour {

    public Color TargetColor = Color.white;

    Color scoreColor = Color.white;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        scoreColor = Color.Lerp(scoreColor, TargetColor, 0.02f);
        GetComponent<MeshRenderer>().material.color = scoreColor;
    }
}
