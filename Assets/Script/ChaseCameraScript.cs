using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCameraScript : MonoBehaviour {
    
    public static GameObject player;

    // play画面の時にカメラが追いかけるためのスクリプト

	// Use this for initialization
	void Start () {
        this.gameObject.transform.position =new Vector3(5, 20, -10);
    }

    // Update is called once per frame
    void Update() {
        this.gameObject.transform.position += (player.transform.position + new Vector3(5, 8, -5) - this.gameObject.transform.position) * 0.02f;
    }
}
