using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("enter down");
            LoadMain();
        }
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("main");
    }
}
