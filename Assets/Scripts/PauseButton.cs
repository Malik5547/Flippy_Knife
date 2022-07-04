using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    GameManager gameManager;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Activate()
    {
        pauseMenu.SetActive(true);
        gameManager.Pause();
        Debug.Log("Pause");
    }
}
