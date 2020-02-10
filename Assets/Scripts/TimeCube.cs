using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCube : Damageable
{
    public List<GameObject> lPlaces = new List<GameObject>();
    public List<GameObject> rPlaces = new List<GameObject>();
    public GameObject lWizPlace;
    public GameObject rWizPlace;
    public GameObject lWizPref;
    public GameObject rWizPref;
    public bool turn;

    private Creature leftWizard;
    private Creature rightWizard;

    public void Start()
    {
        turn = false;

        //start the game
        GameObject lWiz = Instantiate(this.lWizPref);
        GameObject rWiz = Instantiate(this.rWizPref);
        this.leftWizard = lWiz.GetComponent<Creature>();
        this.rightWizard = rWiz.GetComponent<Creature>();

        this.lWizPlace.GetComponent<SlotHandler>().AddBoi(lWiz);
        this.rWizPlace.GetComponent<SlotHandler>().AddBoi(rWiz);

        //PassTurn();
    }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        //so how do you kill time?
    }
    // for testing
    //public void OnMouseDown()
    //{
    //    this.PassTurn();
    //}
    //end for testing

    public void PassTurn()
    {
        if (turn)
        {
            EndTurnAll(lPlaces);
            this.rightWizard.EndTurn();
        } else
        {
            EndTurnAll(rPlaces);
            this.leftWizard.EndTurn();
        }

        turn = !turn;

        this.PassTurn();
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
