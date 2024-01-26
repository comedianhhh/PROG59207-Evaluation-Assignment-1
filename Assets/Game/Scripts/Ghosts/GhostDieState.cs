using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDieState : GhostBaseState
{
    public string GoToReturnState = "Return";
    private int gotoReturnStateHash;

    public float dieDelay = 3.0f;
    private float dieDelayTimer = 0.0f;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoReturnStateHash = Animator.StringToHash(GoToReturnState);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _ghostController.pathCompletedEvent.RemoveAllListeners();
        dieDelayTimer= dieDelay;
        _ghostController.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        Debug.Log("Damn I'm dead..... ");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        dieDelayTimer -= Time.deltaTime;
        if (dieDelayTimer <= 0.0f)
        {
 
            fsm.ChangeState(gotoReturnStateHash);
        }

    }

}
