using UnityEngine;

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
                    float angle = Mathf.SmoothDampAngle(player.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, player.TurnSmoothTime);
                    player.transform.rotation = Quaternion.Euler(0f, angle, 0f);
                }

                // Move relative to camera angle
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if (playerCanMove)
                {
                    player.Controller.Move(moveDir.normalized * player.MoveSpeed * Time.deltaTime * speedMultiplier);
                }
            }
        }
    }
}