using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBoton()
    {

        SceneManager.LoadScene(1);

    }

    public void ExitBoton()
    {

        Application.Quit();

    }

    public void LeaderboardBoton()
    {
        SceneManager.LoadScene(2);

    }
}
