using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeCollisionScript : MonoBehaviour {

    public GameObject slopeSphere;
    Rigidbody slopeSphereBody;

	// Use this for initialization
	void Start () {

        slopeSphereBody = slopeSphere.GetComponent<Rigidbody>();
        slopeSphereBody.useGravity = false;

	}
	
	// Update is called once per frame
	void Update () {
    }

    public void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "last") {
            ChaseCameraScript.player = slopeSphere;
            slopeSphereBody.useGravity = true;
        }
    }
}
