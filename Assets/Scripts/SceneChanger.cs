/*
 * Name: Danny Rosemond
 * Date: 3/20/25
 * Desc: Change scenes in game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneChanger : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Back()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Quack()
    {
        
    }
 
}
