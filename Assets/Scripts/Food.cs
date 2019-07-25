using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    private void Start()
    {
        InvokeRepeating("twinkle", 0.2f, 0.2f);
    }

    private void twinkle()
    {
        GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
    }

    private void OnDestroy()
    {
        CancelInvoke("twinkle");
    }
}
