using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glonk : Creature
{
    public override float Health { get; set; } = 1f;
    public override float Atk { get; set; } = 0f;
    public override Dictionary<string, float> SelfTags { get; set; } = new Dictionary<string, float>();
    public override HashSet<string> AtkTags { get; set; } = new HashSet<string>
    {
        "weak"
    };
    public override HashSet<string> Aspects { get; set; } = new HashSet<string>
    {
        "living",
        "liquid",
        "slime",
        "weak",
        "small"
    };
    public override string description { get; set; } = "The Glonk is the physical form of the abstract concept of a mistake.";
    public override List<string> facts { get; set; } = new List<string>() {
        "does absolutely nothing",
        "cannot be intentionally summoned",
        "dies instantly when hit with anything, regardless of damage"
    };

    public override void Initialize()
    {
        //delegate declarations
        this.AtkSide = null;
        this.SelfAtkSide = null;
        this.SelfEnd = null;
        this.SelfDie = null;
        this.TakeDmg = null;
    }
    
    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        this.BasicDie();
    }
}
