using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDB : MonoBehaviour
{
    public int dbSize;
    public List<String> names;
    public GameObject nullCreature;
    
    private Dictionary<String, GameObject> nameMap;
    
    public void SetupDB()
    {
        this.nameMap = new Dictionary<string, GameObject>();
        this.names = new List<string>();
        this.dbSize = 0;
        UnityEngine.Object[] summonables = Resources.LoadAll("Summonables");

        foreach (UnityEngine.Object o in summonables)
        {
            GameObject g = (GameObject) o;
            //string name = g.GetComponent<Damageable>().Name;
            string sName = g.name;
            this.nameMap.Add(sName, g);
            this.names.Add(sName);
            this.dbSize++;
        }
    }

    public GameObject GetSpell(string name)
    {
        return this.nameMap[name];
    }
}
