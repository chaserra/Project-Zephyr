﻿using UnityEngine;

namespace Zephyr.Player.Movement
{
    public class PlayerMover 
    {
        // Cache
        private float turnSmoothVelocity;

        public void Move(PlayerController player, bool playerCanRotate, bool playerCanMove, float speedMultiplier)
        {
            Vector3 dir = player.Input.JoystickDirection();

            if (dir.magnitude >= 0.1f)
            {
                // Get angle of directional input
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + player.Cam.transform.eulerAngles.y;

                if (playerCanRotate)
                {
                    // Rotate character
                    float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, player.GetTurnSmoothTime / speedMultiplier);
                    player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }

                // Move relative to camera angle
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if (playerCanMove)
                {
                    player.Controller.Move(moveDir.normalized * player.GetMoveSpeed * Time.deltaTime * speedMultiplier);
                }
            }
            else
            {
                // Reset reference to how fast the player rotates to prevent quick turn bug.
                turnSmoothVelocity = 0f;
            }
        }
    }
}