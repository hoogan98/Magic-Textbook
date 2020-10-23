using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glonk : Creature
{
    public override void Initialize()
    {
        //book text
        this.description = "The Glonk is the physical form of the abstract concept of a mistake.";
        this.facts = new List<string>() {
            "does absolutely nothing",
            "cannot be intentionally summoned",
            "dies instantly when hit with anything, regardless of damage"
        };
        
        //basic vals
        this.Health = 1f;
        this.Atk = 0f;
        
        //tag declarations
        this.SelfTags = new Dictionary<string, float>();
        
        this.AtkTags = new HashSet<string>
        {
            "weak"
        };
        
        this.Aspects = new HashSet<string>
        {
            "living",
            "liquid",
            "slime",
            "weak",
            "small"
        };
        
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
