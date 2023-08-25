using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PlayerScore
{
    public int score;
    public int grappleAttemptsLeft;
}

public class ScoreManager : MonoBehaviour
{
    public  PlayerScore CurrentScore;
    
    public List<LevelData> levelDatas;

    public int currentLevel = 1;


    private void Start()
    {
        OnGameLoaded();
    }

    private void OnGameLoaded()
    {
        SetScore();
        SetGrapplingAttempts();
    }

    private void SetGrapplingAttempts()
    {
        CurrentScore.grappleAttemptsLeft = levelDatas[0].InitialAttempts;
        UImanager.OnGrappleFiredUpdated.Invoke(CurrentScore.grappleAttemptsLeft);
    }

    private void SetScore()
    {
        CurrentScore.score = 0;
        UImanager.OnScoreUpdated.Invoke(CurrentScore.score);
    }
}
