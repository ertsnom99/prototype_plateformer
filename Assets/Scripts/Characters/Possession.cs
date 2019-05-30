﻿using Cinemachine;
using UnityEngine;

// This script requires thoses components and will be added if they aren't already there
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class Possession : MonoBehaviour, IPhysicsObjectCollisionListener
{
    [Header("Camera")]
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    [Header("Sprite")]
    [SerializeField]
    private bool _flipSpawnedPlayer = false;

    [Header("Sounds")]
    [SerializeField]
    private AudioClip _enterPossessionModeSound;
    [SerializeField]
    private AudioClip _exitPossessionModeSound;

    public bool InPossessionMode { get; private set; }

    private const string _possessModeAnimationLayerName = "Possess Mode";
    private int _possessModeAnimationLayerIndex;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        InPossessionMode = false;
        // Change if player collides with AIs
        //Physics2D.IgnoreLayerCollision(GameManager.PlayerLayerIndex, GameManager.AILayerIndex, !InPossessionMode);

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        _possessModeAnimationLayerIndex = _animator.GetLayerIndex(_possessModeAnimationLayerName);
        _animator.SetLayerWeight(_possessModeAnimationLayerIndex, .0f);
    }

    public void ChangePossessionMode(bool inPossessionMode)
    {
        if (inPossessionMode != InPossessionMode)
        {
            InPossessionMode = inPossessionMode;
            _animator.SetLayerWeight(_possessModeAnimationLayerIndex, InPossessionMode ? 1.0f : .0f);
            
            // Change if player collides with AIs
            //Physics2D.IgnoreLayerCollision(GameManager.PlayerLayerIndex, GameManager.AILayerIndex, !InPossessionMode);

            if (InPossessionMode)
            {
                _audioSource.PlayOneShot(_enterPossessionModeSound);
            }
            else
            {
                _audioSource.PlayOneShot(_exitPossessionModeSound);
            }
        }
    }

    // Take possession of the given AIController
    public void TakePossession(AIController possessedController)
    {
        if (possessedController.Possess(this))
        {
            ChangePossessionMode(false);
            gameObject.SetActive(false);
        }
    }

    // Release possession of the AIController that it is in possession of, if it's the case
    public void ReleasePossession(Vector2 respawnPos, Vector2 respawnFacingDirection)
    {
        // Replace the character
        gameObject.transform.position = respawnPos;

        // Make the character face the correct way
        if (respawnFacingDirection == Vector2.left)
        {
            _spriteRenderer.flipX = !_flipSpawnedPlayer;
        }
        else if (respawnFacingDirection == Vector2.right)
        {
            _spriteRenderer.flipX = _flipSpawnedPlayer;
        }

        // Change the camera
        VirtualCameraManager.Instance.ChangeVirtualCamera(_virtualCamera);

        // Show the character
        gameObject.SetActive(true);
    }

    // Methods of the IPhysicsObjectCollisionListener interface
    public void OnPhysicsObjectCollisionEnter(PhysicsCollision2D collision)
    {
        if (InPossessionMode && collision.GameObject.tag == GameManager.EnemyTag)
        {
            TakePossession(collision.GameObject.GetComponent<AIController>());
        }
    }

    public void OnPhysicsObjectCollisionExit(PhysicsCollision2D collision) { }
}
