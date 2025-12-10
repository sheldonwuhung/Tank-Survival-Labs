using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameStatusController gameStatusController;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI enemiesLeftText;
    public GameObject gameInfoContainer;

    public int score = 0;

    public int currentScore = 0;
    public int wave = 0;
    public int enemiesLeft = 0;
    public int tempHealth = 150;
    public GameObject enemyContainer;
    public PlayerController playerController;

    public bool switchingLevels = false;
    public bool loaded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateText();
    }

    private void Update()
    {
        if (enemyContainer != null)
        {
            enemiesLeft = enemyContainer.transform.childCount;
            if (enemiesLeftText != null)
            {
                enemiesLeftText.text = "Enemies: " + enemiesLeft;
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0) // Title screen
        {
            ResetData();
        }

        if (gameInfoContainer == null)
        {
            GameObject found = GameObject.FindWithTag("GameInfoContainer");
            if (found != null)
            {
                SetParameters();
                UpdateText();
            }
        }

        loaded = true;
    }

    private void SetParameters()
    {
        print(GameObject.FindWithTag("ScoreText").transform.GetChild(0).name);
        scoreText = GameObject.FindWithTag("ScoreText").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        waveText = GameObject.FindWithTag("WaveText").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        enemiesLeftText = GameObject.FindWithTag("EnemiesText").transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        enemyContainer = GameObject.Find("EnemyContainer");
        gameStatusController = GameObject.Find("GameStatusContainer").transform.GetChild(0).GetComponent<GameStatusController>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.health = tempHealth;

    }

    private void UpdateText()
    {
        if (scoreText != null) scoreText.text = "Score: " + score;
        if (waveText != null) waveText.text = "Wave: " + wave;
    }

    public void RestartLevel()
    {
        loaded = false;
        score = currentScore;
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void LoadNextLevel()
    {
        loaded = false;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            if (playerController)
            {
                currentScore -= playerController.maxHealth + playerController.health;
                tempHealth = playerController.health;
            }
            currentScore += score;
            SceneManager.LoadScene(nextIndex);
            wave += 1;
        }
        else
        {
            Debug.Log("🎉 Game complete! Restarting to Title...");
            SceneManager.LoadScene(0);
            wave = 0;
        }
    }



    public void LoadNextLevelWithDelay(float delay)
    {
        StartCoroutine(LoadNextSceneDelayed(delay));
    }

    public System.Collections.IEnumerator LoadNextSceneDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        LoadNextLevel();
    }
    
    public void RestartLevelWithDelay(float delay)
    {
        StartCoroutine(RestartLevelDelayed(delay));
    }
    
    public System.Collections.IEnumerator RestartLevelDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        RestartLevel();
    }


    // Optional reset
    public void ResetData()
    {
        currentScore = 0;
        score = 0;
        wave = 0;
    }
}
