using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour, IMoveSpeedBuff
{
    public float moveSpeed = 5f;
    public float levelStep = 2f;
    Vector3 move;

    public void AddMoveSpeedLevel(int level)
    {
        moveSpeed += level * levelStep;
    }

    public void RemoveMoveSpeedLevel(int level)
    {
        moveSpeed -= level * levelStep;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveData = context.ReadValue<Vector2>();
        move = new Vector3(moveData.x, 0, moveData.y);

        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (move.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, transform.up);
            transform.rotation = targetRotation;
        }
    }

    

    void Update()
    {
#if false
        // W/S for forward/backward movement
        float moveZ = Input.GetAxis("Vertical"); // W = +1, S = -1

        // A/D for left/right movement
        float moveX = Input.GetAxis("Horizontal"); // A = -1, D = +1

#endif
        // Combine movement directions
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        if (move.magnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, transform.up);
            transform.rotation = targetRotation;
        }
    }
}
