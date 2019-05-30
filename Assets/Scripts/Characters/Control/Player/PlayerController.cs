﻿using UnityEngine;

// This script requires thoses components and will be added if they aren't already there
[RequireComponent(typeof(PlatformerMovement))]
[RequireComponent(typeof(Possession))]

public class PlayerController : CharacterController
{
    private PlatformerMovement _movementScript;
    private Possession _possession;

    private void Awake()
    {
        _movementScript = GetComponent<PlatformerMovement>();
        _possession = GetComponent<Possession>();
    }

    private void Update()
    {
        // Only update when time isn't stop
        if (Time.deltaTime > .0f && ControlsEnabled())
        {
            // Get the inputs used during this frame
            Inputs inputs = FetchInputs();

            UpdateMovement(inputs);
            UpdatePossession(inputs);
        }
    }

    protected override Inputs FetchInputs()
    {
        Inputs inputs = new Inputs();

        if (UseKeyboard)
        {
            // Inputs from the keyboard
            inputs.Vertical = Input.GetAxisRaw("Vertical");
            inputs.Horizontal = Input.GetAxisRaw("Horizontal");
            inputs.Jump = Input.GetButtonDown("Jump");
            inputs.ReleaseJump = Input.GetButtonUp("Jump");
            inputs.Dash = Input.GetButtonDown("Dash");
            inputs.ReleaseDash = Input.GetButtonUp("Dash");
            inputs.Possess = Input.GetButtonDown("Possess");
        }
        else
        {
            // TODO: Create inputs specific to the controler
            // Inputs from the controler
            inputs.Vertical = Input.GetAxisRaw("Vertical");
            inputs.Horizontal = Input.GetAxisRaw("Horizontal");
            inputs.Jump = Input.GetButtonDown("Jump");
            inputs.ReleaseJump = Input.GetButtonUp("Jump");
            inputs.Dash = Input.GetButtonDown("Dash");
            inputs.ReleaseDash = Input.GetButtonUp("Dash");
            inputs.Possess = Input.GetButtonDown("Possess");
        }

        return inputs;
    }

    protected override void UpdateMovement(Inputs inputs)
    {
        _movementScript.SetInputs(inputs);
    }

    protected override void UpdatePossession(Inputs inputs)
    {
        if (inputs.Possess)
        {
            _possession.ChangePossessionMode(!_possession.InPossessionMode);
        }
    }
}
