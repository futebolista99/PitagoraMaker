using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;   

public class TrickGenerator : MonoBehaviour {

    int i;
    public static string status = "start";
    string direction = "frontStart";
    float distance = 0.0f;
    float angle = 0.0f;
    float restartTimer = 0.0f;
    string moveDirection = "stay";


    public GameObject player;
    public Camera playerCamera;
    public GameObject chaseCamera;
    public static GameObject startSphere;
    public float reachableDistance = 100.0f;
    public GameObject blockPrefab;
    public GameObject playStartButton;
    public GameObject restartButton;
    
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

    private float finishTimer = 0.0f;
    private bool finishSoundBool = false;
    private bool playSoundBool = false;
    public AudioClip putSound;
    public AudioClip playSound;
    public AudioClip finishSound;
    private AudioSource audioSource;

    void Awake() {
        
        // spriteの代入
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
        chaseCamera.SetActive(false);
        image = this.gameObject.GetComponent<Image>();

        // 最初に候補として表示される仕掛けはドミノのstartPrefab
        image.sprite = spriteStart[0];

        audioSource = this.gameObject.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {


        

        // rayを飛ばして物が存在するかどうかを判断
        ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.6f, 0.0f));
        bool isRayhit = Physics.Raycast(ray, out hitInfo, reachableDistance);
        rayLeft = playerCamera.ViewportPointToRay(new Vector3(0.3f, 0.5f, 0.0f));
        bool isRayhitLeft = Physics.Raycast(rayLeft, out hitInfoLeft, reachableDistance);
        rayFront = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.8f, 0.0f));
        bool isRayhitFront = Physics.Raycast(rayFront, out hitInfoFront, reachableDistance);
        rayRight = playerCamera.ViewportPointToRay(new Vector3(0.7f, 0.5f, 0.0f));
        bool isRayhitRight = Physics.Raycast(rayRight, out hitInfoRight, reachableDistance);
        

        // Camera movement　moveDirectionがstartやfrontPositonなら前に進む、leftやrightも然り
        if(moveDirection == "start" || moveDirection == "front") {
            player.transform.Translate(new Vector3(0, 0, 0.2f));
            distance += 0.2f;
        }
        if(distance >= 9.8f) {
            distance = 0.0f;
            moveDirection = "stay";
        }

        if (moveDirection == "leftRotation") {
            player.transform.Rotate(new Vector3(0, -2.0f, 0));
            angle += 2.0f;
        }
        if (angle >= 90.0f) {
            angle = 0.0f;
            moveDirection = "leftPosition";
        }

        if(moveDirection == "leftPosition") {
            player.transform.Translate(new Vector3(0, 0, 0.2f));
            distance += 0.2f;
        }
        if(distance >= 9.8f) {
            distance = 0.0f;
            moveDirection = "stay";
        }

        if (moveDirection == "rightRotation") {
            player.transform.Rotate(new Vector3(0, 2.0f, 0));
            angle += 2.0f;
        }
        if (angle >= 90.0f) {
            angle = 0.0f;
            moveDirection = "rightPosition";
        }
        if (moveDirection == "rightPosition") {
            player.transform.Translate(new Vector3(0, 0, 0.2f));
            distance += 0.2f;
        }
        if (distance >= 9.8f) {
            distance = 0.0f;
            moveDirection = "stay";
        }



        // Select blocks
        if (status == "middle") {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = "left";
                blockPrefab = trickLeft[0];
                image.sprite = spriteLeft[0];
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = "front";
                blockPrefab = trickFront[0];
                image.sprite = spriteFront[0];
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = "right";
                blockPrefab = trickRight[0];
                image.sprite = spriteRight[0];
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = "stayGoal";
                blockPrefab = trickGoal[0];
                image.sprite = spriteGoal[0];
            }

            if (direction == "left") {
                for (int i = 1; i <= trickLeft.Length; i++) {
                    if (Input.GetKeyDown(i.ToString())) {
                        blockPrefab = trickLeft[i - 1];
                        image.sprite = spriteLeft[i - 1];
                    }
                }
            } else if (direction == "front") {
                for (int i = 1; i <= trickFront.Length; i++) {
                    if (Input.GetKeyDown(i.ToString())) {
                        blockPrefab = trickFront[i - 1];
                        image.sprite = spriteFront[i - 1];
                    }
                }
            } else if (direction == "right") {
                for (int i = 1; i <= trickRight.Length; i++) {
                    if (Input.GetKeyDown(i.ToString())) {
                        blockPrefab = trickRight[i - 1];
                        image.sprite = spriteRight[i - 1];
                    }
                }
            } else if (direction == "stayGoal") {
                for (int i = 1; i <= trickGoal.Length; i++) {
                    if (Input.GetKeyDown(i.ToString())) {
                        blockPrefab = trickGoal[i - 1];
                        image.sprite = spriteGoal[i - 1];
                    }
                }
            }
        }

        if(GoalJudgeScript.playStatus == "now" && playSoundBool == false) {
            audioSource.clip = playSound;
            audioSource.Play();
            playSoundBool = true;
        }

        if(GoalJudgeScript.playStatus == "finish" && playSoundBool == true) {
            audioSource.Stop();
            playSoundBool = false;
        }

        if(GoalJudgeScript.playStatus == "finish" && finishSoundBool == false) {
            finishTimer += Time.deltaTime;
            if (finishTimer >= 1.0f) {
                audioSource.clip = finishSound;
                audioSource.Play();
                finishSoundBool = true;
            }
        }

        if (status == "goal") {
            playStartButton.SetActive(true);
        }

        if (GoalJudgeScript.playStatus == "finish") {
            restartTimer += Time.deltaTime;
        }
        if (restartTimer >= 5.0f) {
            restartButton.SetActive(true);
        }
    }

    public void OnClick() {

        if (status == "start") {
            blockPrefab = trickStart[0];
            hitObjPos = hitInfo.collider.gameObject.transform.position;
            Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);

            startSphere = trickStart[0];

            audioSource.clip = putSound;
            audioSource.Play();
            moveDirection = "start";
            image.sprite = spriteFront[0];
            blockPrefab = trickFront[0];
            status = "middle";
        }

        else if (status == "middle") {
            // Put blocks
            if (trickLeft.Contains(blockPrefab)) {
                if (hitInfoLeft.collider.gameObject.name == "greenPrefab(Clone)" || hitInfoLeft.collider.gameObject.name == "whitePrefab(Clone)") {
                    hitObjPos = hitInfo.collider.gameObject.transform.position;
                    Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                    audioSource.clip = putSound;
                    audioSource.Play();
                    moveDirection = "leftRotation";
                }
            }
            if (trickFront.Contains(blockPrefab)) {
                if (hitInfoFront.collider.gameObject.name == "greenPrefab(Clone)" || hitInfoFront.collider.gameObject.name == "whitePrefab(Clone)") {
                    hitObjPos = hitInfo.collider.gameObject.transform.position;
                    Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                    audioSource.clip = putSound;
                    audioSource.Play();
                    moveDirection = "front";

                }
            }
            if (trickRight.Contains(blockPrefab)) {
                if (hitInfoRight.collider.gameObject.name == "greenPrefab(Clone)" || hitInfoRight.collider.gameObject.name == "whitePrefab(Clone)") {
                    hitObjPos = hitInfo.collider.gameObject.transform.position;
                    Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                    audioSource.clip = putSound;
                    audioSource.Play();
                    moveDirection = "rightRotation";
                }
            }
            if (trickGoal.Contains(blockPrefab)) {
                hitObjPos = hitInfo.collider.gameObject.transform.position;
                Instantiate(blockPrefab, hitObjPos + hitInfo.normal * blockWidth * 0.55f, player.transform.rotation);
                audioSource.clip = putSound;
                audioSource.Play();
                status = "goal";
            }
        }
    }
}
