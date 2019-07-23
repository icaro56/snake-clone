using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    public GameObject bodyPrefab;
    public MyGrid myGrid;

    private float mySize = 1.0f;
    private Vector2 dir;
    private Rigidbody2D myRigidbody;
    private List<Transform> tail = new List<Transform>();
    private bool ate = false;

    void Start()
    {
        dir = Vector2.right * mySize;
        myRigidbody = GetComponent<Rigidbody2D>();

        myGrid.SetNodeIsEmpty(transform.position, false);

        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    private void Move()
    {
        Vector2 v = transform.position;

        transform.Translate(dir);
        //myRigidbody.MovePosition((Vector2)transform.position + dir);

        myGrid.SetNodeIsEmpty(transform.position, false);

        if (ate)
        {
            GameObject g = Instantiate(bodyPrefab, v, Quaternion.identity);
            tail.Insert(0, g.transform);
            ate = false;
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            ate = true;
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right * mySize;
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = Vector2.left * mySize;
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up * mySize;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = Vector2.down * mySize;
    }
}
