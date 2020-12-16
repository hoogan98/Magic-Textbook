using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtRat : Creature
{
    //the dirt rat, a psychic pile of sentient dirt shaped like a rat.  Communicates telepathically, but can only say "more dirt"
    // => gets a stats up and size up from getting hit with "dirty" attacks.  
    // => weak to clean attacks
    // => has telepathic attacks

    private float origXScale;
    private List<string> facs = new List<string> {
        "gets a stats up and size up from getting hit with dirty attacks.",
        "weak to clean attacks",
        "has telepathic attacks"
    };

    public override string description
    {
        get
        {
            return "the dirt rat, a psychic pile of sentient dirt shaped like a rat.Communicates telepathically, but can only say 'more dirt'";
        }
        set { }
    }

    public override List<string> facts
    {
        get { return this.facs; }
        set { }
    }



    public override void Initialize()
    {
        //basic vals
        this.Health = 30f;
        this.Atk = 7f;

        this.origXScale = this.transform.localScale.x;


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
        this.AtkSide = null;
        this.SelfAtkSide = null;
        this.SelfEnd = this.BasicEndTurn;
        this.SelfDie = null;
        this.TakeDmg = this.BasicTakeDmg;
        this.TakeDmg += OnDmgDel;
    }

    void OnDmgDel(float dmg, HashSet<string> tags, SideEffect s)
    {
        Debug.Log("delegate called on dirt rat");
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

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        if (this.TakeDmg != null)
        {
            Debug.Log("damage taken on dirt rat");
            this.TakeDmg(dmg, tags, s);
        }
    }
}
