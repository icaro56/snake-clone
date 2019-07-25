using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Reporter))]
public class MyGameManager : MonoBehaviour
{
    private Reporter reporter;

    private void Awake()
    {
        reporter = GetComponent<Reporter>();

        reporter.SnakeDied += GameOverBad;
        reporter.SnakeWon += GameOverGood;
    }

    private void OnDestroy()
    {
        reporter.SnakeDied -= GameOverBad;
        reporter.SnakeWon -= GameOverGood;
    }

    public Reporter GetReporter()
    {
        return reporter;
    }

    void GameOverBad()
    {
        Debug.Log("A cobra morreu");
    }

    void GameOverGood()
    {
        Debug.Log("A cobra venceu. Comeu todas as comidas possíveis");
    }
}
