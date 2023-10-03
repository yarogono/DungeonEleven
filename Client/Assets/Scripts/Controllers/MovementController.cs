using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    private static readonly int isJump = Animator.StringToHash("isJump");
    private static readonly int isFall = Animator.StringToHash("isFall");

    private PlayerController _controller;
    private PlayerStatsHandler _stats;

    
    private Vector2 _movementDirection = Vector2.zero;
    private Rigidbody2D _rigidbody;
    public Animator anim;

    private Vector2 _Knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;
    public SpriteRenderer _spriteRenderer;

    
    [Range(1f, 20f)] public float jumpingPower = 5f;
    public float maxSpeed;

    private void Awake()
    {
        _controller = GetComponent<PlayerController>(); 
        _stats = GetComponent<PlayerStatsHandler>();   
        _rigidbody = GetComponent<Rigidbody2D>();
    }



    
    void Start()
    {
        _controller.OnMoveEvent += Move;
        _controller.OnJumpEvent += Jump;
        _controller.OnFallEvent += Fall;
    }

   void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.normalized.x * 0.5f, _rigidbody.velocity.y);
        }
        if (Input.GetButtonDown("Horizontal"))
        {
            _spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        ApplyJump(jumpingPower);
        
    }


    private void FixedUpdate()
    {
        ApplyMovement(_movementDirection);
        /*if (knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }*/
        Debug.DrawRay(_rigidbody.position, Vector2.down, new Color(0, 1, 0));
        
        RaycastHit2D rayHit = Physics2D.Raycast(_rigidbody.position, Vector3.down, 1,LayerMask.GetMask("Map"));
        
        if (_rigidbody.velocity.y < 0)
        {
            anim.SetBool(isFall, _rigidbody.velocity.y < 0f);
            anim.SetBool(isJump, false);
            if (rayHit.collider != null)
            {
                if(rayHit.distance > 0.5f)
                {
                    
                    anim.SetBool("isFall", false);
                }
            }
        }
    }

    private void Jump()
    {
        
    }

    private void ApplyJump(float direction)
    {
        if (Input.GetButtonDown("Jump") && !anim.GetBool("isJump"))
        {
            Debug.Log("무브먼트");

            _rigidbody.AddForce(Vector2.up * direction, ForceMode2D.Impulse);
            anim.SetBool(isJump, _rigidbody.velocity.y > 0.1f);
        }
    }
    
    private void ApplyFall()
    {
        Debug.Log("Fall : " + _rigidbody.velocity.y);
        
        anim.SetBool(isFall, _rigidbody.velocity.y < 0f);
        
    }

    private void Move(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void Fall()
    {

    }

    private void ApplyMovement(Vector2 direction)
    {
        _rigidbody.AddForce(Vector2.right * direction.x, ForceMode2D.Impulse);
        if(_rigidbody.velocity.x> maxSpeed)
        {
            _rigidbody.velocity = new Vector2(maxSpeed, _rigidbody.velocity.y);
        }else if(_rigidbody.velocity.x < maxSpeed * (-1))
        {
            _rigidbody.velocity = new Vector2(maxSpeed*(-1), _rigidbody.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Enemy") {
            OnDamaged(collision.transform.position);
        }    
    }

    void OnDamaged(Vector2 targetPos)
    {
        //무적
        gameObject.layer = 10;
        _spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;

        //넉백
        Debug.Log(dirc);
        _rigidbody.AddForce(new Vector2(-1, 1) * 5, ForceMode2D.Impulse);


        // animation

        anim.SetTrigger("isHit");
       Invoke("OffDamaged", 3);

    }

    void OffDamaged()
    {
        gameObject.layer = 3;

        _spriteRenderer.color = new Color(1, 1, 1, 1);


    }


    /*public void ApplyKnockkback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        _Knockback = -(other.position - transform.position).normalized * power;
    }*/

}
