using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public Material coinMat;
    public Material transparentCoinMat;

    public MeshRenderer meshRenderer;

    bool collected = false;

    public void SetCollected(bool val)
    {
        if (val)
        {
            collected = true;
            tag = "Transparent";
            meshRenderer.material = transparentCoinMat;
        }
    }

    public bool IsCollected()
    {
        return collected;
    }

}
