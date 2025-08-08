using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class groundmoving : MonoBehaviour
{

    private MeshRenderer meshRenderer;
    // Start is called before the first frame update

    private void Awake() {

        meshRenderer=GetComponent<MeshRenderer>();

    }
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = gameManager.instance.gameSpeed / transform.localScale.x;

        meshRenderer.material.mainTextureOffset += Vector2.right * speed * Time.deltaTime;

    }
}