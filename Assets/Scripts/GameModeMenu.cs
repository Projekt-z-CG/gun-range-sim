using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameModeMenu : MonoBehaviour
{
    public void On1v1ButtonClicked()
    {
        SceneManager.LoadScene("1v1 Map");
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
