using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Početak igre - START
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Mainmenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    //Izlazak - QUIT
    public void EndGame()
    {
        Application.Quit();

        //u consoli nam javlja da li se igra iskljucuje
        Debug.Log("Igra se isključuje.");
    }

    //O igri - ABOUT
    public void Aboutt()
    {
        SceneManager.LoadScene("About");
    }
}
