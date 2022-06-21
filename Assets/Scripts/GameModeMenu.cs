using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeMenu : MonoBehaviour
{
    public void OnZombieButtonClicked()
    {
        SceneManager.LoadScene("Zombie Map");
    }

    public void OnParkourButtonClicked()
    {
        SceneManager.LoadScene("Parkour Map");
    }

    public void OnGunRangeButtonClicked()
    {
        SceneManager.LoadScene("Gun Range Map");
    }
}
