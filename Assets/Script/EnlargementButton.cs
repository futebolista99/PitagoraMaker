using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargementButton : MonoBehaviour {
    
    bool push = false;

	// Use this for initialization
	void Start () {
	}
	
    public void PushDown() {
        push = true;
    }

    public void PushUp() {
        push = false;
    }

	// Update is called once per frame
	void Update () {
        if (push && this.transform.position.y >= 20) {
             Move();
        }
	}
    
    public void Move() {
        this.gameObject.transform.position += transform.forward * 0.5f;
    }
}
