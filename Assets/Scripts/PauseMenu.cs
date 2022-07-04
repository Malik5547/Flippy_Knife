using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    private GameManager _gameManager;

    public void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void Select()
    {
        GameManager.SelectKnife();
    }

    public void Resume()
    {
        _gameManager.Resume();
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        GameManager.MainMenu();
    }

    public void Restart()
    {
        _gameManager.Restart();
    }

    public void SelectButtonClicked()
    {
        GameManager.SelectKnife();
    }

}
