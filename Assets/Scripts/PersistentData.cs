using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class PersistentData 
{
    public int points;
    public DateTime date;
    //date=DateTime.Today;

    //CONSTRUCTOR--------------------
    public PersistentData(int pointsPlayer,DateTime dateGame) {
        points = pointsPlayer;
        date = dateGame;  
    }

    //String----------
    public string formatoString() {
        return "Points: "+ points.ToString() + " | "+date.ToString();
    }

   

   
}
