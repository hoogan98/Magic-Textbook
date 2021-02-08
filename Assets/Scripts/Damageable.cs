using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    //public delegate void OnDamage(float dmg, HashSet<string> tags, SideEffect s);
    //public string Name;
    public GameObject targetingMarker;
    public GameObject targeterMarker;
    public GameObject TM;
    
    //book text
    public abstract string description { get; set; }
    public abstract List<string> facts { get; set; }
    
    protected GameObject T;

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
        
        GameObject newTarget = Instantiate(this.targetingMarker, targ.transform);
        GameObject tMarker = Instantiate(this.targeterMarker, this.transform);
        newTarget.SetActive(false);
        tMarker.SetActive(false);
        
        this.T = newTarget;
        this.target.TM = tMarker;
    }

    public void ShowTargets(bool showval)
    {
        if (this.T == null)
        {
            return;
        }
        this.T.SetActive(showval);
        this.target.TM.SetActive(showval);
    }

    protected void Detarget()
    {
        this.ShowTargets(false);
    }
}
