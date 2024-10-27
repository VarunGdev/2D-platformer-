using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
  public float Speed;
  public float Damping;
  public float JumpForce;
  public Transform Ground_Check;
  public float Radius;
  public LayerMask Ground_Layermask;
  public AudioSource walkSfx;

  [Range(0f, 1f)]
  public float jumpheight;
  public float jumpTimeCounter;
  public float jumpCount;
  private bool isJumping = false;
  public float GroundTime;
  public float GroundTimePressed;
  private bool JumpPressed = false;
  public InputActionMap playerInputActions;
  public Transform Pivot;

  private Rigidbody2D rb;


  void OnEnable()
  {
    playerInputActions.Enable();
  }

  void OnDisable()
  {
    playerInputActions.Disable();
  }


  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update()
  {
    Movment();
    playerInputActions["Jump"].performed += ctx => JumpPressed = true;
    jump();
    Crouch();

  }
  void Crouch()
  {
    if (Input.GetKey(KeyCode.DownArrow))
    {
      Pivot.localScale = new UnityEngine.Vector2(Pivot.localScale.x, 0.7f);
    }
    else if (Input.GetKeyUp(KeyCode.DownArrow))
    {
      Pivot.localScale = new UnityEngine.Vector2(Pivot.localScale.x, 1f);
    }
    else { Pivot.localScale = new UnityEngine.Vector2(Pivot.localScale.x, Pivot.localScale.y); }
  }


  void jump()
  {
    bool isGrounded = Physics2D.OverlapCircle(Ground_Check.position, Radius, Ground_Layermask);

    if (JumpPressed)
    {
      GroundTime = GroundTimePressed;
    }

    if (isGrounded && JumpPressed)
    {

      isJumping = true;
      jumpTimeCounter = jumpCount - (1f - jumpheight);

      rb.velocity = new UnityEngine.Vector2(rb.velocity.x, JumpForce * Time.deltaTime);
      isGrounded = false;

    }

    if (!isGrounded)
    {
      GroundTime += Time.deltaTime;

    }
    else if (isGrounded)
    {
      GroundTime = 0f;
    }

    if (JumpPressed && isJumping)
    {
      if (jumpTimeCounter > 0)
      {
        rb.velocity = new UnityEngine.Vector2(rb.velocity.x, JumpForce);
        jumpTimeCounter -= Time.deltaTime;
      }
    }
    else
    {
      isJumping = false;
      JumpPressed = false;
    }

    if (JumpPressed)
    {
      isJumping = false;

    }
  }

  void Movment()
  {

    float Horizontal = Input.GetAxisRaw("Horizontal");

    if (Horizontal >= 0.01f)
    {
      Horizontal = 1f;
      walkSfx.Play();

    }
    else if (Horizontal <= -0.01f)
    {
      Horizontal = -1f;
      walkSfx.Play();

    }
    else
    {
      Horizontal = 0f;
    }

    float velocity = Mathf.Lerp(rb.velocity.x, Speed * Horizontal * Time.deltaTime * 100f, Damping);

    rb.velocity = new UnityEngine.Vector2(velocity, rb.velocity.y);

  }
}
