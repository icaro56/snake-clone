using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public AudioClip startSound;

    private void Awake()
    {
        Screen.SetResolution(1024, 768, false);
    }

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
        SoundManager.instance.PlaySingle(startSound);
        SceneManager.LoadScene("main");
    }
}
