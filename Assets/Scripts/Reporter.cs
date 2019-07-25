using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reporter : MonoBehaviour
{
    public event System.Action FoodWasEaten;
    public event System.Action SnakeDied;
    public event System.Action SnakeWon;

    private void Awake()
    {
        FoodWasEaten += () => { };
        SnakeDied += () => { };
        SnakeWon += () => { };
    }

    public void InformFoodWasEaten()
    {
        FoodWasEaten();
    }

    public void InformSnakeDied()
    {
        SnakeDied();
    }

    public void InformSnakeWon()
    {
        SnakeWon();
    }
}
