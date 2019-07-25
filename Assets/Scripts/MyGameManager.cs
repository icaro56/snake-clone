using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Reporter))]
public class MyGameManager : MonoBehaviour
{
    public static int highScore = 0;
    public int foodScore = 0;

    public Text foodValueText;
    public Text highValueText;
    public Text finalMessageText;

    private Reporter reporter;

    private void Awake()
    {
        finalMessageText.enabled = false;
        foodScore = 0;

        reporter = GetComponent<Reporter>();

        reporter.SnakeDied += GameOverBad;
        reporter.SnakeWon += GameOverGood;
        reporter.FoodWasEaten += IncrementFoodValue;

        UpdateHud();
    }

    private void OnDestroy()
    {
        reporter.SnakeDied -= GameOverBad;
        reporter.SnakeWon -= GameOverGood;
        reporter.FoodWasEaten -= IncrementFoodValue;
    }

    public Reporter GetReporter()
    {
        return reporter;
    }

    IEnumerator backToMenu(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("menu");
    }

    void GameOverBad()
    {
        finalMessageText.enabled = true;
        finalMessageText.text = "GAME OVER";
        Debug.Log("A cobra morreu");
        StartCoroutine(backToMenu(5.0f));
    }

    void GameOverGood()
    {
        finalMessageText.enabled = true;
        finalMessageText.text = "YOU WON";
        Debug.Log("A cobra venceu. Comeu todas as comidas possíveis");
        StartCoroutine(backToMenu(10.0f));
    }

    void IncrementFoodValue()
    {
        foodScore++;

        highScore = foodScore > highScore ? foodScore : highScore;

        UpdateHud();
    }

    void UpdateHud()
    {
        foodValueText.text = foodScore.ToString();
        highValueText.text = highScore.ToString();
    }
}
