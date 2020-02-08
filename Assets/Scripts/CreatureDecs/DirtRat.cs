using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Creature;

public class DirtRat : Creature
{
    //the dirt rat, a psychic pile of sentient dirt shaped like a rat.  Communicates telepathically, but can only say "more dirt"
    // => gets a stats up and size up from getting hit with "dirty" attacks.  
    // => weak to clean attacks
    // => has telepathic attacks

    private float origXScale;

    void Start()
    {
        //basic vals
        this.Health = 30f;
        this.Atk = 7f;

        this.origXScale = this.transform.localScale.x;

        if (isFacingLeft)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

        //tag declarations
        this.SelfTags = new Dictionary<string, float>
        {
            { "clean", 3 },
            { "dirty", 0 }  //completely immune
        };


        this.AtkTags = new HashSet<string>
        {
            "bite",
            "dirty"
        };

        this.Aspects = new HashSet<string>
        {
            "telepathic",
            "elemental",
            "living",
            "dirty",
            "small",
            "angry",
            "animal"
        };

        //delegate declarations
        this.DmgHandler = new OnDamage(OnDmgDel);
        this.AtkSide = new SideEffect(AttackSideEffect);
        this.SelfAtkSide = null;
        this.SelfEnd = null;
        this.SelfDie = null;
        this.TakeDmg = new HandleAddDamage(this.BasicHit);
        this.TakeDmg += new HandleAddDamage(this.BasicCheckDeath);
    }


    void AttackSideEffect(Damageable t)
    {
        if (t is Creature c)
        {
            c.Health -= 1f;
        }
    }

    void OnDmgDel(float dmg, HashSet<string> tags, SideEffect s)
    {
        if (tags.Contains("dirty"))
        {
            this.Health += dmg / 2;
            this.Atk += (dmg / 10);
            Vector3 scl = this.transform.localScale;
            scl.x *= 1.05f;
            scl.y *= 1.05f;
            scl.z *= 1.05f;
            this.transform.localScale = scl;

            if (scl.x >= (this.origXScale * 2)) {
                this.Aspects.Remove("small");
                this.Aspects.Add("large");
                this.Aspects.Add("heavy");
            }
        }
    }

    public override void EndTurn()
    {
        Debug.Log("dirt rat: " + this.Health);
        if (this.SelfEnd != null)
        {
            this.SelfEnd();
        }

        if (this.Aspects.Contains("living"))
        {
            this.Attack();
        }
    }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        if (this.TakeDmg != null)
        {
            this.TakeDmg(dmg, tags, s);
        }
    }
}
