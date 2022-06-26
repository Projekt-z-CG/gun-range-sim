using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Script used for quiting the game
 */
public class MainMenu : MonoBehaviour
{
    // Quits game
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
