using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PlayerScore
{
    public int score;
    public int grappleAttemptsLeft;
}
public enum ScoreType
{
    Score = 0,
    Attempts = 1,   
}
public class ScoreManager : MonoBehaviour
{
    public  PlayerScore CurrentScore;
    
    public List<LevelData> levelDatas;

    public int currentLevel = 1;

    public static UnityAction<ScoreType, int> ScoreChanged;


    private void OnEnable()
    {
        ScoreChanged += SetScore;
    }
    private void OnDisable()
    {
        ScoreChanged -= SetScore;
    }

    void SetScore(ScoreType type, int value)
    {
        switch (type)
        {
            case ScoreType.Score:
                CurrentScore.score += value;
                UImanager.OnScoreUpdated.Invoke(CurrentScore.score);
                break;
            case ScoreType.Attempts:
                CurrentScore.grappleAttemptsLeft += value ;
                UImanager.OnGrappleFiredUpdated.Invoke(CurrentScore.grappleAttemptsLeft);
                break;
            default:
                break;
        }
    }
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
