using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    public int bestScore;

    public void saveBest(int newBest)
    {
        PlayerPrefs.SetInt("BestScore", newBest);
    }

    public int loadBest()
    {
        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        else
        {
            bestScore = 0;
        }
        return bestScore;

    }
}
