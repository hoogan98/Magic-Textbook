using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Damageable;

public abstract class Creature : Damageable
{
    //book text
    public string description;
    public List<string> facts;

    private float health;
    private float atk;
    private HashSet<string> aspects;
    private Dictionary<string, float> selfTags;
    private HashSet<string> atkTags;
    private bool isRight;
    public bool isFacingLeft;

    //private bool hasInfiniteTurn;
    //private float turnTime;           //for wizards

    private SideEffect atkSide;
    private OnAttack selfAtkSide;
    private OnTurnEnd selfEnd;
    private OnDeath selfDie;
    private HandleAddDamage takeDmg;

    public float Atk { get => atk; set => atk = value; }
    public Dictionary<string, float> SelfTags { get => selfTags; set => selfTags = value; }
    public HashSet<string> AtkTags { get => atkTags; set => atkTags = value; }
    public SideEffect AtkSide { get => atkSide; set => atkSide = value; }
    public OnAttack SelfAtkSide { get => selfAtkSide; set => selfAtkSide = value; }
    public OnTurnEnd SelfEnd { get => selfEnd; set => selfEnd = value; }
    public float Health { get => health; set => health = value; }
    public HashSet<string> Aspects { get => aspects; set => aspects = value; }
    public bool IsRight { get => isRight; set => isRight = value; }
    public OnDeath SelfDie { get => selfDie; set => selfDie = value; }
    public HandleAddDamage TakeDmg { get => takeDmg; set => takeDmg = value; }

    public virtual void Attack()
    {
        //delegates run first, then attack. Creature could die during delegates.
        if (this.selfAtkSide != null)
        {
            this.selfAtkSide();
        }

        this.target.Damage(this.atk, this.atkTags, this.atkSide);

    }

    public abstract void EndTurn();

    //basic functions that act as default settings for the delegates
    protected void BasicHit(float dmg, HashSet<string> tags, SideEffect s)
    {
        //save original damage value to pass to delegate
        //float origDmg = dmg;

        //check strength/weakness
        foreach (string tag in tags)
        {
            if (this.SelfTags.ContainsKey(tag))
            {
                dmg *= this.selfTags[tag];
            }
        }
        //dmg is taken first, then delegates
        this.Health -= dmg;

        s(this);
        //if (this.dmgHandler != null)
        //{
        //    //uses the original damage value that came in
        //    dmgHandler(origDmg, tags, s);
        //}
    }

    //works as handleAddDmg delegate, hence the weird args
    protected void BasicCheckDeath(float dmg, HashSet<string> tags, SideEffect s)
    {
        if (this.Health <= 0 && this.Aspects.Contains("living"))
        {
            this.Aspects.Remove("living");
            this.Aspects.Add("dead");
            //rotate 90 degrees
            this.transform.Translate(new Vector3(0, this.GetComponent<Renderer>().bounds.size.y, 0));
            this.transform.Rotate(new Vector3(0, 0, 1), 180f);
            
        }
    }
}
