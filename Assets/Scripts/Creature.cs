using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Damageable;

public abstract class Creature : Damageable
{
    private float health;
    private float atk;
    private HashSet<string> aspects;
    private Dictionary<string, float> selfTags;
    private HashSet<string> atkTags;
    private bool isRight;
    public bool isFacingLeft;

    //private bool hasInfiniteTurn;
    //private float turnTime;           //for wizards

    private OnDamage dmgHandler;
    private SideEffect atkSide;
    private OnAttack selfAtkSide;
    private OnTurnEnd selfEnd;
    private OnDeath selfDie;

    public float Atk { get => atk; set => atk = value; }
    public Dictionary<string, float> SelfTags { get => selfTags; set => selfTags = value; }
    public HashSet<string> AtkTags { get => atkTags; set => atkTags = value; }
    public OnDamage DmgHandler { get => dmgHandler; set => dmgHandler = value; }
    public SideEffect AtkSide { get => atkSide; set => atkSide = value; }
    public OnAttack SelfAtkSide { get => selfAtkSide; set => selfAtkSide = value; }
    public OnTurnEnd SelfEnd { get => selfEnd; set => selfEnd = value; }
    public float Health { get => health; set => health = value; }
    public HashSet<string> Aspects { get => aspects; set => aspects = value; }
    public bool IsRight { get => isRight; set => isRight = value; }
    public OnDeath SelfDie { get => selfDie; set => selfDie = value; }

    override public void HandleAddDamage(float dmg, HashSet<string> tags, SideEffect s)
    {
        //save original damage value to pass to delegate
        float origDmg = dmg;

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
        if (this.dmgHandler != null)
        {
            dmgHandler(origDmg, tags, s);

        }
    }

    public void Attack()
    {
        //delegates run first, then attack. Creature could die during delegates.
        if (this.selfAtkSide != null)
        {
            this.selfAtkSide();
        }

        this.target.HandleAddDamage(this.atk, this.atkTags, this.atkSide);

    }

    public abstract void EndTurn();
}
