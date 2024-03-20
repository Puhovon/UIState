using Scripts.Player;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerInputEvents input;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        input.move += Move;
    }

    private void Move(Vector2 dir)
    {
        print("move");
        rb.velocity = new Vector3(dir.x * speed, rb.velocity.y, dir.y * speed);
    }
}
