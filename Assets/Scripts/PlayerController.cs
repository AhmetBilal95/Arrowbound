using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarlari")]
    public float moveSpeed = 6f;
    public float rotateSpeed = 15f;

    private Rigidbody rb;
    private Vector3 moveDirection;
    public bool IsMoving { get; private set; }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Test aşamasında klavye girdilerini alıyoruz (WASD veya Ok Tuşları)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Archero mantığı: Karakter hareket ediyor mu kontrolü
        IsMoving = moveDirection.sqrMagnitude > 0.05f;

        // Hareket ediyorsa gittiği yöne baksın
        if (IsMoving)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        // Unity 6 için linearVelocity kullanılır
        if (IsMoving)
        {
            rb.linearVelocity = moveDirection * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector3.zero;
        }
    }
}