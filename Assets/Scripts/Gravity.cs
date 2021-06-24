using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zephyr.Util
{
    [RequireComponent(typeof (CharacterController))]
    public class Gravity : MonoBehaviour
    {
        // Cache
        private CharacterController characterController;

        // Properties
        [SerializeField] private float groundDistance = 0.2f;
        [SerializeField] private LayerMask groundMask;
        private const float gravity = -9.81f;
        private const float freeFallGravity = -3.6f;

        // State
        private Vector3 velocity;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            ApplyGravity();
        }

        private void ApplyGravity()
        {
            if (IsGrounded() && velocity.y < 0)
            {
                velocity.y = freeFallGravity; //Prevents floating or immediate fall when free-falling
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }
            characterController.Move(velocity * Time.deltaTime);
        }

        private bool IsGrounded()
        {
            return Physics.CheckSphere(transform.position, groundDistance, groundMask);
        }
    }
}