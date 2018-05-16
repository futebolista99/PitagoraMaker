using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    void Start() {
        

    }


    void Update() {
        
    }

    public void OnClick() {
        SceneManager.LoadScene("Main");
    }
}
