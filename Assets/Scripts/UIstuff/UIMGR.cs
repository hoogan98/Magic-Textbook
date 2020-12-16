using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMGR : MonoBehaviour
{
    public GameObject leftPlayerState;
    public GameObject rightPlayerState;

    private Text leftText;
    private Text rightText;
    
    public void Setup()
    {
        leftText = leftPlayerState.GetComponent<Text>();
        rightText = rightPlayerState.GetComponent<Text>();
    }

    public void UpdateTurnState(char screenSide, Soul.TurnState state)
    {
        if (screenSide == 'L' || screenSide == 'l')
        {
            leftText.text = state.ToString();
        }
        else
        {
            rightText.text = state.ToString();
        }
    }

    public void DisplayBook(Book b, int pageIndex)
    {
        Page current = b.PrintPage(pageIndex);
        
        Debug.Log("name: " + current.realName);
        Debug.Log("spell: " + current.spellName);
        Debug.Log("desc: " + current.description);
        foreach (string fact in current.facts)
        {
            Debug.Log("fact: " + fact);
        }

    }

    public void CloseCurrentBook()
    {
        
    }
    
}
