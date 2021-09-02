// GENERATED AUTOMATICALLY FROM 'Assets/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Avatar"",
            ""id"": ""d7903512-bd06-4227-9898-24c3e514dad6"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""abb17017-3089-454c-bc68-82a7c6fdea14"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""00c5bfd6-1912-4283-a338-596981e83f45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""dda159f7-6e93-4023-b43e-3697dec8ff54"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8c4b264d-b130-40e6-8236-d4d32ca1b012"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1e230e52-5a4f-4b6e-8715-7df93b683fcc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""661421ab-0e51-4d06-8954-b3c8caba949e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5445bfd8-2543-46eb-be08-1075ada57043"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2069a82-666d-4bb8-b454-8b21bf979109"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Avatar
        m_Avatar = asset.FindActionMap("Avatar", throwIfNotFound: true);
        m_Avatar_Move = m_Avatar.FindAction("Move", throwIfNotFound: true);
        m_Avatar_Jump = m_Avatar.FindAction("Jump", throwIfNotFound: true);
        m_Avatar_Quit = m_Avatar.FindAction("Quit", throwIfNotFound: true);
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

    // Avatar
    private readonly InputActionMap m_Avatar;
    private IAvatarActions m_AvatarActionsCallbackInterface;
    private readonly InputAction m_Avatar_Move;
    private readonly InputAction m_Avatar_Jump;
    private readonly InputAction m_Avatar_Quit;
    public struct AvatarActions
    {
        private @Controls m_Wrapper;
        public AvatarActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Avatar_Move;
        public InputAction @Jump => m_Wrapper.m_Avatar_Jump;
        public InputAction @Quit => m_Wrapper.m_Avatar_Quit;
        public InputActionMap Get() { return m_Wrapper.m_Avatar; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(AvatarActions set) { return set.Get(); }
        public void SetCallbacks(IAvatarActions instance)
        {
            if (m_Wrapper.m_AvatarActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_AvatarActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_AvatarActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_AvatarActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_AvatarActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_AvatarActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_AvatarActionsCallbackInterface.OnJump;
                @Quit.started -= m_Wrapper.m_AvatarActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_AvatarActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_AvatarActionsCallbackInterface.OnQuit;
            }
            m_Wrapper.m_AvatarActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
            }
        }
    }
    public AvatarActions @Avatar => new AvatarActions(this);
    public interface IAvatarActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
    }
}
