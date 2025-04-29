using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPathFinder : MonoBehaviour
{

    private Vector2 currentPos = new Vector2(1.53f, -3.49f);
    private Vector2 targetPos = new Vector2(-6.57f, -0.53f);
    private List<MyNode> visited = new List<MyNode>();
    private List<MyNode> open = new List<MyNode>();
    private List<Vector2> path = new List<Vector2>();

    private void Awake()
    {
        FindPath();
    }

    private void FindPath()
    {
        MyNode currentNode = new MyNode(currentPos, 0, 0, null);
        visited.Add(currentNode);
        //MyNode right = new MyNode(GetRight(), 1, Vector2.Distance(GetRight(), targetPos), currentNode);
        //MyNode left = new MyNode(GetLeft(), 1, Vector2.Distance(GetLeft(), targetPos), currentNode);
        //MyNode up = new MyNode(GetUp(), 1, Vector2.Distance(GetUp(), targetPos), currentNode);
        //MyNode down = new MyNode(GetDown(), 1, Vector2.Distance(GetDown(), targetPos), currentNode);
        //MyNode upRight = new MyNode(GetUpRight(), Mathf.Sqrt(2), Vector2.Distance(GetUpRight(), targetPos), currentNode);
        //MyNode downRight = new MyNode(GetDownRight(), Mathf.Sqrt(2), Vector2.Distance(GetDownRight(), targetPos), currentNode);
        //MyNode upLeft = new MyNode(GetUpLeft(), Mathf.Sqrt(2), Vector2.Distance(GetUpLeft(), targetPos), currentNode);
        //MyNode downLeft = new MyNode(GetDownLeft(), Mathf.Sqrt(2), Vector2.Distance(GetDownLeft(), targetPos), currentNode);

        int j = 0;

        while (true)
        {
            SetOpen(GetRight(), 10 + currentNode.cost, Vector2.Distance(GetRight(), targetPos), currentNode);
            SetOpen(GetLeft(), 10 + currentNode.cost, Vector2.Distance(GetLeft(), targetPos), currentNode);
            SetOpen(GetUp(), 10 + currentNode.cost, Vector2.Distance(GetUp(), targetPos), currentNode);
            SetOpen(GetDown(), 10 + currentNode.cost, Vector2.Distance(GetDown(), targetPos), currentNode);
            SetOpen(GetUpRight(), 14 + currentNode.cost, Vector2.Distance(GetUpRight(), targetPos), currentNode);
            SetOpen(GetDownRight(), 14 + currentNode.cost, Vector2.Distance(GetDownRight(), targetPos), currentNode);
            SetOpen(GetUpLeft(), 14 + currentNode.cost, Vector2.Distance(GetUpLeft(), targetPos), currentNode);
            SetOpen(GetDownLeft(), 14 + currentNode.cost, Vector2.Distance(GetDownLeft(), targetPos), currentNode);


            MyNode shortest = open[0];

            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].cost < shortest.cost)
                {
                    shortest = open[i];
                }

                if (open[i].cost == shortest.cost)
                {
                    if (open[i].distance < shortest.distance)
                    {
                        shortest = open[i];
                    }
                } 
            }

            visited.Add(shortest);
            open.Remove(shortest);
            currentPos = shortest.position;
            currentNode = shortest;

            if ((int)targetPos.x == (int)shortest.position.x && (int)targetPos.y == (int)shortest.position.y) {
                MyNode starting = shortest;
                //Debug.Log("Target reached" + shortest.position);
                //Debug.Log("=============Test start===========");
                while (starting != null)
                {
                    if (starting.sourceNode != null)
                    {
                        path.Add(starting.position);
                        //Debug.Log(starting.position);
                    }
                    starting = starting.sourceNode;
                }
                break;
            }
            //Debug.Log(shortest.position);
            j++;
        }
        Debug.Log(path[0]);
        path.Reverse();
        //Debug.Log(path.Count);
        Debug.Log(Vector2.Distance(GetUpRight(), targetPos));
        Debug.Log(Vector2.Distance(GetRight(), targetPos));
    }

    private void SetOpen(Vector2 pos, int moveCost, float distance, MyNode sourceNode)
    {
        MyNode myNode = new MyNode(pos, moveCost, distance, sourceNode);

        if (IsWall(pos))
        {
            //Debug.Log("Wall");
            visited.Add(myNode);
            return;
        }

        if (visited.Contains(myNode))
        {
            return;
        }

        foreach (MyNode node in open)
        {
            if (node.position == pos)
            {
                if (myNode.cost < node.cost)
                {
                    //Debug.Log("Parent node changed");
                    node.sourceNode = sourceNode;
                    node.cost = node.sourceNode.cost + moveCost;
                }
                return;
            }
        }

        open.Add(myNode);
    }

    private bool IsWall(Vector2 pos)
    {
        Collider2D hit = Physics2D.OverlapCircle(pos, .25f, LayerMask.GetMask("Wall"));
        //Debug.Log(pos);
        return hit != null;
    }

    private Vector2 GetRight()
    {
        return currentPos + Vector2.right;
    }

    private Vector2 GetLeft()
    {
        return currentPos - Vector2.right;
    }

    private Vector2 GetUp()
    {
        return currentPos + Vector2.up;
    }

    private Vector2 GetDown()
    {
        return currentPos - Vector2.up;
    }

    private Vector2 GetUpRight()
    {
        return currentPos + (Vector2.up + Vector2.right);
    }

    private Vector2 GetDownRight()
    {
        return currentPos + (Vector2.down + Vector2.right);
    }

    private Vector2 GetUpLeft()
    {
        return currentPos + (Vector2.up + Vector2.left);
    }

    private Vector2 GetDownLeft()
    {
        return currentPos + (Vector2.down + Vector2.left);
    }

    public List<Vector2> GetPath()
    {
        return path;
    }
}
