using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform fuel;
    [SerializeField] float sceneDelay = 1;

    int fuelCount;
    int score = 0;
    bool gameOngoing = true;
    bool crashesEnabled = true;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Debug.Log("Welcome to Missiont50!");
        }
        fuelCount = fuel.GetComponentsInChildren<CapsuleCollider>().Length;
    }

    void Update()
    {
        CheckDebugKeys();
    }


    public void Win()
    {
        gameOngoing = false;
        Invoke("LoadNextLevel", sceneDelay);
    }

    public void Lose()
    {
        gameOngoing = false;
        Invoke("ResetScene", sceneDelay);
    }


    public void CollectFuel(GameObject fuel)
    {
        fuel.SetActive(false);
        score++;
        Debug.Log("You've gotten fuel " + $"{score}/{fuelCount}");
    }

    public bool AllFuelRetrieved()
    {
        return score >= fuelCount;
    }

    public bool GameOngoing()
    {
        return gameOngoing;
    }

    public bool CrashesEnabled()
    {
        return crashesEnabled; 
    }

    private void CheckDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            Debug.Log("Debug Key: next level loaded");
            gameOngoing = false;
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Debug Key: crashes disabled");
            crashesEnabled = false;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Debug Key: restarting game");
            SceneManager.LoadScene(0);
        }
    }


    // Helper Methods \\
    private void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadNextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
