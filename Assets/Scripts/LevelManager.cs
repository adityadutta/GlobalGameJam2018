using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void Quit()
    {
        Application.Quit();
    }

    public void OnStart()
    {
        SceneManager.LoadScene("Forest");
    }
}
