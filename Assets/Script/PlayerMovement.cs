using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float jumpforce;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float raycastDistance;

    [SerializeField]
    private LayerMask mask;

    private Rigidbody2D rb;

    private bool grounded;

    private Collider2D col;

    private bool canMove = true;

    private Animator animator;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.instance.CallStartBreak += LockMovement;
        GameManager.instance.CallStopBreak += UnlockMovement;
    }

    void Update()
    {
        if(canMove)
        {
            RaycastHit2D hit1;
            RaycastHit2D hit2;

            hit1 = Physics2D.Raycast(col.bounds.center + Vector3.left * col.bounds.extents.x, Vector2.down, col.bounds.extents.y + raycastDistance + 0.5f);
            hit2 = Physics2D.Raycast(col.bounds.center + Vector3.right * col.bounds.extents.x, Vector2.down, col.bounds.extents.y + raycastDistance + 0.5f);
        

            if ((hit1.collider != null || hit2.collider != null))
            {
                transform.rotation = Quaternion.identity;

                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && !animator.IsInTransition(0))
                {
                    animator.SetBool("StopJump", true);
                }
                hit1 = Physics2D.Raycast(col.bounds.center + Vector3.left * col.bounds.extents.x, Vector2.down, col.bounds.extents.y + raycastDistance + raycastDistance);
                hit2 = Physics2D.Raycast(col.bounds.center + Vector3.right * col.bounds.extents.x, Vector2.down, col.bounds.extents.y + raycastDistance + raycastDistance);
                if ((hit1.collider != null || hit2.collider != null))
                {
                    grounded = true;
                }
            }
            else if(grounded == true)
            {
                grounded = false;
                animator.SetBool("StopJump", false);
            }

            if (Input.GetKeyDown(KeyCode.W) && grounded == true)
            {
                transform.position += (Vector3)(Vector2.up * raycastDistance);
                rb.velocity = new Vector2(rb.velocity.x * speed, jumpforce);

                if (animator != null)
                {
                    animator.SetTrigger("doJump");
                    animator.SetBool("StopJump", false);
                }
            }

            Debug.DrawLine(col.bounds.center + Vector3.left * col.bounds.extents.x / 2, col.bounds.center + (Vector3.left * col.bounds.extents.x / 2) + (Vector3.down * (col.bounds.extents.y + raycastDistance)), Color.red);
            Debug.DrawLine(col.bounds.center + Vector3.right * col.bounds.extents.x / 2, col.bounds.center + (Vector3.right * col.bounds.extents.x / 2) + (Vector3.down * (col.bounds.extents.y + raycastDistance)), Color.red);

            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rb.velocity.y);
            if(dir.x > 0)
            {
                animator.SetFloat("speed", 1);
            }
            else if (dir.x < 0)
            {
                animator.SetFloat("speed", -1);
            }

            rb.velocity = dir;
        }
    }


    private void LockMovement()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        canMove = false;
    }

    private void UnlockMovement()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        canMove = true;
    }
}
