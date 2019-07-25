using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    public GameObject bodyPrefab;
    public MyGrid myGrid;

    // velocidade inicial
    public float speed = 1.0f;

    // tempo de espera para executar um novo movimento
    public float waitTime = 0.3f;

    private Vector2 dir;
    private Vector2 oldDir;

    private Rigidbody2D myRigidbody;
    private List<Transform> tail = new List<Transform>();

    private bool collidedWithFood = false;
    private bool dead = false;
    private Reporter reporter;

    void Start()
    {
        dead = false;
        dir = Vector2.right;
        oldDir = Vector2.zero;
        myRigidbody = GetComponent<Rigidbody2D>();

        myGrid.SetNodeIsEmpty(transform.position, false);

        MyGameManager gameManager = GameObject.Find("Controllers").GetComponent<MyGameManager>();
        reporter = gameManager.GetReporter();
        reporter.SnakeWon += SnakeClearStage;

        Invoke("Move", waitTime);
    }

    private void SnakeClearStage()
    {
        freeze(true);
    }

    private void Move()
    {
        if (!dead)
        {
            Vector2 v = transform.position;

            transform.Translate(dir);
            oldDir = dir;

            myGrid.SetNodeIsEmpty(transform.position, false);

            if (collidedWithFood)
            {
                GameObject g = Instantiate(bodyPrefab, v, Quaternion.identity);
                tail.Insert(0, g.transform);
                collidedWithFood = false;
                myGrid.SetNodeIsEmpty(v, false);
            }
            else
            {
                if (tail.Count > 0)
                {
                    myGrid.SetNodeIsEmpty(tail.Last().position, true);

                    tail.Last().position = v;

                    myGrid.SetNodeIsEmpty(v, false);

                    tail.Insert(0, tail.Last());
                    tail.RemoveAt(tail.Count - 1);
                }
                else
                {
                    myGrid.SetNodeIsEmpty(v, true);
                }
            }

            Invoke("Move", waitTime);
        }
    }

    private void increaseSpeed()
    {
        if (waitTime > 0.045f)
        {
            // aumenta 5% da velocidade atual a cada chamada
            float newSpeed = speed * 1.05f;

            float newWaitTime = (speed * waitTime) / newSpeed;

            speed = newSpeed;
            waitTime = Mathf.Max(newWaitTime, 0.045f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            collidedWithFood = true;
            Destroy(collision.gameObject);

            reporter.InformFoodWasEaten();

            // aumenta velocidade da cobra
            increaseSpeed();
        }
        else
        {
            freeze(true);

            Destroy(gameObject);

            for (int i = 0; i < tail.Count; ++i)
            {
                Destroy(tail[i].gameObject);
            }

            reporter.InformSnakeDied();
        }
    }

    private void freeze(bool enable)
    {
        dead = enable;

        if (enable)
            CancelInvoke("Move");
        else
            Invoke("Move", waitTime);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && (oldDir != Vector2.left || tail.Count == 0))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.LeftArrow) && (oldDir != Vector2.right || tail.Count == 0))
            dir = Vector2.left;
        else if (Input.GetKey(KeyCode.UpArrow) && (oldDir != Vector2.down || tail.Count == 0))
            dir = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow) && (oldDir != Vector2.up || tail.Count == 0))
            dir = Vector2.down;
    }
}
