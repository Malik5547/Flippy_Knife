using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingLine : MonoBehaviour
{

    public float lineLength = 1f;

    private LineRenderer lineRenderer;

    public void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void SetStart(Vector3 start)
    {
        lineRenderer.SetPosition(0, start);
    }

    public void SetEnd(Vector3 end)
    {
        lineRenderer.SetPosition(1, end * lineLength);
    }

    public void SetDirection(Vector3 direction)
    {
        lineRenderer.SetPosition(1, lineRenderer.GetPosition(0) + direction * lineLength);
    }

}
