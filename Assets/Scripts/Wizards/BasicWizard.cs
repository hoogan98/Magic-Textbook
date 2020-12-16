using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWizard : Creature
{
    //public KeyCode atkKey;
    //public KeyCode summonKey;
    //public KeyCode targetKey;
    //public bool choosingTurn;

    //private KeyCode turnKey;
    //private OnTurnEnd lastTurn;
    //private bool targeting;

    public override void Initialize()
    {

        //test control scheme
        //this.atkKey = KeyCode.W;
        //this.summonKey = KeyCode.A;
        //this.targetKey = KeyCode.D;

        //this.choosingTurn = false;

       // this.lastTurn = null;

        //creature related values
        //book text
        this.description = "A wizard, controlled by a player's soul (hopefully)";
        this.facts = new List<string>
        {
            "Immune to many magical effects"
        };

        //basic vals
        this.Health = 1000f;
        this.Atk = 10f;

        //tag declarations
        this.SelfTags = new Dictionary<string, float>();

        this.AtkTags = new HashSet<string>
        {
            "bluntForce"
        };

        this.Aspects = new HashSet<string>
        {
            "wizard",
            "living",
            "human"
        };

        //delegate declarations
        this.AtkSide = null;
        this.SelfAtkSide = null;
        this.SelfEnd = null;
        this.SelfDie = null;
        this.TakeDmg = new HandleAddDamage(this.BasicTakeDmg);

    }

    public override string description { get; set; }
    public override List<string> facts { get; set; }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        if (this.TakeDmg != null)
        {
            this.TakeDmg(dmg, tags, s);
        }
    }

    //private void Update()
    //{
    //    //if (this.targeting)
    //    //{
    //    //    if (Input.GetMouseButtonDown(0))
    //    //    {
    //    //        Debug.Log("click");
    //    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    //        RaycastHit hit;

    //    //        if (Physics.Raycast(ray, out hit, 100))
    //    //        {
    //    //            Debug.Log(hit.transform.gameObject.name);
    //    //            this.targeting = false;
    //    //        }
    //    //    }
    //    //}
    //    //else if (!this.choosingTurn)
    //    if (!this.choosingTurn)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        string inp = Input.inputString;
    //        if (inp.Length != 0)
    //        {
    //            Debug.Log(this.name + ": " + inp);
    //            this.choosingTurn = false;
    //            this.turnKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), inp.ToUpper());

    //            if (this.turnKey == this.atkKey)
    //            {
    //                Debug.Log("attacking: " + this.Health);
    //                this.lastTurn = new OnTurnEnd(this.BookBash);
    //                this.SelfEnd += this.lastTurn;
    //            }
    //            else if (this.turnKey == this.summonKey)
    //            {
    //                Debug.Log("summoning");
    //            }
    //            else if (this.turnKey == this.targetKey)
    //            {
    //                Debug.Log("targeting");
    //                //this.targeting = true;
    //            }
    //            else
    //            {
    //                this.choosingTurn = true;
    //            }
    //        }
    //    }
    //}

    //public void SetupTurn()
    //{
    //    if (this.lastTurn != null)
    //    {
    //        this.SelfEnd -= this.lastTurn;
    //    }
        
    //    this.choosingTurn = true;
    //}
}
