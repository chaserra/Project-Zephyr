using UnityEngine;
using Zephyr.Player.Controls;

namespace Zephyr.Player.Movement
{
    public class PlayerStateMove : PlayerStateBase
    {
        //Cache
        private CharacterController characterController;
        private InputController inputManager;
        private Camera cam;
        private float turnSmoothVelocity;

        // Parameters
        private float moveSpeed = 5f;
        private float turnSmoothTime = .1f;


        public override void EnterState(PlayerController player)
        {
            // Initialize
            characterController = player.Controller;
            inputManager = player.Input;
            cam = player.Cam;
            moveSpeed = player.MoveSpeed;
            turnSmoothTime = player.TurnSmoothTime;
        }

        public override void Update(PlayerController player)
        {
            Move(player);
        }

        private void Move(PlayerController player)
        {
            Vector3 dir = inputManager.JoystickDirection();

            if (dir.magnitude >= 0.1f)
            {
                // Get angle of directional input
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                
                // Rotate character
                float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                player.transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Move relative to camera angle
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                characterController.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
            }
        }

    }
}