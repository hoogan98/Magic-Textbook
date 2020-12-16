using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Book : MonoBehaviour
{
    private CreatureDB db;
    private Dictionary<string, Tuple<string, bool>> nameTranslations;
    private List<string> spellIndexes;
    
    private List<char> consonants= new List<char>()
    {
        'q','w','r','t','p','s','d','f','g','h','j','k','l','z','x','c','v','b','n','m'
    };
    private List<char> vowels= new List<char>()
    {
        'a','e','i','o','u','y'
    };
    //this is in here to make sure that spells aren't named something bad
    private List<string> badwords = new List<string>()
    {
        "nig", "fag", "wanker", "fuck", "cancer", "cunt", "cock", "cuck", "shit", "ass", "cum", "tard", "dick", "tit",
        "pussy", "twat", "clit", "gypsy", "jap", "lesbo"
    };

    public void SetCube(GameObject timeCube)
    {
        this.db = timeCube.GetComponent<CreatureDB>();
    }

    public GameObject AttemptSummon(string spellText)
    {
        if (this.nameTranslations.ContainsKey(spellText))
        {
            Tuple<string, bool> transName = this.nameTranslations[spellText];
            Tuple<string, bool> newName = new Tuple<string, bool>(transName.Item1, true);
            GameObject newSpell = this.db.GetSpell(transName.Item1);

            this.nameTranslations[spellText] = newName;
            Debug.Log("chose spell: " + newName.Item1);
            return newSpell;
        }

        Debug.Log("no such spell exists! you summoned a null creature");
        return this.db.nullCreature;
    }

    public Page PrintPage(int pg)
    {
        string spellName = this.spellIndexes[pg];
        string realName = this.nameTranslations[spellName].Item1;
        Damageable currentSpell = this.db.GetSpell(realName).GetComponent<Damageable>();
        
        return new Page
        {
            realName = realName,
            spellName = spellName,
            description = currentSpell.description,
            facts = currentSpell.facts
        };
    }

    public void GenerateBook()
    {
        this.nameTranslations = new Dictionary<string, Tuple<string, bool>>();
        this.spellIndexes = new List<string>();
        List<string> names = this.db.names;
        
        for (int i = 0; i < db.dbSize; i++)
        {
            //2 clauses, CVC in style
            string newName = "";
            newName += this.GetRandomConsonant();
            newName += this.GetRandomVowel();
            newName += this.GetRandomConsonant();
            newName += this.GetRandomConsonant();
            newName += this.GetRandomVowel();
            newName += this.GetRandomConsonant();

            bool cont = false;
            
            foreach (string bad in badwords)
            {
                if (newName == bad || newName.Contains(bad))
                {
                    i--;
                    cont = true;
                    break;
                }
            }

            if (this.nameTranslations.ContainsKey(newName) || cont)
            {
                i--;
                continue;
            }
            
            Debug.Log(newName + " = " + names[i]);
            this.nameTranslations.Add(newName, new Tuple<string, bool>(names[i], false));
            this.spellIndexes.Add(newName);
        }
    }

    private char GetRandomConsonant()
    {
        int chosen = Random.Range(0, this.consonants.Count);
        return this.consonants[chosen];
    }

    private char GetRandomVowel()
    {
        int chosen = Random.Range(0, this.vowels.Count);
        return this.vowels[chosen];
    }
    
}
