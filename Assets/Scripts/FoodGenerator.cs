using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public GameObject foodPrefab;

    private MyGrid myGrid;
    Node bottomLeft;
    Node topRight;

    // Start is called before the first frame update
    void Start()
    {
        myGrid = GetComponent<MyGrid>();

        bottomLeft = myGrid.NodeFromGridPos(0, 0);
        topRight = myGrid.NodeFromGridPos(myGrid.GridSizeX-1, myGrid.GridSizeY-1);

        InvokeRepeating("Generate", 4, 4);
    }

    void Generate()
    {
        Node randomNode = myGrid.randomNode();

        if (randomNode != null)
        {
            Instantiate(foodPrefab, randomNode.worldPosition, Quaternion.identity);

            myGrid.SetNodeIsEmpty(randomNode.worldPosition, false);
        }
    }
}
