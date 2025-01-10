using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu( fileName = "InputReader", menuName = "ChainGame/Input Reader SO" )]
public class InputReader : ScriptableObject, GameInput.IGameplayActions
{
    // Left and right player events.
    public event UnityAction LeftPlayerRollEvent;
    public event UnityAction LeftPlayerSwapEvent;
    public event UnityAction RightPlayerRollEvent;
    public event UnityAction RightPlayerSwapEvent;

    // Global game events.
    public event UnityAction PauseEvent;

    private GameInput _gameInput;

    private void OnEnable()
    {
        // Creates an instance of the GameInput class and sets it's callbacks to this object. 
        if ( _gameInput == null )
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks( this );
        }

        EnableInput();
    }

    private void OnDisable()
    {
        DisableInput();
    }

    public void OnLeftPlayerRoll( InputAction.CallbackContext context )
    {
        if ( context.action.phase == InputActionPhase.Performed )
        {
            LeftPlayerRollEvent?.Invoke();
        }
    }

    public void OnLeftPlayerSwap( InputAction.CallbackContext context )
    {
        if ( context.action.phase == InputActionPhase.Performed )
        {
            LeftPlayerSwapEvent?.Invoke();
        }
    }

    public void OnRightPlayerRoll( InputAction.CallbackContext context )
    {
        if ( context.action.phase == InputActionPhase.Performed )
        {
            RightPlayerRollEvent?.Invoke();
        }
    }

    public void OnRightPlayerSwap( InputAction.CallbackContext context )
    {
        if ( context.action.phase != InputActionPhase.Performed )
        {
            RightPlayerSwapEvent?.Invoke();
        }
    }

    public void OnPause( InputAction.CallbackContext context )
    {
        if ( context.action.phase == InputActionPhase.Performed )
        {
            PauseEvent?.Invoke();
        }
    }

    public void EnableInput() => _gameInput.Gameplay.Enable();
    public void DisableInput() => _gameInput.Gameplay.Disable();
}
