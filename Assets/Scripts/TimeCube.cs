using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCube : Damageable
{
    public List<GameObject> lPlaces = new List<GameObject>();
    public List<GameObject> rPlaces = new List<GameObject>();

    public bool turn;

    public void Start()
    {
        turn = false;
    }

    public override void HandleAddDamage(float dmg, HashSet<string> tags, SideEffect s)
    {
        //so how do you kill time?
    }
    // for testing
    public void OnMouseDown()
    {
        this.PassTurn();
    }
    //end for testing

    public void PassTurn()
    {
        if (turn)
        {
            EndTurnAll(lPlaces);
        } else
        {
            EndTurnAll(rPlaces);
        }

        turn = !turn;
    }

    private void EndTurnAll(List<GameObject> places)
    {
        foreach (GameObject g in places)
        {
            SlotHandler s = g.GetComponent<SlotHandler>();
            if (s.myBoi is Creature c)
            {
                c.EndTurn();
            }
        }
    }
}
