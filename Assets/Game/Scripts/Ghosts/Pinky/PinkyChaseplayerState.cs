using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChaseplayerState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        gotoRunawayStateHash = Animator.StringToHash(GoToRunawayState);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (_ghostController != null&& _ghostController.PacMan != null)
        {
            if(GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
            {
                fsm.ChangeState(gotoRunawayStateHash);
                return;
            }
            // Calculate Pinky's target position based on Pac-Man's anticipated position.
            Vector3 playerPosition = _ghostController.PacMan.transform.position;
            Vector3 pinkyTargetPosition = CalculatePinkyTarget(playerPosition);

            // Set the ghost's destination to chase the calculated target position.
            _ghostController.SetMoveToLocation(pinkyTargetPosition);
        }

    }

    private Vector3 CalculatePinkyTarget(Vector3 playerPosition)
    {
        // Determine Pac-Man's direction.
        PacmanController.MoveDirection pacManDirection = GameDirector.Instance.pacmanController.moveDirection;
        Vector3 targetPosition = playerPosition;
        switch (pacManDirection)
        {
            case PacmanController.MoveDirection.Left:
                targetPosition.x-=3;
                break;
            case PacmanController.MoveDirection.Right:
                targetPosition.x += 3;
                break;
            case PacmanController.MoveDirection.Up:
                targetPosition.y += 3;
                break;
            case PacmanController.MoveDirection.Down:
                targetPosition.y -= 3;
                break;
        }

        return targetPosition;
    }
}
