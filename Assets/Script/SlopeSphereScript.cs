using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeSphereScript : MonoBehaviour {

    private Rigidbody slopeSphere;
    private float timer = 0.0f;
    private bool sphereSpeed = false;

	// Use this for initialization
	void Start () {
        slopeSphere = this.GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
		if(sphereSpeed == true) {
            timer += Time.deltaTime;
            if(timer >= 1.0f) {
                slopeSphere.velocity = Vector3.zero;
                slopeSphere.isKinematic = true;
            }
        }
	}
    
    public void OnCollisionEnter(Collision col) {
        if (col.gameObject.name == "dominoPrefab") {
            sphereSpeed = true;
        }
    }
}
