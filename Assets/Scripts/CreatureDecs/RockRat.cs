using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRat : Creature
{
    //An elemental rat made out of rocks.
    // => strong against blades and water
    // => weak against other rocks and lava

    
    private List<string> facs = new List<string>
    {
        "strong against blades and water",
        "weak against other rocks and lava",
        "gives concussions to hit creatures, resulting in lower attack if they attack using telepathy or psychic magic"
    };

    public override string description
    {
        get
        {
            return "An elemental rat made out of rocks.";
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
        this.Health = 45f;
        this.Atk = 9f;

        //tag declarations
        this.SelfTags = new Dictionary<string, float>
        {
            { "slice", 0f },
            { "wet", 0.1f },
            { "rock", 1.5f },
            { "lava", 2f }
        };

        this.AtkTags = new HashSet<string>
        {
            "rock",
            "bluntForce"
        };

        this.Aspects = new HashSet<string>
        {
            "heavy",
            "small",
            "rock",
            "animal",
            "living",
            "elemental"
        };

        //delegate declarations
        this.AtkSide = new SideEffect(giveConcussion);
        this.SelfAtkSide = null;
        this.SelfEnd = this.BasicEndTurn;
        this.SelfDie = null;
        this.TakeDmg = new HandleAddDamage(this.BasicTakeDmg);
    }

    void giveConcussion(Damageable d)
    {
        if (d is Creature c)
        {
            if (c.Aspects.Contains("telepathic") || c.Aspects.Contains("psychic"))
            {
                c.Atk /= 2;
            }
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
