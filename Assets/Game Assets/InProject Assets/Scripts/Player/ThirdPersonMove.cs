using System.Collections;


using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class ThirdPersonMove : MonoBehaviour
{

    public float speed = 3.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    public Transform mainCamera;
    public Animator animator;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;



    void Start()
    {


    }


    void FixedUpdate()
    {



        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            // Получаем направление движения от ввода игрока
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // Переводим направление движения в локальные координаты персонажа
            inputDirection = transform.TransformDirection(inputDirection);

            // Устанавливаем направление движения
            moveDirection = inputDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

        }



        // Получаем вектор направления от персонажа до камеры
        Vector3 cameraDirection = mainCamera.forward;
        cameraDirection.y = 0f;

        // Устанавливаем направление движения в направление камеры
        moveDirection = Vector3.ClampMagnitude(cameraDirection * Input.GetAxis("Vertical") + mainCamera.right * Input.GetAxis("Horizontal"), 1f) * speed;

        // Получаем вращение камеры по оси Y
        float cameraRotationY = mainCamera.rotation.eulerAngles.y;

        // Устанавливаем только вращение по оси Y для персонажа
        transform.rotation = Quaternion.Euler(0f, cameraRotationY, 0f);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        Debug.Log("Is grounded: " + controller.isGrounded + " " + "Jump: " + moveDirection.y);



        if (moveDirection.magnitude > 2)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }



    }

       void Update(){
        
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            animator.SetBool("hit2", false);
        }
        


        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        //cooldown time
        if (Time.time > nextFireTime)
        {
            // Check for mouse input
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();

            }
        }



    }

    public void OnClick()
    {   
        Collider collider = GameObject.Find("Sword_1").GetComponent<Collider>();
        //so it looks at how many clicks have been made and if one animation has finished playing starts another one.
        lastClickedTime = Time.time;
        noOfClicks++; 
        
        if (noOfClicks == 1)
        {
            animator.SetBool("hit1", true);
            collider.enabled = true;
           
        }

        
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);

        if (noOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            animator.SetBool("hit1", false);
            animator.SetBool("hit2", true);
            collider.enabled = true;
          

        }

       
           
        
        
    }
}
    




/*
public CharacterController controller;
public Transform cam;
public Animator animator;
public float speed = 6;
public float gravity = -9.81f;
public float jumpHeight = 3f;
public Transform groundCheck;
public float groundDistance = 0.4f;
public LayerMask groundMask;

Vector3 velocity;
bool isGrounded;
bool isJumping;

float turnSmoothVelocity;
public float turnSmoothTime = 0.1f;

void Update()
{
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) ||
         Physics.CheckSphere(groundCheck.position + new Vector3(0, -0.1f, 0), groundDistance, groundMask) ||
         Physics.CheckSphere(groundCheck.position + new Vector3(0, -0.2f, 0), groundDistance, groundMask);


    if (Physics.SphereCast(transform.position, controller.radius, Vector3.up, out RaycastHit hit, groundDistance, groundMask))
    {
        float distanceToGround = hit.distance;
        if (distanceToGround < groundDistance)
        {
            isGrounded = true;
            velocity.y = -2f;
        }
        else
        {
            isGrounded = false;
        }
    }
    else
    {
        isGrounded = false;
    }

    if (controller.isGrounded && velocity.y < 0)
    {
        velocity.y = -2f;

    }


    if (Input.GetButtonDown("Jump") && controller.isGrounded)
    {
        isJumping = true;
        animator.SetBool("Jump", true);
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        Invoke("StopJumpAnimation", 0.1f);
    }


    if (!controller.isGrounded && isJumping && velocity.y > 0)
    {
        isJumping = false;
        animator.SetBool("Land", true);
    }

    if (!isGrounded)
    {
        animator.SetBool("Land", true);
    }

    if (controller.isGrounded && animator.GetBool("Land"))
    {

        animator.SetBool("Land", false);
    }

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    float horizontal = Input.GetAxisRaw("Horizontal");
    float vertical = Input.GetAxisRaw("Vertical");
    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    if (direction.magnitude >= 0.1f)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);

        animator.SetBool("Walk", true);
    }
    else
    {
        animator.SetBool("Walk", false);
    }


    if (!controller.isGrounded && isJumping && velocity.y > 0)
    {
        isJumping = false;
        animator.SetBool("Land", true);
        Debug.Log("Landed, isJumping = " + isJumping);
    }

    if (!controller.isGrounded)
    {
        animator.SetBool("Land", true);
        Debug.Log("Not grounded, isJumping = " + isJumping);
    }

    if (controller.isGrounded && animator.GetBool("Land"))
    {
        animator.SetBool("Land", false);
        Debug.Log("Grounded, isJumping = " + isJumping);
    }

}



void StopJumpAnimation()
{
    animator.SetBool("Jump", false);
}


*/

/*	public CharacterController controller;
	public float speed = 6f;
	public float turnShoothTime = 0.1f;
	float turnSmoothVelocity;

	Update is called once per frame
	void Update()
	{
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");
		Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

		if (direction.magnitude >= 0.1f)
		{
			float targetAngle = Mathf.Atan2(direction.x, direction.y);
			float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnShoothTime);
			transform.rotation = Quaternion.Euler(0f, angle, 0f);
			controller.Move(direction * speed * Time.deltaTime);
		}
	}
}*/







/*public float moveSpeed = 5f; // скорость движения персонажа
public Animator animator;
private Rigidbody rb;
float turnSmoothVelocity;
float turnSmoothTime = 0.1f;

void Start()
{
    rb = GetComponent<Rigidbody>();
}

void FixedUpdate()
{
    float moveHorizontal = Input.GetAxis("Horizontal"); // получаем значение оси X (A, D или ←, →)
    float moveVertical = Input.GetAxis("Vertical"); // получаем значение оси Z (W, S или ↑, ↓)

    Vector3 moveDirection = new Vector3(moveHorizontal, 0f, moveVertical).normalized;
    rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);


    if (moveDirection.magnitude >= 0.1f)
    {
        float targetAngle = Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

    }








    if (moveDirection.magnitude > 0f) // если персонаж движется, переключаем анимации
    {
        animator.SetBool("Walk", true);
    }
    else
    {
        animator.SetBool("Walk", false);
    }


}*/