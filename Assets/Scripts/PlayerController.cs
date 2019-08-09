using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	public CharacterController characterController;
	public float gravity;
	public float lateralSpeed = 0.8f;
	public float speed = 1f;
    public float jumpForce = 2f;
	public Animator anim;
	public AudioSource jumpSound;


	private float fallingVelocity;
	private float lateralInput;
	private float frontInput;

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
	}

    // Update is called once per frame
    void Update() {


        //PLAYER INPUT
        lateralInput = Input.GetAxis("Horizontal");
        frontInput = Input.GetAxis("Vertical");

        fallingVelocity -= gravity * Time.deltaTime;

        if (characterController.isGrounded)
        {
            fallingVelocity = 0f;
            
        } 
		
		if (Physics.Raycast (transform.position, -transform.up, 1.15f))
		{
		if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
			if(Mathf.Abs(lateralInput)+Mathf.Abs(frontInput) > 0f)
			{
				anim.SetBool("isWalking", true);		
			} 
			else 
			{
				anim.SetBool("isWalking", false);	
			}
		}
		else
		{
			anim.SetBool("isWalking", false);
		}
		
        characterController.Move(transform.forward*speed*Time.deltaTime*frontInput + transform.right*lateralSpeed*Time.deltaTime*lateralInput + transform.up*fallingVelocity*Time.deltaTime);

    
	 }
		
	
	
	private void Jump()
	{
        fallingVelocity = jumpForce;
		anim.Play("Hands_jump");
		jumpSound.Play();
	}
}
