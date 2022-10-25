using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    /**
     * Base class for movement
     */
    public class Move : MonoBehaviour, IAction
    {
        private const string forwardSpeed = "fSpeed";
        private NavMeshAgent agent;
        private Animator animator;
        private ActionScheduler actionScheduler;

        // Start is called before the first frame update
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
        }

        /**
         * Move navMesh agent to the specific destination
         * @param: Vector3 destination
         */
        public void movePosition(Vector3 destination)
        {
            actionScheduler.startAction(this);
            moveTo(destination);

        }

        public void moveTo(Vector3 destination)
        {
            agent.destination = destination;
            agent.isStopped = false;
        }

        /**
         * Method for stopping the agent movement
         */
        public void Cancel()
        {
            agent.isStopped = true;
        }
        
        /**
         * Method for updating the animation
         */
        public void updateforwardAnimatorMovement()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
            animator.SetFloat(forwardSpeed, localVelocity.z);
        }

    }
}
