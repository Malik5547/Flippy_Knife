using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeIcon : MonoBehaviour
{

    public GameObject knifePrefab;
    public GameObject selectionFrame;
    public GameObject darkLayer;
    public Transform objectParent;

    static SelectionManager selectionManager = null;

    bool locked = false;

    // Start is called before the first frame update
    void Start()
    {
        selectionFrame.SetActive(false);
    }

    public void IconClicked()
    {
        selectionManager.SetCurrentSelection(this);
        Debug.Log("Icon " + gameObject + " clicked.");
    }

    
    public void SetSelected(bool value)
    {
        selectionFrame.SetActive(value);
        Debug.Log("Set selected: " + value);
    }

    public void SetLocked(bool value)
    {
        darkLayer.SetActive(value);
        locked = value;
    }

    public bool IsLocked()
    {
        return locked;
    }

}
