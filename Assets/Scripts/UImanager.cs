using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UImanager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI GrapplingText;


    public static UnityAction<int> OnScoreUpdated;
    public static UnityAction<int> OnGrappleFiredUpdated;

    private void OnEnable()
    {
        OnScoreUpdated += UpdateScore;
        OnGrappleFiredUpdated += UpdateGrapplingText;
    }
    private void OnDisable()
    {
        OnScoreUpdated -= UpdateScore;
        OnGrappleFiredUpdated -= UpdateGrapplingText;
    }
    void UpdateScore(int score)
    {
        ScoreText.text = score.ToString();
    }
    void UpdateGrapplingText(int value)
    {
        GrapplingText.text = value.ToString();
    }
}
