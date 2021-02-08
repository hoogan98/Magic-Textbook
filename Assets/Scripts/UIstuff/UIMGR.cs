using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMGR : MonoBehaviour
{
    public GameObject leftPlayerState;
    public GameObject rightPlayerState;
    public GameObject UICanvas;
    public GameObject bookPageUI;

    private Text leftText;
    private Text rightText;
    private GameObject openBook;
    
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
        if (this.openBook == null)
        {
            this.openBook = Instantiate(this.bookPageUI, UICanvas.transform);
        }
        
        Page current = b.PrintPage(pageIndex);
        
        // Debug.Log("name: " + current.realName);
        // Debug.Log("spell: " + current.spellName);
        // Debug.Log("desc: " + current.description);
        // foreach (string fact in current.facts)
        // {
        //     Debug.Log("fact: " + fact);
        // }

        openBook.transform.Find("NameText").GetComponent<Text>().text = current.realName;
        openBook.transform.Find("SpellText").GetComponent<Text>().text = current.spellName;
        openBook.transform.Find("DescriptionText").GetComponent<Text>().text = current.description;
        Text factBox = openBook.transform.Find("FactsText").GetComponent<Text>();
        Text statBox = openBook.transform.Find("StatsText").GetComponent<Text>();
        factBox.text = "";
        statBox.text = "";

        if (current.subject == null)
        {
            return;
        }
        
        foreach (string fact in current.facts)
        {
            factBox.text += fact + "\r\n";
        }

        if (current.subject is Creature c)
        {
            statBox.text = "Health: " + c.Health + "\r\n" +
                "Attack: " + c.Atk + "\r\n" + 
                "Aspects:";
            foreach (string aspect in c.Aspects)
            {
                statBox.text += ", " + aspect;
            }
        
            statBox.text += "\r\n" + "Damage Mods:";
            foreach (string mod in c.SelfTags.Keys)
            {
                statBox.text += ", " + mod + " : x" + c.SelfTags[mod];
            }
            
            statBox.text += "\r\n" + "Attack Tags:";
            foreach (string atk in c.AtkTags)
            {
                statBox.text += ", " + atk;
            }
        } 
        // else if (current.subject is Spell s)
        // {
        //     //for later, when you implement spells
        // }
        
    }

    public void CloseCurrentBook()
    {
        Destroy(this.openBook);
    }
    
}
