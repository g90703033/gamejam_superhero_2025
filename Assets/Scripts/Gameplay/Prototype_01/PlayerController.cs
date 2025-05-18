using UnityEngine;

public class PlayerController : MonoBehaviour, ISpeedBuff
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    public void AddSpeedLevel(int level)
    {
        moveSpeed += level * 2;
    }

    public void RemoveSpeedLevel(int level)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement forward and backward
        float moveDirection = Input.GetAxis("Vertical"); // W = +1, S = -1
        transform.Translate(Vector3.forward * moveDirection * moveSpeed * Time.deltaTime);

        // Rotation left and right
        float rotationDirection = Input.GetAxis("Horizontal"); // A = -1, D = +1
        transform.Rotate(Vector3.up * rotationDirection * rotationSpeed * Time.deltaTime);
    }
}
