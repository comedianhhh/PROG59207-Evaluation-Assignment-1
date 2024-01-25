using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyRunawayState : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;


    public string GoToDieState = "Die";
    private int gotoDieStateHash;

    public List<Vector2> loopWaypoints; // List of waypoints forming a loop
    public int currentWaypointIndex;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
        gotoDieStateHash = Animator.StringToHash(GoToDieState);

    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        currentWaypointIndex = Random.Range(0, loopWaypoints.Count);
        if (_ghostController != null)
        {
            _ghostController.SetMoveToLocation(loopWaypoints[currentWaypointIndex]);
            _ghostController.pathCompletedEvent.AddListener(() =>
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= loopWaypoints.Count)
                {
                    currentWaypointIndex = 0;
                }
                _ghostController.SetMoveToLocation(loopWaypoints[currentWaypointIndex]);
            });
            _ghostController.killedEvent.AddListener(() => fsm.ChangeState(gotoDieStateHash));
        }

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        Debug.Log("Pinky" + _ghostController.pathCompleted);
        if (_ghostController != null)
        {
            if ( _ghostController.PacMan != null && GameDirector.Instance.state == GameDirector.States.enState_Normal)
            {
                fsm.ChangeState(gotoChaseStateHash);
            }
        }


    }

}
