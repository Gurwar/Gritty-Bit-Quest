using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCatcher : MonoBehaviour
{
    public void PlayAudio()
    {

    }

    public void FireArrow()
    {
        Debug.LogError("1");
        //GetComponentInParent<AIBowScript>().SolveForFireArrow();
    }
    public void Melee(float damage)
    {
        GetComponentInParent<G_AIMeleeScript>().SolveForMeleeAttack(damage);
    }

    public void FireSpell()
    {
        //GetComponentInParent<SpellScript>().SolveForSpellAttack();
    }
}
