using UnityEngine;
using Mirror;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PohybHraca : NetworkBehaviour
{
    // nastavenia
    public float walkingSpeed = 7.5f;

    public float runningSpeed = 10f;

    public float jumpSpeed = 8f;

    public float gravity = 20f;

    public Camera playerCamera;

    public float lookSpeed = 2f;

    public float lookXLimit = 75f;

    Animator anim;
    CharacterController charCtrl;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0f;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        charCtrl = GetComponent<CharacterController>();

        // myö
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // pohyb
    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // ovl·danie
        bool isRunning = Input.GetKey(KeyCode.LeftControl);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        // sk·kanie
        if (Input.GetButton("Jump") && canMove && charCtrl.isGrounded) {
            moveDirection.y = jumpSpeed;

            // zvuky
            FindObjectOfType<AudioManager>().PrehratZvuk("Skok");
        }
        else moveDirection.y = movementDirectionY;

        // gravit·cia
        if (!charCtrl.isGrounded) moveDirection.y -= gravity * Time.deltaTime;

        // pohyb
        charCtrl.Move(moveDirection * Time.deltaTime);
        anim.SetBool("isRunning", curSpeedX != 0 || curSpeedY != 0);

        // ot·Ëanie
        if (canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}
