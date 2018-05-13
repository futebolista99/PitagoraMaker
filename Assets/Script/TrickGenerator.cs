using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;   

public class TrickGenerator : MonoBehaviour {

    int i;
    int status = 0;
    int direction = -2;
    float distance = 0;
    float angle = 0;
    string moveDirection = "stay";

    public GameObject player;
    public Camera playerCamera;
    public Camera chaseCamera;
    public float reachableDistance = 100.0f;
    public GameObject blockPrefab;
    public GameObject blockGray;

    GameObject startSphere;
    Rigidbody sphereBody;
    bool sphereMake = false;

    float blockWidth;
    public GameObject[] trickStart;
    public GameObject[] trickLeft;
    public GameObject[] trickFront;
    public GameObject[] trickRight;
    public GameObject[] trickGoal;

    Ray ray;
    Ray rayLeft;
    Ray rayFront;
    Ray rayRight;
    RaycastHit hitInfo;
    RaycastHit hitInfoLeft;
    RaycastHit hitInfoFront;
    RaycastHit hitInfoRight;
    Renderer blockGrayRenderer;
    Vector3 hitObjPos;
    Image image;
    Sprite[] spriteStart = new Sprite[5];
    Sprite[] spriteLeft = new Sprite[5];
    Sprite[] spriteFront = new Sprite[5];
    Sprite[] spriteRight = new Sprite[5];
    Sprite[] spriteGoal = new Sprite[5];
    

    void Awake() {
        for (i = 0; i < trickStart.Length; ++i) {
            spriteStart[i] = Resources.Load<Sprite>(trickStart[i].name);
        }
        for (i = 0; i < trickLeft.Length; ++i) {
            spriteLeft[i] = Resources.Load<Sprite>(trickLeft[i].name);
        }
        for (i = 0; i < trickFront.Length; ++i) {
            spriteFront[i] = Resources.Load<Sprite>(trickFront[i].name);
        }
        for (i = 0; i < trickRight.Length; ++i) {
            spriteRight[i] = Resources.Load<Sprite>(trickRight[i].name);
        }
        for (i = 0; i < trickGoal.Length; ++i) {
            spriteGoal[i] = Resources.Load<Sprite>(trickGoal[i].name);
        }
    }

    // Use this for initialization
    void Start() {
        blockWidth = blockPrefab.transform.localScale.x;
        blockGrayRenderer = blockGray.GetComponent<Renderer>();

        image = this.gameObject.GetComponent<Image>();
        image.sprite = spriteStart[0];
        
    }

    // Update is called once per frame
    void Update() {

        if(sphereMake == true) {
            startSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            startSphere.transform.position = new Vector3(0, 12, -4.5f);
            sphereMake = false;
        }

        // Show gray block
        ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        bool isRayhit = Physics.Raycast(ray, out hitInfo, reachableDistance);
        rayLeft = playerCamera.ViewportPointToRay(new Vector3(0.3f, 0.5f, 0.0f));
        bool isRayhitLeft = Physics.Raycast(rayLeft, out hitInfoLeft, reachableDistance);
        rayFront = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.8f, 0.0f));
        bool isRayhitFront = Physics.Raycast(rayFront, out hitInfoFront, reachableDistance);
        rayRight = playerCamera.ViewportPointToRay(new Vector3(0.7f, 0.5f, 0.0f));
        bool isRayhitRight = Physics.Raycast(rayRight, out hitInfoRight, reachableDistance);

        if (isRayhit && status != -1) {
            blockGrayRenderer.enabled = true;
            hitObjPos = hitInfo.collider.gameObject.transform.position;
            blockGray.transform.position = hitObjPos + hitInfo.normal * blockWidth * 0.3f;
        }

        // Camera movement
        if(moveDirection == "start") {
            blockGrayRenderer.enabled = false;
            player.transform.Translate(new Vector3(0, 0, 0.2f));
            distance += 0.2f;
        }
        if(distance >= 10.0f) {
            blockGrayRenderer.enabled = true;
            distance = 0.0f;
            moveDirection = "stay";
        }

        if (moveDirection == "leftRotation") {
            blockGrayRenderer.enabled = false;
            player.transform.Rotate(new Vector3(0, -2.0f, 0));
            angle += 2.0f;
        }
        if (angle >= 90.0f) {
            blockGrayRenderer.enabled = true;
            angle = 0.0f;
            moveDirection = "leftPosition";
        }
        if(moveDirection == "leftPosition") {
            blockGrayRenderer.enabled = false;
            player.transform.Translate(new Vector3(0, 0, 0.2f));
            distance += 0.2f;
        }
        if(distance >= 10.0f) {
            blockGrayRenderer.enabled = true;
            distance = 0.0f;
            moveDirection = "stay";
        }

        if (moveDirection == "front") {
            blockGrayRenderer.enabled = false;
            player.transform.Translate(new Vector3(0, 0, 0.2f));
            distance += 0.2f;
        }
        if (distance >= 10.0f) {
            blockGrayRenderer.enabled = true;
            distance = 0.0f;
            moveDirection = "stay";
        }

        if (moveDirection == "rightRotation") {
            blockGrayRenderer.enabled = false;
            player.transform.Rotate(new Vector3(0, 2.0f, 0));
            angle += 2.0f;
        }
        if (angle >= 90.0f) {
            blockGrayRenderer.enabled = true;
            angle = 0.0f;
            moveDirection = "rightPosition";
        }
        if (moveDirection == "rightPosition") {
            blockGrayRenderer.enabled = false;
            player.transform.Translate(new Vector3(0, 0, 0.2f));
            distance += 0.2f;
        }
        if (distance >= 10.0f) {
            blockGrayRenderer.enabled = true;
            distance = 0.0f;
            moveDirection = "stay";
        }

        // Select blocks
        if (status == 1) {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = -1;
                blockPrefab = trickLeft[0];
                image.sprite = spriteLeft[0];
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = 0;
                blockPrefab = trickFront[0];
                image.sprite = spriteFront[0];
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = 1;
                blockPrefab = trickRight[0];
                image.sprite = spriteRight[0];
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = 2;
                blockPrefab = trickGoal[0];
            }

            if (direction == -1) {
                for (int i = 1; i <= trickLeft.Length; i++) {
                    blockPrefab = trickLeft[i - 1];
                    image.sprite = spriteLeft[i - 1];
                }
            } else if (direction == 0) {
                for (int i = 1; i <= trickFront.Length; i++) {
                    blockPrefab = trickFront[i - 1];
                    image.sprite = spriteFront[i - 1];
                }
            } else if (direction == -1) {
                for (int i = 1; i <= trickRight.Length; i++) {
                    blockPrefab = trickRight[i - 1];
                    image.sprite = spriteRight[i - 1];
                }
            } else if (direction == 2) {
                for (int i = 1; i <= trickRight.Length; i++) {
                    blockPrefab = trickGoal[i - 1];
                    image.sprite = spriteGoal[i - 1];
                }
            }
        }

        if (status == 2) {
            
            chaseCamera.enabled = true;
            playerCamera.enabled = false;
            blockGrayRenderer.enabled = false;
            startSphere.AddComponent<Rigidbody>();
            sphereBody = startSphere.GetComponent<Rigidbody>();
            sphereBody.useGravity = true;
            sphereBody.AddForce(0, 0, 0.5f, ForceMode.Impulse);
            status = -1;
        }
    }

    public void OnClick() {

        if (status == 0) {
            blockPrefab = trickStart[0];
            hitObjPos = hitInfo.collider.gameObject.transform.position;
            Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);

            sphereMake = true;

            moveDirection = "start";
            image.sprite = spriteFront[0];
            blockPrefab = trickFront[0];
            status = 1;
        }

        else if (status == 1) {
            if (hitInfo.collider.gameObject.name == "greenPrefab(Clone)" || hitInfo.collider.gameObject.name == "whitePrefab(Clone)") {
                // Put blocks
                if (trickLeft.Contains(blockPrefab)) {
                    if (hitInfoLeft.collider.gameObject.name == "greenPrefab(Clone)" || hitInfoLeft.collider.gameObject.name == "whitePrefab(Clone)") {
                        hitObjPos = hitInfo.collider.gameObject.transform.position;
                        Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                        moveDirection = "leftRotation";
                    }
                }
                if (trickFront.Contains(blockPrefab)) {
                    if (hitInfoFront.collider.gameObject.name == "greenPrefab(Clone)" || hitInfoFront.collider.gameObject.name == "whitePrefab(Clone)") {
                        hitObjPos = hitInfo.collider.gameObject.transform.position;
                        Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                        moveDirection = "front";

                    }
                }
                if (trickRight.Contains(blockPrefab)) {
                    if (hitInfoRight.collider.gameObject.name == "greenPrefab(Clone)" || hitInfoRight.collider.gameObject.name == "whitePrefab(Clone)") {
                        hitObjPos = hitInfo.collider.gameObject.transform.position;
                        Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                        moveDirection = "rightRotation";
                    }
                }
                if (trickGoal.Contains(blockPrefab)) {
                    hitObjPos = hitInfo.collider.gameObject.transform.position;
                    Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                    status = 2; 
                }
            }
        }
    }
}
