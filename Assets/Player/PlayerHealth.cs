using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    private int _currentHealth;
    private int _maxPlayerHealth = 60; //cannot exceed this number
    public static event Action<int> OnHealthChange;
    public PlayerHealth(int startingValue)
    {
        _currentHealth = startingValue;
        OnHealthChange?.Invoke(_currentHealth); //push an event with current health (for UI)

    }
    //returns true if the health is above 0. returns false if not.
    public void RemoveHealth(int value)
    {
        _currentHealth -= value;
        //validate health not above 0.
        _currentHealth = Mathf.Max(_currentHealth, 0); 
        OnHealthChange?.Invoke(_currentHealth); //push an event with current health (for UI)
    }
    public void AddHealth(int value)
    {
        //catch healing above max health.
        if(_currentHealth + value > _maxPlayerHealth) 
        { 
            _currentHealth = _maxPlayerHealth;  
            Debug.Log("Already at max health"); 
            return; 
        }
        _currentHealth += value;
        OnHealthChange?.Invoke(_currentHealth); //push an event with current health (for UI)
    }
    public bool IsAlive()
    {
        if (_currentHealth <= 0) return false;
        return true;
    }


}
