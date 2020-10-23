using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Soul : MonoBehaviour
{
    //the great thing about summoning is that nothing you bring forth has a soul!
    // ...unless you decide to inhabit it
    public enum TurnState {Waiting, Reading, Targeting, Summoning, Placing, Attacking};
    
    public GameObject slots;
    public Creature currentBody;
    public TimeCube tCube;
    public bool hasTurn = false;
    public string testName;
    public Book boundBook;

    //private EndingAction endingAction;
    private TurnState state;
    private string summonText;
    private GameObject currentSpell;
    private GameObject targeter;
    private bool end;

    private void Start()
    {
        this.slots = this.transform.parent.parent.parent.gameObject;
        this.state = TurnState.Waiting;
        this.end = false;
    }

    public void GiveBook(GameObject book)
    {
        this.boundBook = book.GetComponent<Book>();
        this.boundBook.SetCube(this.tCube.gameObject);
        this.boundBook.GenerateBook();
    }

    public void InhabitBody(Creature body)
    {
        this.currentBody = body;
        this.transform.parent = this.currentBody.gameObject.transform;
        this.transform.localPosition = Vector3.zero;
    }

    //private delegate void EndingAction();

    private void Update()
    {
        if (this.hasTurn)
        {
            if (this.end)
            {
                this.end = false;
                this.EndTurn();
            }
            switch (this.state)
            {
                case TurnState.Waiting:
                    this.GetInput();
                    break;
                
                case TurnState.Reading:
                    this.ReadBook();
                    break;
                
                case TurnState.Summoning:
                    this.Summon();
                    break;
                
                case TurnState.Placing:
                    this.PlaceCreature();
                    break;
                
                case TurnState.Targeting:
                    this.SelectTargets();
                    break;
                
                case TurnState.Attacking:
                    this.currentBody.Attack();
                    this.end = true;
                    break;
            }

        }
    }

    private void GetInput()
    {
        
        string inp = Input.inputString;
        if (inp.Length != 0)
        {
            // Debug.Log(this.testName + " " + inp);
            foreach (KeyCode k in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(k))
                {
                    Debug.Log(k.ToString());
                    switch (k)
                    {
                        case KeyCode.W:
                            Debug.Log(this.testName + " attacks");
                            this.state = TurnState.Attacking;
                            break;
                
                        case KeyCode.S:
                            Debug.Log(this.testName + " reads");
                            this.state = TurnState.Reading;
                            break;
                
                        case KeyCode.A:
                            Debug.Log(this.testName + " summons");
                            this.state = TurnState.Summoning;
                            this.summonText = "";
                            break;
                
                        case KeyCode.D:
                            Debug.Log(this.testName + " changes targets");
                            this.state = TurnState.Targeting;
                            this.targeter = null;
                            break;
                
                        case KeyCode.Return:
                            Debug.Log(this.testName + " skips turn");
                            this.end = true;
                            break;
                    }
                }
            }
        }
    }

    private void ReadBook()
    {
        //open the book
    }

    private void Summon()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(this.testName + " attempting to summon: " + this.summonText);
            this.currentSpell = this.boundBook.AttemptSummon(this.summonText);
            this.summonText = "";
            this.state = TurnState.Placing;
            return;
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.state = TurnState.Waiting;
            return;
        }
        
        string inp = Input.inputString;
        if (inp.Length != 0)
        {
            this.summonText += inp;
            Debug.Log(this.testName + " summoning: " + this.summonText);
        }
    }

    private void SelectTargets()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);

            if (hit.collider != null)
            {
                if (this.targeter == null)
                {
                    Debug.Log("selected: " + hit.collider.gameObject.name);
                    this.targeter = hit.collider.gameObject;
                }
                else
                {
                    Debug.Log(this.targeter.name + " now targets: " + hit.collider.gameObject.name);
                    this.targeter.GetComponent<Damageable>().SetTarget(hit.collider.gameObject);
                    this.targeter = null;
                }
            }
        } else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(this.testName + "is finished targeting");
            this.end = true;
        }
    }
    
    private void EndTurn()
    {
        Debug.Log(this.testName + " ends turn");
        // if (this.endingAction != null)
        // {
        //     this.endingAction();
        // }
        this.state = TurnState.Waiting; 
        this.tCube.PassTurn();
        // Debug.Log("got through the passturn");
    }

    // private void AttackChoice()
    // {
    //     this.currentBody.Attack();
    // }

    private void PlaceCreature()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.forward);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Slot") && hit.transform.parent.gameObject == this.slots)
                {
                    GameObject newBoi = Instantiate(this.currentSpell, hit.transform);
                    Damageable newD = newBoi.GetComponent<Damageable>();
                    hit.collider.gameObject.GetComponent<SlotHandler>().myBoi = newD;
                    Debug.Log(hit.collider.gameObject.GetComponent<SlotHandler>().myBoi.name);
                    newD.Initialize();
                    //this.state = TurnState.Waiting;
                    this.end = true;
                }
            }
        }
    }

    public TurnState GetState()
    {
        return this.state;
    }

    //to check for destruction, check the creature for the dead tag or the absence of "alive" or "undead" tags
}
