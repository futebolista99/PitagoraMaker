using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSphereScript : MonoBehaviour {

    private bool start = true;
    private Rigidbody startSphereBody;

	// Use this for initialization
	void Start () {

        startSphereBody = this.GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		if( start == true && TrickGenerator.status == "play") {
            startSphereBody.AddForce(0, 0, 0.5f, ForceMode.Impulse);
            start = false;
        }
	}
}
