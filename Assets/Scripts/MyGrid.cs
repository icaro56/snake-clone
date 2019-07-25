using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public bool displayGridGizmos;
    public Vector2 gridWorldSize;
    public float nodeRadius;

    private int offsetY;

    Node[,] grid;

    Dictionary<uint, Node> emptyNodes;

    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    void Awake()
    {
        emptyNodes = new Dictionary<uint, Node>();

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        offsetY = -1 * (int)transform.localPosition.y;

        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    public int GridSizeX
    {
        get
        {
            return gridSizeX;
        }
    }

    public int GridSizeY
    {
        get
        {
            return gridSizeY;
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        uint countId = 0;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);

                grid[x, y] = new Node(worldPoint, x, y, countId);

                emptyNodes.Add(countId, grid[x, y]);

                ++countId;
            }
        }
    }

    public Node randomNode()
    {
        List<Node> list = emptyNodes.Values.ToList();

        if (list.Count > 0)
        {
            int index = Random.Range(0, list.Count);

            return list[index];
        }
        else
            return null;
    }

    public void SetNodeIsEmpty(Vector2 worldPosition, bool isEmpty)
    {
        Node node = NodeFromWorldPoint(worldPosition);

        if (node != null)
        {
            node.isEmpty = isEmpty;

            if (emptyNodes.ContainsKey(node.Id))
            {
                if (!isEmpty)
                {
                    emptyNodes.Remove(node.Id);
                }
            }
            else
            {
                if (isEmpty)
                {
                    emptyNodes.Add(node.Id, node);
                }
            }

            //Instantiate(debugObject, node.worldPosition, Quaternion.identity);
        }
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        worldPosition = new Vector2(worldPosition.x, worldPosition.y + offsetY);

        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public Node NodeFromGridPos(int x, int y)
    {
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null && displayGridGizmos)
        {
            // Node playerNode = NodeFromWorldPoint(player.position);
            foreach (Node n in grid)
            {
                Gizmos.color = Color.white;

                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }
}

