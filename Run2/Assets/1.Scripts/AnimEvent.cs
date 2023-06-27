using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : ClickAction
{
    public void AttackOver()
    {
        isAttack = false;
    }
}
