using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObjectState : FSMBaseState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(owner);
    }
}
