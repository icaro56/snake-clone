using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public GameObject foodPrefab;

    private MyGrid myGrid;

    private Reporter reporter;

    // Start is called before the first frame update
    void Start()
    {
        myGrid = GetComponent<MyGrid>();

        reporter = transform.GetComponent<MyGameManager>().GetReporter();
        reporter.FoodWasEaten += GenerateNewFood;

        Invoke("GenerateNewFood", 2.0f);
    }

    private void OnDestroy()
    {
        reporter.FoodWasEaten -= GenerateNewFood;
    }

    void GenerateNewFood()
    {
        bool foodWasGenerated = Generate();

        // Se comida não foi gerada, então não há lugar para gerar comida. Logo, a cobra foi a vencedora
        if (!foodWasGenerated)
        {
            reporter.InformSnakeWon();
        }
    }

    bool Generate()
    {
        Node randomNode = myGrid.randomNode();

        if (randomNode != null)
        {
            Instantiate(foodPrefab, randomNode.worldPosition, Quaternion.identity);

            myGrid.SetNodeIsEmpty(randomNode.worldPosition, false);

            return true;
        }

        return false;
    }
}
