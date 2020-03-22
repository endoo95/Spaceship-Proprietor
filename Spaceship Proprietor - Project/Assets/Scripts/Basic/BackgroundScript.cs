using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float parralax = 4;

    private void Update()
    {
        MeshRenderer mr;
        mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;
        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / transform.localScale.x / parralax;
        offset.y = transform.position.y / transform.localScale.y / parralax;

        mat.mainTextureOffset = offset;
    }
}
