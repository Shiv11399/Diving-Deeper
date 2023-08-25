using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public enum State
{
    Moving = 0,
    Idle = 1,
}
public class Player : MonoBehaviour
{
    [Range(0,1)]
    [SerializeField]private float MovemenytSpeed = 0.2f;

    private static State playerState = State.Idle;


    private Vector2 Initial = Vector2.zero;
    private Vector2 Target = Vector2.zero;


    private Transform TargetElementTransform;
    private float time  = 0f;

    public static State PlayerState { get => playerState; private set => playerState = value; }

    /// <summary>
    /// This method will move the player to respective target position.
    /// </summary>
    /// <param name="position"></param>
    ///
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    public void MoveTo(Transform targetElement) // this move will be a command stored in an list of commands.
    {
        if (targetElement.GetComponent<Element>().Type == ElementType.Stone)
        {
            //WrongHit();
            return;
        }
        if (playerState == State.Moving) return;
        TargetElementTransform = targetElement;
        Target = targetElement.position;
        Initial = transform.position;
        playerState = State.Moving;

        
    }

    private async void WrongHit()
    {
        playerState = State.Moving;
        await Task.Delay(1000);
        playerState = State.Idle;
    }

    private void Update()
    {
        if(playerState == State.Moving)
        {
            time += Time.deltaTime * MovemenytSpeed;
            transform.position = Vector2.Lerp(Initial, Target, time);
            if (Vector2.Distance(transform.position, Target) < 0.1f)
            {
                Destroy(TargetElementTransform.gameObject);
                playerState = State.Idle;
                Target = Vector2.zero;
                time = 0;
                ScoreManager.ScoreChanged.Invoke(ScoreType.Score, 1);
            };
        }
    }

    private float SinOscillator(float speed)
    {
        return Mathf.Sin(speed);
    }


}
