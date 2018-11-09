﻿using UnityEngine;

// This script requires thoses components and will be added if they aren't already there
[RequireComponent(typeof(PlayerMovement))]

public class PlayerControl : MonoBehaviour
{
    [Header("Controls")]
    [SerializeField]
    private bool m_useKeyboard;

    private Inputs noControlInputs;
    
    public bool ControlsEnabled { get; private set; }

    private PlayerMovement m_playerMovement;

    private void Awake()
    {
        noControlInputs = new Inputs();
        ControlsEnabled = true;

        m_playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        // Only update when time isn't stop
        if (Time.deltaTime > .0f)
        {
            // Get the inputs used during this frame
            Inputs inputs = FetchInputs();

            if (ControlsCharacter())
            {
                UpdateMovement(inputs);
            }
            else
            {
                UpdateMovement(noControlInputs);
            }
        }
    }
    
    private Inputs FetchInputs()
    {
        Inputs inputs = new Inputs();

        if (m_useKeyboard)
        {
            // Inputs from the keyboard
            inputs.vertical = Input.GetAxisRaw("Vertical");
            inputs.horizontal = Input.GetAxisRaw("Horizontal");
            inputs.jump = Input.GetButtonDown("Jump");
            inputs.releaseJump = Input.GetButtonUp("Jump");
        }
        else
        {
            // TODO: Create inputs specific to the controler
            // Inputs from the controler
            inputs.vertical = Input.GetAxisRaw("Vertical");
            inputs.horizontal = Input.GetAxisRaw("Horizontal");
            inputs.jump = Input.GetButtonDown("Jump");
            inputs.releaseJump = Input.GetButtonUp("Jump");
        }

        return inputs;
    }

    public void SetKeyboardUse(bool useKeyboard)
    {
        m_useKeyboard = useKeyboard;
    }

    private bool ControlsCharacter()
    {
        return ControlsEnabled;
    }

    private void UpdateMovement(Inputs inputs)
    {
        m_playerMovement.SetInputs(inputs);
    }

    public void EnableControl(bool enable)
    {
        ControlsEnabled = enable;
    }
}
