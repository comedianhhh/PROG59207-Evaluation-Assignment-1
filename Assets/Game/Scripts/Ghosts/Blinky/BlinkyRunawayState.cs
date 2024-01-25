using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyRunawayState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;


    public string GoToDieState = "Die";
    private int gotoDieStateHash;
    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
        gotoDieStateHash = Animator.StringToHash(GoToDieState);

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator,stateInfo,layerIndex);
        if (_ghostController != null)
        {
            _ghostController.killedEvent.AddListener(() => fsm.ChangeState(gotoDieStateHash));
        }

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (_ghostController != null)
        {
            Vector3 playerPosition = _ghostController.PacMan.position;
            Vector3 oppositePosition = CalculateOppositePosition(playerPosition);

            // Set the ghost's destination to run away from Pac-Man.
            _ghostController.SetMoveToLocation(oppositePosition);
        }
        if (_ghostController!=null&& _ghostController.PacMan != null && GameDirector.Instance.state == GameDirector.States.enState_Normal)
        {
           fsm.ChangeState(gotoChaseStateHash);
        }
    }
    private Vector3 CalculateOppositePosition(Vector3 targetPosition)
    {

        Vector3 ghostPosition = _ghostController.transform.position;
        Vector3 directionToTarget = targetPosition - ghostPosition;
        Vector3 oppositePosition = ghostPosition - (directionToTarget * 2);

        return oppositePosition;
    }
}
