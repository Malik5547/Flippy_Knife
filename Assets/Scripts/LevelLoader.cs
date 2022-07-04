using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public float transitionTime = 1f;

    public GameObject[] coins;

    private List<LevelData> levelDatas;

    //private GameManager gameManager;
    private int levelIndex;

    private void Awake()
    {
        SaveSystem.SetPath(Application.persistentDataPath + "/leveldata.dat");
        levelDatas = SaveSystem.GetLevelData();

        levelIndex = int.Parse(SceneManager.GetActiveScene().name.Replace("Level ", "")) - 1;

        Debug.Log("Level data: " + levelDatas);

        if (levelDatas != null)
        {
            if (levelDatas.Count >= levelIndex + 1)
            {
                Debug.Log("Coin data: " + levelDatas[levelIndex].coins.ToString());

                for (int i = 0; i < levelDatas[levelIndex].coins.Length; i++)
                {
                    if (levelDatas[levelIndex].coins[i])
                    {
                        break;
                    }
                    else
                    {
                        coins[i].GetComponent<Coin>().SetCollected(true);
                    }
                }

                return;
            }
        } else
            levelDatas = new List<LevelData>();

        bool[] coinData = new bool[coins.Length];

        for (int i = 0; i < coins.Length; i++)
        {
            coinData[i] = coins[i].activeSelf; 
        }

        LevelData levelData = new LevelData(coinData);

        levelDatas.Add(levelData);
    }

    public void LaodNextLevel()
    {
        bool[] coinData = new bool[coins.Length];

        for (int i = 0; i < coins.Length; i++)
        {
            coinData[i] = !coins[i].GetComponent<Coin>().IsCollected();
        }

        LevelData levelData = new LevelData(coinData);

        if (levelDatas != null)
        {
            if (levelDatas.Count >= levelIndex + 1)
            {
                levelDatas[levelIndex] = levelData;
            } else
            {
                Debug.Log("Adding to level data: " + levelData);
                levelDatas.Add(levelData);
            }
        }
        else
        {
            Debug.Log("Level index: " + levelIndex);
            levelDatas = new List<LevelData>();
            levelDatas.Add(levelData);
        }


        SaveSystem.SaveLevelData(levelDatas);

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        Debug.Log("After wait.");
        SceneManager.LoadScene(levelIndex);
    }

}
