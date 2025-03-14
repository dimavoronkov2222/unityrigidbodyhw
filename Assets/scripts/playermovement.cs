using UnityEngine;
public class playermovement : MonoBehaviour
{
    public GameObject playerCapsule;
    public float walkSpeed = 2f;
    public float sprintSpeed = 4f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;
    private Rigidbody rb;
    private float playerSpeed;
    private float yRot;
    private bool isGrounded;
    void Start()
{
    if (playerCapsule == null)
    {
        Debug.LogError("❌ Ошибка: `playerCapsule` не назначен! Перетащи капсулу в инспекторе.");
        enabled = false;
        return;
    }
    
    Debug.Log("✅ `playerCapsule` найден: " + playerCapsule.name);

    rb = playerCapsule.GetComponent<Rigidbody>();

    if (rb == null)
    {
        Debug.LogError("❌ Ошибка: `Rigidbody` отсутствует на `playerCapsule`! Добавь его.");
        enabled = false;
        return;
    }

    Debug.Log("✅ `Rigidbody` найден!");
    rb.freezeRotation = true;
    Cursor.lockState = CursorLockMode.Locked;
    playerSpeed = walkSpeed;
}

    void Update()
	{
    	if (rb == null) return;
    	yRot += Input.GetAxis("Mouse X") * mouseSensitivity;
    	transform.localEulerAngles = new Vector3(0, yRot, 0);
    	float moveX = Input.GetAxisRaw("Horizontal");
    	float moveZ = Input.GetAxisRaw("Vertical");
    	Vector3 move = transform.right * moveX + transform.forward * moveZ;
    	rb.linearVelocity = new Vector3(move.x * playerSpeed, rb.linearVelocity.y, move.z * playerSpeed);
    	if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
    	{
        	rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
    	}
    	playerSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
    }
    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
