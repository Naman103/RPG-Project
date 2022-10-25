using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        private int maxHealthPoints = 100;
        private int currentHealthPoint;
        private bool isDead;
        private Animator anim;

        public void Awake()
        {
            anim = GetComponent<Animator>();
            isDead = false;
            currentHealthPoint = maxHealthPoints;
        }

        public void damage(int damagePoints)
        {
            currentHealthPoint = Mathf.Max(currentHealthPoint-damagePoints,0);
            if (currentHealthPoint <= 0 && !isDead)
            {
                anim.SetTrigger("dead");
                isDead = true;
            }
        }

        public bool getIsDead()
        {
            return isDead;
        }

    }
}
