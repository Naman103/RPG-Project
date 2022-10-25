using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using Unity.Collections;
using RPG.Core;

namespace RPG.Combat
{
    /**
     * Base class for implemnting Combat
     * 
     * 
     */
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private int weaponDamage = 4;

        private float timeSinceLastAttack = 0f;
        private Move move;
        private Health target;
        private ActionScheduler actionScheduler;
        private Animator anim;

        private void Start()
        {
            move = GetComponent<Move>();
            actionScheduler = GetComponent<ActionScheduler>();
            anim = GetComponent<Animator>();
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (target == null || target.getIsDead()) return; 
            if (!getInRange())
            {
                //If enemy is beyond weaponRange first move enemy in weapon Range
                move.moveTo(target.transform.position);
            }
            else
            {
                move.Cancel();
                attackBehavior();
                //position where attacking to enemy starts
            }
        }

        public bool canAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.getIsDead();
        }
        /**
         * Method for attacking
         */
        public void attack(CombatTarget combatTarget)
        {
            actionScheduler.startAction(this);
            this.target = combatTarget.transform.GetComponent<Health>();
        }

        /**
         * Method for canceling combat
         */
        public void Cancel()
        {
            anim.ResetTrigger("attack");
            anim.SetTrigger("stopAttack");
            this.target = null;
        }

        /**
         * Method to check wether  target is in the specified range or not
         */
        public bool getInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        void Hit()
        {
            if (this.target != null) 
                target.damage(weaponDamage);
        }

        public void attackBehavior()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                anim.ResetTrigger("stopAttack");
                anim.SetTrigger("attack");
                timeSinceLastAttack = 0f;
            }
        }
    }
}
