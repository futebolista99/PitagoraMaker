using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour {

    float timer = 0.0f;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnClick() {
        GoalJudgeScript.playStatus = "now";
        TrickGenerator.status = "start";
        SceneManager.LoadScene("Start");
    }
}
