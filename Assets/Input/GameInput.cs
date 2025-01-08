//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Input/GameInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""911c51fd-925f-4993-9934-798e1553c336"",
            ""actions"": [
                {
                    ""name"": ""LeftPlayerRoll"",
                    ""type"": ""Button"",
                    ""id"": ""e9c10914-e2b5-40eb-b3e8-6e916f3f7ef1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftPlayerSwap"",
                    ""type"": ""Button"",
                    ""id"": ""153bbe37-10cd-4696-941c-c2e158f1cbcb"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightPlayerRoll"",
                    ""type"": ""Button"",
                    ""id"": ""57d97b43-af09-40fa-9714-f7d8b5a3b798"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightPlayerSwap"",
                    ""type"": ""Button"",
                    ""id"": ""9376f2bf-2d9f-4f3f-a791-68b71df35eb1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""878a0cfb-13b1-43cc-93ea-5c330a38798b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""21045c30-7f8d-4bbe-ab2d-e00c046493c1"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard + Mouse"",
                    ""action"": ""LeftPlayerRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e0ab3e8c-c4c8-46a8-aa9e-78c507b65d23"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard + Mouse"",
                    ""action"": ""LeftPlayerSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""110d3108-5f99-4b1b-873b-e1c18d217f47"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard + Mouse"",
                    ""action"": ""RightPlayerRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3aef9c09-7a07-4618-b690-9bb28932ff08"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard + Mouse"",
                    ""action"": ""RightPlayerSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bde5b887-5100-415b-9f97-28059f4d806b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard + Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_LeftPlayerRoll = m_Gameplay.FindAction("LeftPlayerRoll", throwIfNotFound: true);
        m_Gameplay_LeftPlayerSwap = m_Gameplay.FindAction("LeftPlayerSwap", throwIfNotFound: true);
        m_Gameplay_RightPlayerRoll = m_Gameplay.FindAction("RightPlayerRoll", throwIfNotFound: true);
        m_Gameplay_RightPlayerSwap = m_Gameplay.FindAction("RightPlayerSwap", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
    }

    ~@GameInput()
    {
        UnityEngine.Debug.Assert(!m_Gameplay.enabled, "This will cause a leak and performance issues, GameInput.Gameplay.Disable() has not been called.");
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_LeftPlayerRoll;
    private readonly InputAction m_Gameplay_LeftPlayerSwap;
    private readonly InputAction m_Gameplay_RightPlayerRoll;
    private readonly InputAction m_Gameplay_RightPlayerSwap;
    private readonly InputAction m_Gameplay_Pause;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftPlayerRoll => m_Wrapper.m_Gameplay_LeftPlayerRoll;
        public InputAction @LeftPlayerSwap => m_Wrapper.m_Gameplay_LeftPlayerSwap;
        public InputAction @RightPlayerRoll => m_Wrapper.m_Gameplay_RightPlayerRoll;
        public InputAction @RightPlayerSwap => m_Wrapper.m_Gameplay_RightPlayerSwap;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @LeftPlayerRoll.started += instance.OnLeftPlayerRoll;
            @LeftPlayerRoll.performed += instance.OnLeftPlayerRoll;
            @LeftPlayerRoll.canceled += instance.OnLeftPlayerRoll;
            @LeftPlayerSwap.started += instance.OnLeftPlayerSwap;
            @LeftPlayerSwap.performed += instance.OnLeftPlayerSwap;
            @LeftPlayerSwap.canceled += instance.OnLeftPlayerSwap;
            @RightPlayerRoll.started += instance.OnRightPlayerRoll;
            @RightPlayerRoll.performed += instance.OnRightPlayerRoll;
            @RightPlayerRoll.canceled += instance.OnRightPlayerRoll;
            @RightPlayerSwap.started += instance.OnRightPlayerSwap;
            @RightPlayerSwap.performed += instance.OnRightPlayerSwap;
            @RightPlayerSwap.canceled += instance.OnRightPlayerSwap;
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @LeftPlayerRoll.started -= instance.OnLeftPlayerRoll;
            @LeftPlayerRoll.performed -= instance.OnLeftPlayerRoll;
            @LeftPlayerRoll.canceled -= instance.OnLeftPlayerRoll;
            @LeftPlayerSwap.started -= instance.OnLeftPlayerSwap;
            @LeftPlayerSwap.performed -= instance.OnLeftPlayerSwap;
            @LeftPlayerSwap.canceled -= instance.OnLeftPlayerSwap;
            @RightPlayerRoll.started -= instance.OnRightPlayerRoll;
            @RightPlayerRoll.performed -= instance.OnRightPlayerRoll;
            @RightPlayerRoll.canceled -= instance.OnRightPlayerRoll;
            @RightPlayerSwap.started -= instance.OnRightPlayerSwap;
            @RightPlayerSwap.performed -= instance.OnRightPlayerSwap;
            @RightPlayerSwap.canceled -= instance.OnRightPlayerSwap;
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnLeftPlayerRoll(InputAction.CallbackContext context);
        void OnLeftPlayerSwap(InputAction.CallbackContext context);
        void OnRightPlayerRoll(InputAction.CallbackContext context);
        void OnRightPlayerSwap(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}