using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyRunawayState : GhostBaseState
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
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (_ghostController != null)
        {
            // Calculate the opposite position to run away from Pac-Man.
            Vector3 playerPosition = _ghostController.PacMan.position;
            Vector3 oppositePosition = CalculateOppositePosition(playerPosition);

            // Set the ghost's destination to run away from Pac-Man.
            _ghostController.SetMoveToLocation(oppositePosition);
            _ghostController.killedEvent.AddListener(() => fsm.ChangeState(gotoDieStateHash));
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (_ghostController != null)
        {
            _ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoChaseStateHash));
        }
        if(_ghostController!=null&&_ghostController.PacMan!=null&& GameDirector.Instance.state == GameDirector.States.enState_Normal)
        {
            _ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoChaseStateHash));
        }
    }

    private Vector3 CalculateOppositePosition(Vector3 targetPosition)
    {
        Vector3 ghostPosition = _ghostController.transform.position;
        Vector3 blinkyPosition = _ghostController.GetGhostPosition("Blinky"); // Get Blinky's position

        // Calculate the direction from Inky to Blinky.
        Vector3 inkyToBlinkyDirection = blinkyPosition - ghostPosition;

        // Calculate the opposite position to run away from both Pac-Man and Blinky.
        Vector3 oppositePosition = ghostPosition - inkyToBlinkyDirection;

        return oppositePosition;
    }
}