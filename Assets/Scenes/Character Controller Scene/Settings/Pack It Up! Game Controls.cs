//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
<<<<<<< HEAD
//     version 1.6.3
=======
//     version 1.5.1
>>>>>>> master
//     from Assets/Scenes/Character Controller Scene/Settings/Pack It Up! Game Controls.inputactions
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

public partial class @PackItUpGameControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PackItUpGameControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Pack It Up! Game Controls"",
    ""maps"": [
        {
            ""name"": ""Main Gameplay"",
            ""id"": ""24a845c7-ed4e-4b6a-acd3-f3f6670d367a"",
            ""actions"": [
                {
                    ""name"": ""Character Movement"",
                    ""type"": ""Value"",
                    ""id"": ""28c8c25a-bff2-4618-af4d-308abb8ab6ca"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""114c675d-da96-4d5f-80a6-2558811c93fa"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Character Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Key Composite"",
                    ""id"": ""410b70af-6d80-45e2-b7b0-48d2dd556d1e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Character Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""34cf77c1-7109-4569-ba57-6dbb3b95774e"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Character Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7b3c3252-05d8-4bb4-b63c-dff694bc155a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Character Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""58419e61-4466-48d7-9d70-ed84e2781b35"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Character Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""31be0ca2-9c96-4840-b519-2724dd64ab02"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""All"",
                    ""action"": ""Character Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""All"",
            ""bindingGroup"": ""All"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Main Gameplay
        m_MainGameplay = asset.FindActionMap("Main Gameplay", throwIfNotFound: true);
        m_MainGameplay_CharacterMovement = m_MainGameplay.FindAction("Character Movement", throwIfNotFound: true);
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

    // Main Gameplay
    private readonly InputActionMap m_MainGameplay;
    private List<IMainGameplayActions> m_MainGameplayActionsCallbackInterfaces = new List<IMainGameplayActions>();
    private readonly InputAction m_MainGameplay_CharacterMovement;
    public struct MainGameplayActions
    {
        private @PackItUpGameControls m_Wrapper;
        public MainGameplayActions(@PackItUpGameControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @CharacterMovement => m_Wrapper.m_MainGameplay_CharacterMovement;
        public InputActionMap Get() { return m_Wrapper.m_MainGameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainGameplayActions set) { return set.Get(); }
        public void AddCallbacks(IMainGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_MainGameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainGameplayActionsCallbackInterfaces.Add(instance);
            @CharacterMovement.started += instance.OnCharacterMovement;
            @CharacterMovement.performed += instance.OnCharacterMovement;
            @CharacterMovement.canceled += instance.OnCharacterMovement;
        }

        private void UnregisterCallbacks(IMainGameplayActions instance)
        {
            @CharacterMovement.started -= instance.OnCharacterMovement;
            @CharacterMovement.performed -= instance.OnCharacterMovement;
            @CharacterMovement.canceled -= instance.OnCharacterMovement;
        }

        public void RemoveCallbacks(IMainGameplayActions instance)
        {
            if (m_Wrapper.m_MainGameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_MainGameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainGameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainGameplayActions @MainGameplay => new MainGameplayActions(this);
    private int m_AllSchemeIndex = -1;
    public InputControlScheme AllScheme
    {
        get
        {
            if (m_AllSchemeIndex == -1) m_AllSchemeIndex = asset.FindControlSchemeIndex("All");
            return asset.controlSchemes[m_AllSchemeIndex];
        }
    }
    public interface IMainGameplayActions
    {
        void OnCharacterMovement(InputAction.CallbackContext context);
    }
}