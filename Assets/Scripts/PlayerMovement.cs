using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] AudioSource audioSource;
    [SerializeField] private GameObject objectToAppearPause;
     [SerializeField] private GameObject objectToAppearControls;
    bool audioCanPlay = false;

   public  Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    bool isAlive = true;
    public bool isRest = false;
    bool isPaused;
    bool isControls;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    
    void Update()
    {
        if(!isAlive || isPaused)
        {
            return;
        }
        Run();
        FlipSprite();
        ClimbLadder();
        IsRest();
        Die();
    }

    
    
   void IsRest()
   {
    if(isRest)
    {

        myAnimator.SetTrigger("isRest");
        StartCoroutine(WaitBeforeRestS());
    }
    else
        {
            myAnimator.speed = 1;
        }
   }
    

    IEnumerator WaitBeforeRestS()
    {
        yield return new WaitForSeconds(1.3f);
        myAnimator.speed = 0;
    }

    IEnumerator WaitBeforeAn()
    {
        yield return new WaitForSeconds(6f);
       
    }
   
    void OnQuit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    void OnControls()
    {
        objectToAppearControls.SetActive(true);

         isControls = !isControls; // Toggle the paused state

        if (isControls)
        {
             objectToAppearControls.SetActive(true);
           
            
            
        }
        else
        {
            objectToAppearControls.SetActive(false);
          
            
        }
    }
        void OnFire(InputValue value)
        {
            if(!isAlive || isRest) {return;}
            Instantiate(bullet, gun.position, transform.rotation);
            myAnimator.SetTrigger("isAttacking");
        }

    void Die()
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
        
        
    }
    void OnMove(InputValue value)
    {
        if(!isAlive || isRest)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
    }

    void OnPause()
{
     isPaused = !isPaused; // Toggle the paused state

        if (isPaused)
        {
             objectToAppearPause.SetActive(true);
            Time.timeScale = 0; // Pause the game
            
            
        }
        else
        {
            objectToAppearPause.SetActive(false);
            Time.timeScale = 1; // Unpause the game
            
        }
    
}

    void OnJump(InputValue value)
    {
        if(!isAlive || isRest)
        {
            return;
        }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
           
            return;
        }
        if(value.isPressed)
        {
            
    
        if (!audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    
    
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }


void ClimbLadder() 
{
    if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
             myRigidbody.gravityScale = gravityScaleAtStart;
             myAnimator.SetBool("isClimbing", false);
            return;

        }

    Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
    myRigidbody.velocity = climbVelocity;
    myRigidbody.gravityScale = 0f; 
    bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
    myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
   
    
}


void Run()
{
    audioCanPlay = true;
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
    myRigidbody.velocity = playerVelocity;

    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
   
   if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && audioCanPlay && playerHasHorizontalSpeed)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    else if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climb")) || !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) || !playerHasHorizontalSpeed)
    {
        if (audioSource.isPlaying)
        {
            audioCanPlay = false;
            audioSource.Stop();
        }
    }

}

void FlipSprite()
{
    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    if(playerHasHorizontalSpeed)
    {

    
    transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
}
}


}
