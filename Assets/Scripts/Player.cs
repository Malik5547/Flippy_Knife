using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private CinemachineVirtualCamera cmCamera;
    public ParticleSystem _particleSystem;
    public GameObject defaultPrefab;
    public LineRenderer throwingLine;
    public float lineLength = 1f;

    private bool _blockInput = false;

    private Knife currentKnife;
    static private GameObject _knifePrefab = null;

    private void Start()
    {
        if (_knifePrefab == null) 
            SetKnife(defaultPrefab);


        Debug.Log(SelectedKnife.GetKnifePrefab());
        GameObject clone = Instantiate(SelectedKnife.GetKnifePrefab(), transform);
        clone.transform.SetParent(transform);
        clone.GetComponent<Knife>().gameManager = gameManager;
        clone.GetComponent<Knife>().throwingLine = throwingLine;
        clone.GetComponent<Knife>().lineLength = lineLength;
        throwingLine.transform.SetParent(clone.transform, true);

        currentKnife = clone.GetComponent<Knife>();
        Debug.Log("Current Knife: " + currentKnife);

        currentKnife.SetParticleSystem(_particleSystem);

        cmCamera.Follow = clone.transform;
        cmCamera.LookAt = clone.transform;
    }

    private void Update()
    {
        if(!_blockInput)
        if (Input.GetAxisRaw("Cancel") == 1){
            Debug.Log("Cancel");
            gameManager.Pause();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.Win();
        }
    }


    public void SetKnife(GameObject knifePrefab)
    {
        _knifePrefab = knifePrefab;
    }

    public void SetBlockInput(bool val)
    {
        _blockInput = val;
        
        if(currentKnife == null)
        {
            currentKnife = FindObjectOfType<Knife>();
        }

        currentKnife.SetBlockInput(val);
    }
}
