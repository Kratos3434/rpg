using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNode
{
    public Vector2 position;
    public int moveCost;
    public float distance;
    public int cost;
    public MyNode sourceNode;

    public MyNode(Vector2 position, int moveCost, float distance, MyNode sourceNode)
    {
        this.position = position;
        this.moveCost = moveCost;
        this.distance = distance;
        cost = (int)(distance + moveCost);
        this.sourceNode = sourceNode;
    }
}
