using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PState : MonoBehaviour
{
    public Soul pSoul;

    private void Update()
    {
        if (this.pSoul != null)
        {
            if (this.pSoul.hasTurn)
            {
                this.gameObject.GetComponent<Text>().text = this.pSoul.GetState().ToString();
            }
            
        }
    }
}
