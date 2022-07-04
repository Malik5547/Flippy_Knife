using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalingPlatform : MonoBehaviour
{

    public float lifeTime = 1f;

    private Knife _knife = null;

    public void Activate(Knife knife)
    {
        _knife = knife;
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifeTime);

        _knife.SetKinematic(false);

        Destroy(gameObject);
    }
}
