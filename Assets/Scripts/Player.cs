using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State
{
    Moving = 0,
    Idle = 1,
}
public class Player : MonoBehaviour
{

    private State PlayerState = State.Idle;


    private Vector2 Initial = Vector2.zero;
    private Vector2 Target = Vector2.zero;


    private Transform TargetElementTransform;

    /// <summary>
    /// This method will move the player to respective target position.
    /// </summary>
    /// <param name="position"></param>
   public void MoveTo(Transform targetElement) // this move will be a command stored in an list of commands.
    {
        TargetElementTransform = targetElement;

        Target = targetElement.position;
        Initial = transform.position;
        PlayerState = State.Moving;
       // transform.position = vect; // THIS WILL WORK IF WE ARE MOVING IN A STRAIGHT LINE
        
    }

    private void Update()
    {
        var time = Time.deltaTime;
        if(PlayerState == State.Moving)
        {
            transform.position = Vector2.Lerp(Initial, Target, time);
            if (Vector2.Distance(Initial, Target)< 0.1f)
            {
                Destroy(TargetElementTransform.gameObject);
                PlayerState = State.Idle;
                time = 0;
            };
        }


    }


}
