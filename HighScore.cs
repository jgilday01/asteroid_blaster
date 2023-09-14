using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
    private GameController gameController;

    public GameObject enterInitials;
    public GameObject displayHighScore;
    public TMPro.TMP_InputField userInitials;
    public TMP_Text[] nameTextArray;
    public TMP_Text[] scoreTextArray;


    void Start()
    {
        gameController = GetComponent<GameController>(); //on the same GameObject
    }

    public IEnumerator displayHighScores()
    {
        yield return new WaitForSeconds(3);

        gameController.gameOverText.text = "";

        checkSavedScores();
    }

    private void checkSavedScores()
    {
        int currentPoints = gameController.score;

        int minleader = PlayerPrefs.GetInt("minleader", 0);
        int highscore = PlayerPrefs.GetInt("highscore", 0);

        if (currentPoints <= minleader) processScores(0);
        else
        {
            enterInitials.SetActive(true);
            userInitials.ActivateInputField();
        }
    }

    public void processScores(int updateMode)
    {
        var scoresList = new List<(string, int)>();

        for (int i = 0; i < 3; i++)
        {
            string name = PlayerPrefs.GetString("name" + (i + 1).ToString(), "-");
            int score = PlayerPrefs.GetInt("score" + (i + 1).ToString(), 0);
            scoresList.Add((name, score));
        }

        if (updateMode == 0) scoresList.Add(("-", 0));
        else scoresList.Add((userInitials.text.ToUpper(), gameController.score));

        scoresList.Sort((e1, e2) => { return e2.Item2.CompareTo(e1.Item2); });

        displayAndSave(scoresList);
    }


    private void displayAndSave(List<(string, int)> scoresList)
    {
        enterInitials.SetActive(false);
        displayHighScore.SetActive(true);

        gameController.RestartBtn.SetActive(true);
        gameController.MainMenuBtn.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            string suffix = (i + 1).ToString();

            nameTextArray[i].text = scoresList[i].Item1;
            PlayerPrefs.SetString("name" + suffix, scoresList[i].Item1);

            scoreTextArray[i].text = scoresList[i].Item2.ToString();
            PlayerPrefs.SetInt("score" + suffix, scoresList[i].Item2);
        }

        PlayerPrefs.SetInt("highscore", scoresList[0].Item2);
        PlayerPrefs.SetInt("minleader", scoresList[2].Item2);

        PlayerPrefs.Save();
    }
}
