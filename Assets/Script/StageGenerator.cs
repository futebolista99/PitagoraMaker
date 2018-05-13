using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour {

    public GameObject greenPrefab;
    public GameObject whitePrefab;
    public int stageNum = 100;
    float blockWidth;

    // Use this for initialization
    void Start() {

        blockWidth = greenPrefab.transform.localScale.x;

        for (int i = 0; i < stageNum; ++i) {
            for (int j = 0; j < stageNum; ++j) {
                if ((i + j) % 2 == 0) {
                    Vector3 displacement = new Vector3(blockWidth * (i - (stageNum - 1) / 2), 0, blockWidth * (j - (stageNum - 1) / 2));
                    Instantiate(greenPrefab, transform.position + displacement, Quaternion.identity);
                } 
                else {
                    Vector3 displacement = new Vector3(blockWidth * (i - (stageNum - 1) / 2), 0, blockWidth * (j - (stageNum - 1) / 2));
                    Instantiate(whitePrefab, transform.position + displacement, Quaternion.identity);
                }
            }
        }
    }
}
