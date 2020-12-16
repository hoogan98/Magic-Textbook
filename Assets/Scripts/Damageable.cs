using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    //public delegate void OnDamage(float dmg, HashSet<string> tags, SideEffect s);
    //public string Name;
    public GameObject TargetingMarker;
    
    //book text
    public abstract string description { get; set; }
    public abstract List<string> facts { get; set; }

    protected GameObject TM;

    public delegate void OnTurnEnd();

    public delegate void SideEffect(Damageable target);

    public delegate void OnAttack();

    public delegate void OnDeath();

    protected Damageable target;

    public delegate void HandleAddDamage(float dmg, HashSet<string> tags, SideEffect s);

    public abstract void Damage(float dmg, HashSet<string> tags, SideEffect s);

    public abstract void Initialize();

    public void SetTarget(GameObject targ)
    {
        this.target = targ.GetComponent<Damageable>();
        
        // GameObject newTarget = Instantiate(this.TargetingMarker, targ.transform);
        // newTarget.SetActive(false);
        //
        // this.TM = newTarget;
    }
}
