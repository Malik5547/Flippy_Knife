using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    static public string savePath;

    public GameObject pauseMenu;
    public GameObject winScreen;
    public TextMeshProUGUI coinCountText;
    public TextMeshProUGUI throwCountText;
    public Timer timer;

    public Player player;

    private int coinCount = 0;
    private int throwCount = 0;

    private int lastScene = 0;

    static private GameManager _instance = null;

    List<LevelData> levelDatas;

    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            savePath = Application.persistentDataPath + "/leveldata.dat";
            Debug.Log("Game manager awake called.");
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }

    public LevelData GetLevelData(int index)
    {
        return levelDatas[index];
    }

    public void SetLevelData(int index, LevelData levelData)
    {
        levelDatas[index] = levelData;
    }

    static public void SetPlayableKnife(GameObject knifePrefab)
    {
        _instance.player.SetKnife(knifePrefab);
    }

    public void Win()
    {
        player.SetBlockInput(true);
        winScreen.SetActive(true);
        timer.PauseTimer();

        //Update coin score
        int tCoins = PlayerPrefs.GetInt("coins");
        tCoins += coinCount;
        PlayerPrefs.SetInt("coins", tCoins);
        coinCount = 0;

        int levelReached = PlayerPrefs.GetInt("level");
        PlayerPrefs.SetInt("level", levelReached + 1);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        lastScene = SceneManager.GetActiveScene().buildIndex + 1;
        timer.ResetTimer();
        throwCount = 0;
        throwCountText.SetText(throwCount.ToString());
    }

    static public void SelectKnife()
    {
        Time.timeScale = 1f;
        SelectionManager.SetPreviousScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("KnifeSelection");
    }

    public void AddCoin()
    {
        coinCount++;
        coinCountText.SetText(coinCount.ToString());
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timer.ResetTimer();
        throwCount = 0;
        throwCountText.SetText(throwCount.ToString());
    }

    public void Pause()
    {


        Debug.Log("Paused");
        Time.timeScale = 0f;
        //pauseMenu.SetActive(true);
        player.SetBlockInput(true);
        timer.PauseTimer();
    }

    public void Resume()
    {
        Debug.Log("resume");
        Time.timeScale = 1f;
        StartCoroutine(ActivateIput());
        timer.ResumeTimer();
    }

    static public void MainMenu()
    {
        Debug.Log("Loading Main Menu.");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Menu");
    }

    public void ThrowCountIncrease()
    {
        throwCount++;
        throwCountText.SetText(throwCount.ToString());
    }

    //Activate inout after resuming game
    IEnumerator ActivateIput()
    {
        yield return new WaitForFixedUpdate();
        player.SetBlockInput(false);
    }
}
