using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public Text healthText;
    int health = 100;
    public delegate void PlayerKilled();
    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            health -= 20;
            healthText.text = "Health: " + health.ToString();
        }
    }

    void Die()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
