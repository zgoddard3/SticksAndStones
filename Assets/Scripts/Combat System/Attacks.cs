﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Attack", menuName = "Attack")]
public class Attacks : Action
{
    //TO DO: trigger error message for limited will, deal enemy damage

    //PlayerStats player = PlayerStats.instance;

    // SMDialogueTrigger error;
    static Dictionary<string, (int, int, int)> attacks = new Dictionary<string, (int, int, int)>//int or float?
    {
    };                                                                                 //attacks stored as name, (anxietyEffect, willEffect, enemyDamage) pairs

    private void Awake()
    {
        //enemy = GameObject.Find("NPC").GetComponent<NPC>();
        //error = GameObject.Find("Attack 1").GetComponent<SMDialogueTrigger>();
    }
    public override void Learn(string name, int anxiety, int will, int enemyDamage)
    {
        attacks.Add(name, (anxiety, will, enemyDamage));
    }

    public override (int, int, int) Use(string moveName)
    {
        SMPlayerStats.Instance.adjustAnxiety(attacks[moveName].Item1);
        if (SMPlayerStats.Instance.adjustWill(attacks[moveName].Item2) < 0)
        {
            string[] msg = new string[] { "You don't have enough Will!" };
            //error.TriggerDialogue(new Dialogue("", msg));
            //player.switchState(Transitions.Command.waitForPlayer);// should be in combat system
            return (0, 0, 0);
        }                   //rework playerStats so that you get unsuccessful moves if you don't have enough will
        //enemy.adjustHealth(attacks[moveName].Item3);
        return attacks[moveName];
    }

    public static int GetSize()
    {
        return attacks.Count;
    }

}
