using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    /**
     * Attach this script to the target which 
     * is used to track player and make camera child of this
     * object
     * Camera follows the target
     */
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        void LateUpdate()
        {
            transform.position = playerTransform.position;
        }
    }
}
