﻿using UnityEngine;

public class CharacterTypeBController : PhysicsObject
{
    [SerializeField]
    private float m_maxSpeed = 7.0f;
    [SerializeField]
    private float m_jumpTakeOffSpeed = 7.0f;

    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

    protected override void Awake()
    {
        base.Awake();

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (IsGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (m_verticalVelocity.y > .0f)
            {
                CancelJump();
            }
        }

        m_targetVelocity = move * m_maxSpeed;

        bool flipSprite = (m_spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));

        if (flipSprite)
        {
            m_spriteRenderer.flipX = !m_spriteRenderer.flipX;
        }

        m_animator.SetFloat("VelocityY", m_verticalVelocity.y);
        m_animator.SetBool("IsGrounded", IsGrounded);
        m_animator.SetFloat("VelocityX", Mathf.Abs(m_horizontalVelocity.x) / m_maxSpeed);
        m_animator.SetFloat("GroundAngle", m_groundAngle);
    }

    private void Jump()
    {
        m_verticalVelocity.y = m_jumpTakeOffSpeed;
        m_groundNormal = new Vector2(.0f, 1.0f);
    }

    private void CancelJump()
    {
        m_verticalVelocity.y = m_verticalVelocity.y * 0.5f;
    }
}
