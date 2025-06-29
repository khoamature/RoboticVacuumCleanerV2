using UnityEngine;

public class RobotController2D : MonoBehaviour
{
    public float speed = 5f;
    public bool canMoveDiagonally = true;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        // Lấy input từ người chơi
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D hoặc mũi tên trái/phải
        float moveY = Input.GetAxisRaw("Vertical");   // W/S hoặc mũi tên lên/xuống

        if (!canMoveDiagonally)
        {
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
                moveY = 0;
            else
                moveX = 0;
        }

        movementInput = new Vector2(moveX, moveY).normalized;

        // Xoay robot nếu có hướng
        if (movementInput != Vector2.zero)
        {
            RotateRobot(movementInput.x, movementInput.y);
        }
    }

    void FixedUpdate()
    {
        // Di chuyển dựa vào input
        rb.linearVelocity = movementInput * speed;
    }

    void RotateRobot(float x, float y)
    {
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
