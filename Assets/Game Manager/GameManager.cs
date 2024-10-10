using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; 
    public int TurnsSurvived { get; private set; } = 0; //the number of turns the player has survived, if it exceeds a threshold the player wins the round
    public int TurnsRequiredToWin = 4; 
    public int PlayerHealth { get; private set; } //the player's health. if it reaches 0 the player loses. this probably shouldn't be on the game manager and should find a 
    //home on a player script, but i'll leave it here for now. 


    public void OnEnable()
    {
        //setup singleton
        if(Instance == null) Instance = this;
        else Destroy(this);
    }

    //reduce the player's health by x
    public void ReducePlayerHealth(int amount)
    {
        PlayerHealth -= amount;
    }

}
