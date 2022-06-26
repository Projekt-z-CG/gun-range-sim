using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**
 * Script used for changing current scenes
 */
public class GameModeMenu : MonoBehaviour
{
    // Changes current scene to Zombie Map
    public void OnZombieButtonClicked()
    {
        SceneManager.LoadScene("Zombie Map");
    }

    // Changes current scene to Parkour Map
    public void OnParkourButtonClicked()
    {
        SceneManager.LoadScene("Parkour Map");
    }

    // Changes current scene to Gun Range Map
    public void OnGunRangeButtonClicked()
    {
        SceneManager.LoadScene("Gun Range Map");
    }
}
