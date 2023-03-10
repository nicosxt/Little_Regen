//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem/InputActions.inputactions
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

public partial class @InputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""8fd54349-a36f-4462-82fa-97998e0505f1"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""0191281d-8675-4439-b6e4-df95c876ceb3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Click1"",
                    ""type"": ""Button"",
                    ""id"": ""13c6d66a-9974-4087-88b2-071ce8256456"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""638f79e9-87a4-4cdb-a301-a9d8caa0ac7d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21a2ecfa-8309-446b-8a4c-3071711518ff"",
                    ""path"": ""<Touchscreen>/touch0/tap"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bc34d912-05ca-4101-9df0-5511e5328349"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dee0a909-225a-467e-9f9b-ad4f5d43846d"",
                    ""path"": ""<Touchscreen>/touch0/tap"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlantObject"",
            ""bindingGroup"": ""PlantObject"",
            ""devices"": []
        }
    ]
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_Click = m_Default.FindAction("Click", throwIfNotFound: true);
        m_Default_Click1 = m_Default.FindAction("Click1", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_Click;
    private readonly InputAction m_Default_Click1;
    public struct DefaultActions
    {
        private @InputActions m_Wrapper;
        public DefaultActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Default_Click;
        public InputAction @Click1 => m_Wrapper.m_Default_Click1;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick;
                @Click1.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick1;
                @Click1.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick1;
                @Click1.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnClick1;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Click1.started += instance.OnClick1;
                @Click1.performed += instance.OnClick1;
                @Click1.canceled += instance.OnClick1;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    private int m_PlantObjectSchemeIndex = -1;
    public InputControlScheme PlantObjectScheme
    {
        get
        {
            if (m_PlantObjectSchemeIndex == -1) m_PlantObjectSchemeIndex = asset.FindControlSchemeIndex("PlantObject");
            return asset.controlSchemes[m_PlantObjectSchemeIndex];
        }
    }
    public interface IDefaultActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnClick1(InputAction.CallbackContext context);
    }
}
