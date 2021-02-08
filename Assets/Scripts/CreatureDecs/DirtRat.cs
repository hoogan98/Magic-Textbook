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
    
    public override float Health { get; set; } = 30f;
    public override float Atk { get; set; } = 7f;
    public override Dictionary<string, float> SelfTags { get; set; } = new Dictionary<string, float>
    {
        { "clean", 3 },
        { "dirty", 0 }  //completely immune
    };
    public override HashSet<string> AtkTags { get; set; } = new HashSet<string>
    {
        "bite",
        "dirty"
    };
    public override HashSet<string> Aspects { get; set; } = new HashSet<string>
    {
        "telepathic",
        "elemental",
        "living",
        "dirty",
        "small",
        "angry",
        "animal"
    };
    public override string description { get; set; } = "the dirt rat, a psychic pile of sentient dirt shaped like a rat.Communicates telepathically, but can only say 'more dirt'";
    public override List<string> facts { get; set; } = new List<string> {
        "gets a stats up and size up from getting hit with dirty attacks.",
        "weak to clean attacks",
        "has telepathic attacks"
    };

    private float origXScale;
    
    public override void Initialize()
    {
        this.origXScale = this.transform.localScale.x;

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
            this.TakeDmg(dmg, tags, s);
        }
    }
}
