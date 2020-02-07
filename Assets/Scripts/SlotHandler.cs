using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : Damageable
{
    public Damageable myBoi;

    //for testing
    public GameObject otherSlot;

    public void Start()
    {
        myBoi.target = otherSlot.GetComponent<SlotHandler>().myBoi;
    }
    //end for testing

    public override void HandleAddDamage(float dmg, HashSet<string> tags, SideEffect s)
    {
        //figure out how these things should be hurt
    }

    public void ChangeTarget(SlotHandler slot)
    {
        myBoi.target = slot.myBoi;
    }
}
