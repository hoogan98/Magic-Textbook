using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCube : Damageable
{
    //public GameObject lPlaces;
    //public GameObject rPlaces;

    public GameObject soul;
    public GameObject p1B;
    public GameObject p2B;
    public GameObject book;

    public UIMGR uimgr;
    //public GameObject lWizPlace;
    //public GameObject rWizPlace;
    //public GameObject lWizPref;
    //public GameObject rWizPref;
    //public bool turn;
    //public bool canTurn;

    //private BasicWizard leftWizard;
    //private BasicWizard rightWizard;

    private Creature p1Body;
    private Creature p2Body;
    private Soul p1Soul;
    private Soul p2Soul;

    // private Soul nextUp;
    // private Soul lastUp;

    public override void Initialize()
    {
        //maybe move start to here idk
    }

    public void Start()
    {
        //turn = false;
        //this.canTurn = true;

        ////instantiate wizards
        //GameObject lWiz = Instantiate(this.lWizPref);
        //lWiz.name = "leftBoi";
        //GameObject rWiz = Instantiate(this.rWizPref);
        //rWiz.name = "rightBoi";
        //this.leftWizard = lWiz.GetComponent<BasicWizard>();
        //this.rightWizard = rWiz.GetComponent<BasicWizard>();
        //this.leftWizard.tCube = this;
        //this.rightWizard.tCube = this;

        //this.lWizPlace.GetComponent<SlotHandler>().AddBoi(lWiz);
        //this.rWizPlace.GetComponent<SlotHandler>().AddBoi(rWiz);

        ////set initial wizard targets to each other
        //this.rWizPlace.GetComponent<SlotHandler>().ChangeTarget(this.lWizPlace.GetComponent<SlotHandler>());
        //this.lWizPlace.GetComponent<SlotHandler>().ChangeTarget(this.rWizPlace.GetComponent<SlotHandler>());

        //this.leftWizard.SetupTurn();
        
        this.GetComponent<CreatureDB>().SetupDB();

        this.p1Body = this.p1B.GetComponent<Creature>();
        this.p2Body = this.p2B.GetComponent<Creature>();
        this.p1Body.Initialize();
        this.p2Body.Initialize();

        this.p1Soul = Instantiate(this.soul).GetComponent<Soul>();
        this.p2Soul = Instantiate(this.soul).GetComponent<Soul>();

        this.p1Soul.InhabitBody(this.p1Body);
        this.p2Soul.InhabitBody(this.p2Body);
        
        this.p1Soul.testName = "p1";
        this.p2Soul.testName = "p2";

        this.p1Soul.tCube = this;
        this.p2Soul.tCube = this;

        //initiate turns
        // this.nextUp = p1Soul;
        // this.lastUp = p2Soul;
        // this.nextUp.hasTurn = true;
        // this.lastUp.hasTurn = false;
        this.p1Soul.hasTurn = true;
        this.p2Soul.hasTurn = false;

        //for now, generate a new book for each player
        GameObject p1Book = Instantiate(book);
        GameObject p2Book = Instantiate(book);
        this.p1Soul.GiveBook(p1Book);
        this.p2Soul.GiveBook(p2Book);

        this.uimgr.Setup();

        //this.PassTurn();
    }

    public override string description { get; set; }
    public override List<string> facts { get; set; }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        //so how do you kill time?
    }

    public void OnMouseDown()
    {
        //if (this.canTurn)
        //{
        //    this.PassTurn();
        //}
    }

    public void UpdateTurnUI(Soul.TurnState state)
    {
        if (this.p1Soul.hasTurn)
        {
            uimgr.UpdateTurnState('l', state);
        }
        else
        {
            uimgr.UpdateTurnState('r', state);
        }
    }

    public void ShowBook(Book b, int pageIndex)
    {
        //display the first page for now
        this.uimgr.DisplayBook(b, pageIndex);
    }

    public void HideBook()
    {
        this.uimgr.CloseCurrentBook();
    }

    public void PassTurn()
    {
        Debug.Log("pass");
        //if (this.leftWizard.choosingTurn || this.rightWizard.choosingTurn)
        //{
        //    return;
        //}

        //if (turn)
        //{
        //    this.leftWizard.SetupTurn();
        //    this.rightWizard.EndTurn();
        //    EndTurnAll(rPlaces);

        //} else
        //{
        //    this.rightWizard.SetupTurn();
        //    this.leftWizard.EndTurn();
        //    EndTurnAll(lPlaces);
        //}

        //turn = !turn;
        
        if (this.p1Soul.hasTurn)
        {
            this.EndTurnAll(this.p1Soul.slots);
        }
        else
        {
            this.EndTurnAll(this.p2Soul.slots);
        }
        
        this.p1Soul.hasTurn = !this.p1Soul.hasTurn;
        this.p2Soul.hasTurn = !this.p2Soul.hasTurn;
    }

    private void EndTurnAll(GameObject places)
    {
        foreach (SlotHandler s in places.GetComponentsInChildren<SlotHandler>())
        {
            if (s.myBoi != null)
            {
                if (s.myBoi is Creature c)
                {
                    c.SelfEnd?.Invoke();
                }
                //Debug.Log(c.name + " ends turn");
                
            }
        }
    }
}
