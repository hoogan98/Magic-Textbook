using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : Damageable
{
    public Damageable myBoi;

    //for testing
    //public GameObject otherSlot;

    //public void Start()
    //{
    //    myBoi.target = otherSlot.GetComponent<SlotHandler>().myBoi;
    //}
    //end for testing

    // public void AddBoi(GameObject g)
    // {
    //     g.transform.parent = this.transform;
    //     g.transform.localPosition = Vector3.zero;
    // }

    // public void ChangeTarget(GameObject other)
    // {
    //     myBoi.SetTarget(other);
    // }

    public override string description { get; set; }
    public override List<string> facts { get; set; }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        //myBoi.Damage(dmg, tags, s);
        
        //add a way to give these side effects
    }

    public override void Initialize()
    {
        //not sure what to do here
    }

    public void AddBoi(Damageable boi)
    {
        this.myBoi = boi;
        boi.targeterMarker = targeterMarker;
        boi.targetingMarker = targetingMarker;
    }
}
