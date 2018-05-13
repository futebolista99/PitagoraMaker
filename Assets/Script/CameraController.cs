using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {

    public Camera mainCamera;
    public Camera viewCamera;
    public GameObject enlargementButton;
    public GameObject reductionButton;
    public Text buttonText;
    public GameObject trickGenerateButton;
    public int status = 0;

    // Use this for initialization
    void Start () {
        mainCamera.enabled = true;
        viewCamera.enabled = false;
        enlargementButton.SetActive(false);
        reductionButton.SetActive(false);
    buttonText.text = ("view\n画面");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickButton() {
        if (status == 0) {
            trickGenerateButton.SetActive(false);
            mainCamera.enabled = false;
            viewCamera.enabled = true;
            enlargementButton.SetActive(true);
            reductionButton.SetActive(true);
            buttonText.text = ("main\n画面");
            status = 1;
        } else {
            trickGenerateButton.SetActive(true);
            mainCamera.enabled = true;
            viewCamera.enabled = false;
            enlargementButton.SetActive(false);
            reductionButton.SetActive(false);
            buttonText.text = ("view\n画面");
            status = 0;
        }
    }
}
