using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStartButtonScript : MonoBehaviour {

    public GameObject playerCamera;
    public GameObject chaseCamera;

	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnClick() {
        this.gameObject.SetActive(false);
        chaseCamera.SetActive(true);
        playerCamera.SetActive(false);
        GoalJudgeScript.playStatus = "now";
        TrickGenerator.status = "play";
    }
}
