using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeScript : MonoBehaviour {
    
    Renderer rd;
    public Texture image;
    
    Mesh mesh;
    Vector2[] uv = new Vector2[24];

    // Use this for initialization
    void Start () {

        // initialize texture
        rd = GetComponent<Renderer> ();  
        rd.material.mainTexture = null;

        //attach defalt texture if you haven't attached your own
        if (image == null) {
            image = Resources.Load ("LiT!") as Texture;
        }

        // get texture size
        int tHeight = image.height;
        int tWidth = image.width;

        /*
         * this section, create new uv array and set it to the cube
         *
         *  =====how to use uv=====
         *
         * uv is vector2-array. menber of mesh.
         * You can use uv to trim texture.
         *
         * one surface
         *  ______________________________________
         * |                                      |
         * |  [texture]                           |
         * |                                      |
         * |uv[2]______uv[3]                      |
         * ||          |                          |
         * || [surface]|                          |
         * ||          |                          |
         * ||uv[0] ____uv[1]                      |
         * ---------------------------------------
         * If you want to use a texture on a surface like the diagram, set uv like this.
         *         uv[2].x = 0.0f;           uv[2].y = 0.5f;  
         *         uv[3].x = 1.0f / 3;       uv[3].y = 0.5f;
         *         uv[0].x = 0.0f;           uv[0].y = 0.0f;
         *         uv[1].x = 1.0f / 3;       uv[1].y = 0.0f;
         *
         * To use this, you can make dice, sprite animation etc.
         * Moreover, if you twist uv value(ex. switch between uv[2] and uv[3]), very interesting texture would be made.
         *
         * Attention! : Each surface has each uv, so cube has uv[0]-uv[23].
         * Attention! : Each kind of object have its own uv-array, so you might be better to check "mesh.vertics", this
         *              tells positions of uv in real world (index is shared), by printf, Debug.Log, or something.
         *
         * written by livedoor
         */

        mesh = gameObject.GetComponent<MeshFilter> ().mesh;

        uv[2].x = 0.0f;           uv[2].y = 1.0f;  
        uv[3].x = 1.0f;           uv[3].y = 1.0f;
        uv[0].x = 0.0f;           uv[0].y = 0.0f;
        uv[1].x = 1.0f;           uv[1].y = 0.0f;

        uv[4].x = 0.0f;           uv[4].y = 0.0f;
        uv[5].x = 1.0f / tWidth;  uv[5].y = 0.0f;
        uv[8].x = 0.0f;           uv[8].y = 1.0f / tHeight;
        uv[9].x = 1.0f / tWidth;  uv[9].y = 1.0f / tHeight;

        uv[23].x = 1.0f;          uv[23].y = 0.0f;
        uv[21].x = 0.0f;          uv[21].y = 1.0f;
        uv[20].x = 0.0f;          uv[20].y = 0.0f;
        uv[22].x = 1.0f;          uv[22].y = 1.0f;

        uv[19].x = 1.0f;          uv[19].y = 0.0f;
        uv[17].x = 0.0f;          uv[17].y = 1.0f;
        uv[16].x = 0.0f;          uv[16].y = 0.0f;
        uv[18].x = 1.0f;          uv[18].y = 1.0f;

        uv[15].x = 0.0f;          uv[15].y = 0.0f;
        uv[13].x = 1.0f / tWidth; uv[13].y = 0.0f;
        uv[12].x = 0.0f;          uv[12].y = 1.0f / tHeight;
        uv[14].x = 1.0f / tWidth; uv[14].y = 1.0f / tHeight;

        uv[6].x = 1.0f;           uv[6].y = 0.0f;
        uv[7].x = 0.0f;           uv[7].y = 0.0f;
        uv[10].x = 1.0f;          uv[10].y = 1.0f;
        uv[11].x = 0.0f;          uv[11].y = 1.0f;

        mesh.uv = uv;

    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void OnCollisionEnter(Collision col){
        
        rd.material.mainTexture = image;

    }
}