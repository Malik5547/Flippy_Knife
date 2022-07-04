using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{

    public float rotationSpeed = 1f;

    public bool rotateBack;

    // Update is called once per frame
    void Update()
    {
        if(rotateBack)
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        else
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
