
using UnityEngine;

public class Node
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2 worldPosition { get; set; }

    public bool isEmpty { get; set; }

    public uint Id { get; set; }

    public Node(Vector2 worldPos, int gridX, int gridY, uint id)
    {
        worldPosition = worldPos;
        X = gridX;
        Y = gridY;
        isEmpty = true;
        Id = id;
    }
}
