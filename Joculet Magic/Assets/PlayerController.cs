using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movementInput;
    Rigidbody2D rb;
    public float movementSpeed = 1f;

    public ContactFilter2D movementFilter;
    List<RaycastHit2D> castCollision = new List<RaycastHit2D>();

    public float collisionOffset = 0.05f;
    
    Animator animator;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //acceseaza componenta rigidbody
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

  
    private void FixedUpdate()
    {
        if(movementInput != Vector2.zero) //daca incearca sa se miste 
        {
          bool succes= TryMove(movementInput);
          if(!succes )
          {
            succes = TryMove( new Vector2(movementInput.x, 0));
            
          }
          if (!succes)
            {
                succes = TryMove(new Vector2(0, movementInput.y));
            }

            animator.SetBool("isMoving", succes);

        } else
        {
            animator.SetBool("isMoving", false);
        }

        if(movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

private bool TryMove(Vector2 direction)
{
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                           direction,
                           movementFilter,
                           castCollision,
                           movementSpeed * Time.fixedDeltaTime + collisionOffset
                   );
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {

                return false;
            }
        }
        else
        {
            return false;
        }
           
}
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();

    }
}
