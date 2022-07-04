using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedKnife : MonoBehaviour
{

    public GameObject defaultKnifePrefab;

    static GameObject _knifePrefab;

    static private SelectedKnife _instance = null;

    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            _knifePrefab = defaultKnifePrefab;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static void SetSelectedKnife(GameObject knifePrefab)
    {
        _knifePrefab = knifePrefab;
    }

    public static GameObject GetKnifePrefab()
    {
        return _knifePrefab;
    }

}
