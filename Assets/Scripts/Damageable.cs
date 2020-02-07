using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    public delegate void OnDamage(float dmg, HashSet<string> tags, SideEffect s);

    public delegate void OnTurnEnd();

    public delegate void SideEffect(Damageable target);

    public delegate void OnAttack();

    public delegate void OnDeath();

    public Damageable target;

    public abstract void HandleAddDamage(float dmg, HashSet<string> tags, SideEffect s);
}
