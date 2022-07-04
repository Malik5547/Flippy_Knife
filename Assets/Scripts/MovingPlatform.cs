using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 1f;
    public float xRange = 0f;
    public float yRange = 2f;

    private Vector3 startPos;
    private Vector3 nextPos;
    private Vector3 pos1;
    private Vector3 pos2;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        pos1 = startPos - new Vector3(xRange, yRange);
        pos2 = startPos + new Vector3(xRange, yRange);

        nextPos = pos1;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == pos1)
            nextPos = pos2;
        else if (transform.position == pos2)
            nextPos = pos1;

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

    }
}
