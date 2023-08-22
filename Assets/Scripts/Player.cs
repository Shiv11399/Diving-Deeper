using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /// <summary>
    /// This method will move the player to respective target position.
    /// </summary>
    /// <param name="position"></param>
   void MoveTo(Vector2 position)
    {
        transform.Translate(position); // THIS WILL WORK IF WE ARE MOVING IN A STRAIGHT LINE 
    }
}
