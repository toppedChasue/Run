using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAction : MonoBehaviour
{
    public event Action attack;

    ClickAction click;

    private void Start()
    {
        click = GetComponentInParent<ClickAction>();
        
    }

    public void AttackBoolenTogle()
    {
        attack += click.StandAttackBooled;
        attack?.Invoke();
    }
}
