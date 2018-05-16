using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalJudgeScript : MonoBehaviour {
    
    public static string playStatus = "now";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "dominoPrefab (2)") {
            playStatus = "finish";
        }
    }
}
