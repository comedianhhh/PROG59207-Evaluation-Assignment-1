using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDieState : GhostBaseState
{
    public string GoToReturnState = "Return";
    private int gotoReturnStateHash;


    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoReturnStateHash = Animator.StringToHash(GoToReturnState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ghostController.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        fsm.ChangeState(gotoReturnStateHash);
        Debug.Log("Damn I'm dead..... ");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        

    }

}
