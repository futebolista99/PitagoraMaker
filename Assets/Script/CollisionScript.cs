using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnCollisionEnter(Collision col) {
        if (col.gameObject.name != "floorPrefab" && col.gameObject.name != "stairs") {
            ChaseCameraScript.player = col.gameObject;
        }
    }
}
