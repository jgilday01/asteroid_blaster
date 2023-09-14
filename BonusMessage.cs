using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusMessage : MonoBehaviour
{
    private GameController gameController;
    private EnemyControl enemyControllerObj;

    public Text generalMessage;


    void Start()
    {
        gameController = GetComponent<GameController>();
        enemyControllerObj = GameObject.FindGameObjectWithTag("EnemySpawn").GetComponent<EnemyControl>();
    }

    public void PickupTrigger(int pickupScore)
    {
        gameController.AddScore(pickupScore);
        string[] messages = new string[] { "PICKUP BONUS", "Reset Blasters", "+10" };
        StartCoroutine(ShowMessages(messages));
    }

    public void AccuracyTrigger()
    {
        float accuracy = (gameController.destroyed / gameController.shotsFired) * 100.0f;
        float hitCount = (gameController.destroyed / enemyControllerObj.hazardCount) * 100.0f;

        if (accuracy >= 50.0f && hitCount >= 50.0f)
        {
            string showPercentage = hitCount.ToString("n0") + "%\nCleared";
            int calcPoints = (int)((hitCount * 20) / 100);
            string showPoints = "+" + calcPoints.ToString();

            gameController.AddScore(calcPoints);
            string[] messages = new string[] { "CLEANUP\nBONUS", showPercentage, showPoints };
            StartCoroutine(ShowMessages(messages));
        }
    }

    public IEnumerator ShowMessages(string[] messages)
    {
        foreach (var message in messages)
        {
            if (gameController.gameOver) break;

            generalMessage.text = message;
            yield return new WaitForSeconds(1.0f);
        }
        generalMessage.text = "";
    }
}
