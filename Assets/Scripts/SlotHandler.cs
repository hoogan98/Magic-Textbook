using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHandler : Damageable
{
    public Damageable myBoi;
    public bool isRight = false;

    //for testing
    //public GameObject otherSlot;

    //public void Start()
    //{
    //    myBoi.target = otherSlot.GetComponent<SlotHandler>().myBoi;
    //}
    //end for testing

    public void AddBoi(GameObject g)
    {
        g.transform.parent = this.transform;
        g.transform.localPosition = Vector3.zero;

        myBoi = g.GetComponent<Damageable>();
        if (myBoi is Creature c)
        {
            c.isFacingLeft = this.isRight;
        }
    }

    public void ChangeTarget(SlotHandler slot)
    {
        myBoi.target = slot.myBoi;
    }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        //figure out how to damage these things
    }
}
