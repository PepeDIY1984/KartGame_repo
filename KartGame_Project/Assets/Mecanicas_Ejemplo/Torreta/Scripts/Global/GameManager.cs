using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton


    public static GameManager Instance { get; private set; }

    public TextMeshProUGUI textAmmo;
    public int gunAmmo = 50;
    public TextMeshProUGUI textHealth;
    public int health = 100;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        textAmmo.text = gunAmmo.ToString();
        textHealth.text = health.ToString();
    }
    
    public void loseHealth(int healthReduction)
    {
        health -= healthReduction;
    }


    public void checkHealth()
    {
        if (health<=0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

}
