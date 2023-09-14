using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private BonusMessage bonusMessage;
    private EnemyControl enemyController;

    private HighScore highScoreController;

    public Text gameOverText;
    public Text generalMessage;
    public Text scoreText;
    public Text waveText;

    public GameObject RestartBtn;
    public GameObject MainMenuBtn;
    public GameObject PauseBtn;

    public GameObject warpWindow;

    public float startWait;
    public float waveWait;
    public int wave;
    public int score;
    public float shotsFired;
    public float destroyed;
    public bool warping;
    public bool gameOver;

    private bool enemysCleared;
    private bool restart;

    void Start()
    {
        wave = 1;
        score = 0;

        restart = false;
        warping = false;
        gameOver = false;

        RestartBtn.SetActive(false);
        MainMenuBtn.SetActive(false);
        PauseBtn.SetActive(true);

        enemyController = GameObject.FindGameObjectWithTag("EnemySpawn").GetComponent<EnemyControl>();
        bonusMessage = GetComponent<BonusMessage>();
        highScoreController = GetComponent<HighScore>();

        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart && Input.GetKeyDown(KeyCode.R) && !highScoreController.enterInitials.activeSelf)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        scoreText.text = "Score: " + score;
        destroyed++;
    }

    public void GameOver()
    {
        gameOver = true;
        restart = true;

        PauseBtn.SetActive(false);

        gameOverText.text = "Game Over!";

        StartCoroutine(highScoreController.displayHighScores());
    }


    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        // generalMessage.text = "";


        while (true)
        {
            enemysCleared = false;

            destroyed = 0;
            shotsFired = 0;

            scoreText.text = "Score: " + score;
            waveText.text = "Wave: " + wave;

            yield return new WaitForSeconds(waveWait);
            StartCoroutine(enemyController.SpawnEnemies());

            while (!enemysCleared)
            {
                enemysCleared = enemyController.checkForEnemies();
                yield return new WaitForSeconds(0.01f);
            }

            if (gameOver) break;

            bonusMessage.AccuracyTrigger();
            StartCoroutine(ResetPos());

            wave++;
        }
    }

    public IEnumerator ResetPos()
    {
        var instantiatedWarp = Instantiate(warpWindow, new Vector3(0, 0, 50), Quaternion.identity);
        Destroy(instantiatedWarp, 3);

        warping = true;
        yield return new WaitForSeconds(3.0f);
        warping = false;
    }
}
