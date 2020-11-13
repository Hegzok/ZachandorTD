// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputActions : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Land"",
            ""id"": ""cc0c043b-0da2-4dfd-82d5-66329201263b"",
            ""actions"": [
                {
                    ""name"": ""MoveVertical"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8bc90faa-4f91-4060-9ba0-11a5de958763"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveHorizontal"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f5879469-110b-4c71-9a57-e4ae2554da35"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""fadeaace-1ae5-4c12-a7da-d815535f63be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8b623650-ce22-403c-bc1c-89f7f0c7b9fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c7982661-d62c-446d-a071-dc15d26fefab"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Horizontal"",
                    ""id"": ""acc66251-3ef0-4fa1-93c4-bb2e682d06ca"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d654725f-5efb-4847-a51b-873451f05525"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""931c125a-3ae0-4265-a477-8cc3c40d9a49"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveHorizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""fe94d555-e73d-411c-9fce-f061543ebbc7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b53f7d5a-0c5a-4b77-8c9c-3f06d92ce02e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8aaf829b-525c-48cd-800d-0a81dc202e1f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Vertical"",
                    ""id"": ""d1a5ae39-e401-435a-b1e9-0474c0549942"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c5b4b906-4027-41c0-a48c-34b8aa9bcfb5"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""ac7f9ae2-5949-4a97-9157-ccb437dfb858"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveVertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Land
        m_Land = asset.FindActionMap("Land", throwIfNotFound: true);
        m_Land_MoveVertical = m_Land.FindAction("MoveVertical", throwIfNotFound: true);
        m_Land_MoveHorizontal = m_Land.FindAction("MoveHorizontal", throwIfNotFound: true);
        m_Land_Shoot = m_Land.FindAction("Shoot", throwIfNotFound: true);
        m_Land_RightClick = m_Land.FindAction("RightClick", throwIfNotFound: true);
        m_Land_MousePos = m_Land.FindAction("MousePos", throwIfNotFound: true);
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

    // Land
    private readonly InputActionMap m_Land;
    private ILandActions m_LandActionsCallbackInterface;
    private readonly InputAction m_Land_MoveVertical;
    private readonly InputAction m_Land_MoveHorizontal;
    private readonly InputAction m_Land_Shoot;
    private readonly InputAction m_Land_RightClick;
    private readonly InputAction m_Land_MousePos;
    public struct LandActions
    {
        private @PlayerInputActions m_Wrapper;
        public LandActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveVertical => m_Wrapper.m_Land_MoveVertical;
        public InputAction @MoveHorizontal => m_Wrapper.m_Land_MoveHorizontal;
        public InputAction @Shoot => m_Wrapper.m_Land_Shoot;
        public InputAction @RightClick => m_Wrapper.m_Land_RightClick;
        public InputAction @MousePos => m_Wrapper.m_Land_MousePos;
        public InputActionMap Get() { return m_Wrapper.m_Land; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LandActions set) { return set.Get(); }
        public void SetCallbacks(ILandActions instance)
        {
            if (m_Wrapper.m_LandActionsCallbackInterface != null)
            {
                @MoveVertical.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveVertical;
                @MoveVertical.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveVertical;
                @MoveHorizontal.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveHorizontal;
                @MoveHorizontal.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMoveHorizontal;
                @Shoot.started -= m_Wrapper.m_LandActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnShoot;
                @RightClick.started -= m_Wrapper.m_LandActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnRightClick;
                @MousePos.started -= m_Wrapper.m_LandActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_LandActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_LandActionsCallbackInterface.OnMousePos;
            }
            m_Wrapper.m_LandActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveVertical.started += instance.OnMoveVertical;
                @MoveVertical.performed += instance.OnMoveVertical;
                @MoveVertical.canceled += instance.OnMoveVertical;
                @MoveHorizontal.started += instance.OnMoveHorizontal;
                @MoveHorizontal.performed += instance.OnMoveHorizontal;
                @MoveHorizontal.canceled += instance.OnMoveHorizontal;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
            }
        }
    }
    public LandActions @Land => new LandActions(this);
    public interface ILandActions
    {
        void OnMoveVertical(InputAction.CallbackContext context);
        void OnMoveHorizontal(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
    }
}
