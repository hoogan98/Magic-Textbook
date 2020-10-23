using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMGR : MonoBehaviour
{
    public PState p1State;
    public PState p2State;
    
    public void Setup(Soul p1Soul, Soul p2Soul)
    {
        p1State.pSoul = p1Soul;
        p2State.pSoul = p2Soul;
    }
}
