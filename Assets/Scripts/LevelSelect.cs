using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int noOfLevels;
    public GameObject levelButton;
    public RectTransform ParentPanel;
    int levelReached;

    void Awake()
    {
        LevelButtons();
    }

   void LevelButtons()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            levelReached = PlayerPrefs.GetInt("level");
            Debug.Log("Reached level: " + levelReached);
        }
        else
        {
            PlayerPrefs.SetInt("level", 1);
            levelReached = PlayerPrefs.GetInt("level");
        }

        for (int i = 0; i < noOfLevels; i++)
        {
            int x = new int();
            x = i + 1;

            GameObject lvlButton = Instantiate(levelButton);
            lvlButton.transform.SetParent(ParentPanel, false);
            TMP_Text buttonText = lvlButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = (i + 1).ToString();

            lvlButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate
            {
                LevelSelected(x);
            });

            if ((i + 1) > levelReached)
            {
                lvlButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    void LevelSelected(int index)
    {
        SceneManager.LoadScene("Level " + index);
        Debug.Log("Level Selected: " + index.ToString());
    }

    public void BackClicked()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
