using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyChasePlayerState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    private Transform clydeTransform; // Reference to Clyde's transform
    public float inkyDistance = 4.0f; // Adjust as needed

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRunawayStateHash = Animator.StringToHash(GoToRunawayState);

        // Find Clyde's transform. Make sure Clyde is tagged appropriately in your scene.
        GameObject clydeGameObject = GameObject.FindGameObjectWithTag("Clyde");
        if (clydeGameObject != null)
        {
            clydeTransform = clydeGameObject.transform;
        }
        else
        {
            Debug.LogError("Clyde not found. Make sure Clyde is in the scene and tagged appropriately.");
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (_ghostController != null&&GameDirector.Instance.state==GameDirector.States.enState_PacmanInvincible)
        {
            _ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoRunawayStateHash));
        }

        if (_ghostController != null && _ghostController.PacMan != null && clydeTransform != null)
        {
            // Calculate the position based on Pac-Man's position and Clyde's position.
            Vector3 playerPosition = _ghostController.PacMan.transform.position;
            Vector3 clydePosition = clydeTransform.position;
            Vector3 inkyTargetPosition = CalculateInkyTarget(playerPosition, clydePosition);

            // Set the ghost's destination to chase the calculated target position.
            _ghostController.SetMoveToLocation(inkyTargetPosition);
        }
    }

    private Vector3 CalculateInkyTarget(Vector3 playerPosition, Vector3 clydePosition)
    {
        Vector3 blinkyPosition = _ghostController.GetGhostPosition("Blinky"); // Assuming you have a method to get Blinky's position.
        Vector3 targetPosition = clydePosition + 2 * (playerPosition - blinkyPosition);
        return targetPosition;
    }
}
