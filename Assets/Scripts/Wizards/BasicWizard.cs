using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWizard : Creature
{
    public override float Health { get; set; } = 1000f;
    public override float Atk { get; set; } = 10f;
    public override Dictionary<string, float> SelfTags { get; set; } = new Dictionary<string, float>();
    public override HashSet<string> AtkTags { get; set; } = new HashSet<string>
    {
        "bluntForce"
    };
    public override HashSet<string> Aspects { get; set; } = new HashSet<string>
    {
        "wizard",
        "living",
        "human"
    };
    public override string description { get; set; } = "A wizard, controlled by a player's soul (hopefully)";
    public override List<string> facts { get; set; } = new List<string> {
        "Immune to many magical effects"
    };

    public override void Initialize()
    {
        //delegate declarations
        this.AtkSide = null;
        this.SelfAtkSide = null;
        this.SelfEnd = null;
        this.SelfDie = null;
        this.TakeDmg = new HandleAddDamage(this.BasicTakeDmg);
    }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        if (this.TakeDmg != null)
        {
            this.TakeDmg(dmg, tags, s);
        }
    }
}
