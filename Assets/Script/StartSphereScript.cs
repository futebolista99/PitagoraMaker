using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSphereScript : MonoBehaviour {

    // 始めの球に力を加えてスタートさせるためのスクリプト

    private bool start = true;
    private Rigidbody startSphereBody;

	// Use this for initialization
	void Start () {

        startSphereBody = this.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		if( start == true && TrickGenerator.status == "play") {
            ChaseCameraScript.player = this.gameObject;
            startSphereBody.AddForce(0, 0, 0.7f, ForceMode.Impulse);
            start = false;
        }
	}
}
