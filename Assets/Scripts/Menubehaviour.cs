using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menubehaviour : MonoBehaviour
{
    public SpawnBalloon spawner;
    public TouchBalloon toucher;
    public SaveLoad saveLoad;

    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    public GameObject startGameMenu;
    public GameObject gameMenu;
    public GameObject balloonHolder;

    private bool playGame;
    private int bestScore;

    void Start()
    {
        spawner.speed = 0.5f;
        playGame = false;
        bestScore = saveLoad.loadBest();
    }

    public void gameOver()
    {
        spawner.speed = 0.5f;
        gameOverMenu.SetActive(true);
        gameMenu.SetActive(false);
        gameOverMenu.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = toucher.score.ToString();
        if (bestScore < toucher.score)
        {
            saveLoad.saveBest(toucher.score);
        }
        playGame = false;
    }

    public void startNewGame()
    {
        clearBalloons();
        spawner.speed = 1f;
        toucher.score = 0;
        gameMenu.transform.GetChild(0).GetComponent<Text>().text = toucher.score.ToString();
        gameOverMenu.SetActive(false);
        startGameMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameMenu.SetActive(true);
        playGame = true;
    }

    public void puaseGame()
    {
        if (playGame) 
        { 
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            gameMenu.SetActive(false);
            pauseMenu.transform.GetChild(0).GetChild(3).GetComponent<Text>().text = toucher.score.ToString();
        }
        else 
        { 
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            gameMenu.SetActive(true);
        }
        playGame = !playGame; 
    }



    public void clearBalloons()
    {
        List<Transform> allChildren = new List<Transform>();
        foreach (Transform child in balloonHolder.transform) allChildren.Add(child);
        foreach (Transform child in allChildren)
        {
            if (child != null) { Destroy(child.gameObject); }
        }

    }

}
