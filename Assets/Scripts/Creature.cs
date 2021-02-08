using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Damageable;

public abstract class Creature : Damageable
{

    public abstract float Atk { get; set; }
    public abstract Dictionary<string, float> SelfTags { get; set; }
    public abstract HashSet<string> AtkTags { get; set; }
    public abstract float Health { get; set; }
    public abstract HashSet<string> Aspects { get; set; }

    private SideEffect atkSide;
    private OnAttack selfAtkSide;
    private OnTurnEnd selfEnd;
    private OnDeath selfDie;
    private HandleAddDamage takeDmg;
    public SideEffect AtkSide { get => atkSide; set => atkSide = value; }
    public OnAttack SelfAtkSide { get => selfAtkSide; set => selfAtkSide = value; }
    public OnTurnEnd SelfEnd { get => selfEnd; set => selfEnd = value; }
    public OnDeath SelfDie { get => selfDie; set => selfDie = value; }
    public HandleAddDamage TakeDmg { get => takeDmg; set => takeDmg = value; }

    public virtual void Attack()
    {
        //delegates run first, then attack. Creature could die during delegates.
        if (this.SelfAtkSide != null)
        {
            this.SelfAtkSide();
        }

        if (target != null)
        {
            this.target.Damage(this.Atk, this.AtkTags, this.AtkSide);
        }
    }

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
                dmg *= this.SelfTags[tag];
            }
        }
        //dmg is taken first, then delegates
        this.Health -= dmg;
        
        //probably make it so that the creature dies if its health gets to zero here
        
        if (s != null)
        {
            s(this);
        }
        
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
            this.BasicDie();
        }
    }

    protected void BasicDie()
    {
        this.Aspects.Remove("living");
        this.Aspects.Add("dead");
        //rotate 90 degrees
        this.transform.Translate(new Vector3(0, this.GetComponent<Renderer>().bounds.size.y, 0));
        this.transform.Rotate(new Vector3(0, 0, 1), 180f);
        //stop animation
        if (this.GetComponent<SpriteAnim>() != null)
        {
            this.GetComponent<SpriteAnim>().StopAnimation();
        }
    }

    protected void BasicEndTurn()
    {
        //make sure to add a soul inhabited check later on to make sure players can't attack twice
        if (this.Aspects.Contains("living"))
        {
            this.Attack();
        }
    }

    protected void BasicTakeDmg(float dmg, HashSet<string> tags, SideEffect s)
    {
        this.BasicHit(dmg, tags, s);
        this.BasicCheckDeath(dmg, tags, s);
    }
}
