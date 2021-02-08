using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockRat : Creature
{
    //An elemental rat made out of rocks.
    // => strong against blades and water
    // => weak against other rocks and lava
    
    public override float Health { get; set; } = 45f;
    public override float Atk { get; set; } = 9f;
    public override Dictionary<string, float> SelfTags { get; set; } = new Dictionary<string, float>
    {
        { "slice", 0f },
        { "wet", 0.1f },
        { "rock", 1.5f },
        { "lava", 2f }
    };
    public override HashSet<string> AtkTags { get; set; } = new HashSet<string>
    {
        "rock",
        "bluntForce"
    };
    public override HashSet<string> Aspects { get; set; } = new HashSet<string>
    {
        "heavy",
        "small",
        "rock",
        "animal",
        "living",
        "elemental"
    };
    public override string description { get; set; } = "An elemental rat made out of rocks.";
    public override List<string> facts { get; set; } = new List<string>
    {
        "strong against blades and water",
        "weak against other rocks and lava",
        "gives concussions to hit creatures, resulting in lower attack if they attack using telepathy or psychic magic"
    };

    public override void Initialize()
    {
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
