using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;


namespace RPG.Control.Player
{
    /**
     * 
     * A Player controller class for controling Player Action
     * 
     */
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Move moverClass;
        [SerializeField] private Fighter fighter;

        // Start is called before the first frame update
        void Awake()
        {
            moverClass = GetComponent<Move>();
            fighter = GetComponent<Fighter>();
        }

        // Update is called once per frame
        void Update()
        {
            moverClass.updateforwardAnimatorMovement();
            if (handlingCombat()) return;
            if(handlingMovement()) return;
            
        }

        /**
         * Method to handle Movement of the Player 
         */
        public bool handlingMovement()
        {
            Ray ray = getMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    //Changing state from Fighter to Movement
                    moverClass.movePosition(hit.point);
                }
                
                return true;
            }
            
            return false;

        }

        /**
         * Method for handling Player combat
         */
        public bool handlingCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(getMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (!fighter.canAttack(target)) continue;
                if (target != null)
                {
                    if (Input.GetMouseButtonDown(0))
                        fighter.attack(target);
                    return true;
                }
            }
            return false;
        }

        /**
         * Method for getting Ray on clicking Mouse button in the world
         */
        public static Ray getMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
