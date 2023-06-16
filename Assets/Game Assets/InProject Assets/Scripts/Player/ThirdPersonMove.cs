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
    public Camera PlayerCamera;
    public Camera IntroCamera;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;
    public GameObject InventoryScreen; 
    private bool isInvetoryVisible = false;
    public GameObject QuestUI;
    private bool isQuestUIVisible = false;


    void Start()
    {
        animator.SetBool("NewGame", true);
        Invoke("Game", 1.3f);
        Invoke("CameraChange", 4.3f);
    }


    private void ToggleObjectVisibility()
    {
        isInvetoryVisible = !isInvetoryVisible; 

        InventoryScreen.SetActive(isInvetoryVisible); 
    }

    private void ToggleUIVisibility()
    {
        isQuestUIVisible = !isQuestUIVisible; 

        QuestUI.SetActive(isQuestUIVisible);
    }


    private void Game()
    {
       
        
        animator.SetBool("NewGame", false);
    }

    private void CameraChange()
    { 
    IntroCamera.gameObject.SetActive(false);
    PlayerCamera.gameObject.SetActive(true);
    }

    void FixedUpdate()
    {

        

        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

          
            inputDirection = transform.TransformDirection(inputDirection);

     
            moveDirection = inputDirection * speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

        }



       
        Vector3 cameraDirection = mainCamera.forward;
        cameraDirection.y = 0f;

    
        moveDirection = Vector3.ClampMagnitude(cameraDirection * Input.GetAxis("Vertical") + mainCamera.right * Input.GetAxis("Horizontal"), 1f) * speed;

       
        float cameraRotationY = mainCamera.rotation.eulerAngles.y;

     
        transform.rotation = Quaternion.Euler(0f, cameraRotationY, 0f);

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        



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


        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ToggleObjectVisibility(); 
        }

        if (Input.GetKeyDown(KeyCode.J)) 
        {
            ToggleUIVisibility(); 
        }

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

     
        if (Time.time > nextFireTime)
        {
           
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();

            }
        }



    }

    public void OnClick()
    {   
        Collider collider = GameObject.Find("Sword_1").GetComponent<Collider>();
        
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
    

