using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleItem : MonoBehaviour
{
    public Color norm;
    public Color danger;

    public MeshRenderer renderer;

    public void ChangeColor(bool isNorm)
    {
        renderer.material.color = isNorm ? norm : danger;
    }
}
