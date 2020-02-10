using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWizard : Creature
{
    private KeyCode chosenTurn;

    private void Start()
    {

        if (isFacingLeft)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    public override void Damage(float dmg, HashSet<string> tags, SideEffect s)
    {
        throw new System.NotImplementedException();
    }

    public override void EndTurn()
    {
        StartCoroutine(TakeTurn());
    }

    private IEnumerator TakeTurn()
    {
        StartCoroutine(WaitForTurnInput());

        if (this.chosenTurn == KeyCode.UpArrow)
        {
            yield return null;
        }
    }

    private IEnumerator WaitForTurnInput()
    {
        Debug.Log("waiting for turn");
        bool done = false;
        while (!done)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.chosenTurn = KeyCode.UpArrow;
                done = true; // breaks the loop
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
    }
}
