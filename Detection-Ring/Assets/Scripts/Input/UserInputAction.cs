// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/UserInputAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Plumbly
{
    public class @UserInputAction : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @UserInputAction()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""UserInputAction"",
    ""maps"": [
        {
            ""name"": ""PlayerMovement"",
            ""id"": ""5fac4650-91ec-46a8-825f-79d58dc86255"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b9df417c-9ec2-4406-800a-aa7631f53f1d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ef8fd634-c4fa-4910-abee-49eb4a530106"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""ab28f5a8-464c-4b23-a62b-9e61e41dfe9b"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e7c5f1f6-a8e6-4825-9cb7-916eda5273d5"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e84285d9-1832-4661-be3e-fff0fdfc7ed3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""44512942-b094-4598-a949-e6e754399236"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""767d3991-d858-42e4-b2c2-59b5961257b2"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1c64d515-f2c1-4309-b875-339081a696ad"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerActions"",
            ""id"": ""1ab55bce-16d2-4641-a40a-10cc26b400ae"",
            ""actions"": [
                {
                    ""name"": ""ToggleCamera"",
                    ""type"": ""Button"",
                    ""id"": ""cf201b71-7468-43fb-b41a-6a955b14eccd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UsePassiveItem1"",
                    ""type"": ""Button"",
                    ""id"": ""9c50140a-5440-4ce2-b357-a083e7a264dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire1"",
                    ""type"": ""Button"",
                    ""id"": ""43234343-9c5b-472b-8eef-83630d9f2795"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Activate"",
                    ""type"": ""Button"",
                    ""id"": ""e027c0c8-a585-4cce-9f4c-cac3f8a00dff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CycleHeldItemsForwards"",
                    ""type"": ""Button"",
                    ""id"": ""c2461d7c-fe3d-4d59-8b46-17160e9b7e61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""00bb0f0a-305f-4ca6-bf8c-b0b8c4f14c01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""f3111d75-e2e4-4273-954e-9d3f5ff69ef2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6cd8e94f-0011-45ec-ae7f-f2460246fe02"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46dd4829-65d8-40f4-85a3-6d8e1b3a8707"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UsePassiveItem1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""562612de-3340-45ff-9088-12655b2a373f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d77d69e-7502-472f-bc8a-6b765388b3cc"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CycleHeldItemsForwards"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3f2acaf0-2f51-4bc3-81ad-f91f6d58d935"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""536ef1dd-3318-4df0-b2d9-d0d9fe413162"",
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
                    ""id"": ""efd5d5b2-8d08-4d30-a287-e0a6f15742cb"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // PlayerMovement
            m_PlayerMovement = asset.FindActionMap("PlayerMovement", throwIfNotFound: true);
            m_PlayerMovement_Movement = m_PlayerMovement.FindAction("Movement", throwIfNotFound: true);
            m_PlayerMovement_Camera = m_PlayerMovement.FindAction("Camera", throwIfNotFound: true);
            // PlayerActions
            m_PlayerActions = asset.FindActionMap("PlayerActions", throwIfNotFound: true);
            m_PlayerActions_ToggleCamera = m_PlayerActions.FindAction("ToggleCamera", throwIfNotFound: true);
            m_PlayerActions_UsePassiveItem1 = m_PlayerActions.FindAction("UsePassiveItem1", throwIfNotFound: true);
            m_PlayerActions_Fire1 = m_PlayerActions.FindAction("Fire1", throwIfNotFound: true);
            m_PlayerActions_Activate = m_PlayerActions.FindAction("Activate", throwIfNotFound: true);
            m_PlayerActions_CycleHeldItemsForwards = m_PlayerActions.FindAction("CycleHeldItemsForwards", throwIfNotFound: true);
            m_PlayerActions_Jump = m_PlayerActions.FindAction("Jump", throwIfNotFound: true);
            m_PlayerActions_Aim = m_PlayerActions.FindAction("Aim", throwIfNotFound: true);
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

        // PlayerMovement
        private readonly InputActionMap m_PlayerMovement;
        private IPlayerMovementActions m_PlayerMovementActionsCallbackInterface;
        private readonly InputAction m_PlayerMovement_Movement;
        private readonly InputAction m_PlayerMovement_Camera;
        public struct PlayerMovementActions
        {
            private @UserInputAction m_Wrapper;
            public PlayerMovementActions(@UserInputAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_PlayerMovement_Movement;
            public InputAction @Camera => m_Wrapper.m_PlayerMovement_Camera;
            public InputActionMap Get() { return m_Wrapper.m_PlayerMovement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerMovementActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerMovementActions instance)
            {
                if (m_Wrapper.m_PlayerMovementActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnMovement;
                    @Camera.started -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                    @Camera.performed -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                    @Camera.canceled -= m_Wrapper.m_PlayerMovementActionsCallbackInterface.OnCamera;
                }
                m_Wrapper.m_PlayerMovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Camera.started += instance.OnCamera;
                    @Camera.performed += instance.OnCamera;
                    @Camera.canceled += instance.OnCamera;
                }
            }
        }
        public PlayerMovementActions @PlayerMovement => new PlayerMovementActions(this);

        // PlayerActions
        private readonly InputActionMap m_PlayerActions;
        private IPlayerActionsActions m_PlayerActionsActionsCallbackInterface;
        private readonly InputAction m_PlayerActions_ToggleCamera;
        private readonly InputAction m_PlayerActions_UsePassiveItem1;
        private readonly InputAction m_PlayerActions_Fire1;
        private readonly InputAction m_PlayerActions_Activate;
        private readonly InputAction m_PlayerActions_CycleHeldItemsForwards;
        private readonly InputAction m_PlayerActions_Jump;
        private readonly InputAction m_PlayerActions_Aim;
        public struct PlayerActionsActions
        {
            private @UserInputAction m_Wrapper;
            public PlayerActionsActions(@UserInputAction wrapper) { m_Wrapper = wrapper; }
            public InputAction @ToggleCamera => m_Wrapper.m_PlayerActions_ToggleCamera;
            public InputAction @UsePassiveItem1 => m_Wrapper.m_PlayerActions_UsePassiveItem1;
            public InputAction @Fire1 => m_Wrapper.m_PlayerActions_Fire1;
            public InputAction @Activate => m_Wrapper.m_PlayerActions_Activate;
            public InputAction @CycleHeldItemsForwards => m_Wrapper.m_PlayerActions_CycleHeldItemsForwards;
            public InputAction @Jump => m_Wrapper.m_PlayerActions_Jump;
            public InputAction @Aim => m_Wrapper.m_PlayerActions_Aim;
            public InputActionMap Get() { return m_Wrapper.m_PlayerActions; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerActionsActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActionsActions instance)
            {
                if (m_Wrapper.m_PlayerActionsActionsCallbackInterface != null)
                {
                    @ToggleCamera.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToggleCamera;
                    @ToggleCamera.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToggleCamera;
                    @ToggleCamera.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnToggleCamera;
                    @UsePassiveItem1.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUsePassiveItem1;
                    @UsePassiveItem1.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUsePassiveItem1;
                    @UsePassiveItem1.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnUsePassiveItem1;
                    @Fire1.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire1;
                    @Fire1.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire1;
                    @Fire1.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnFire1;
                    @Activate.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnActivate;
                    @Activate.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnActivate;
                    @Activate.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnActivate;
                    @CycleHeldItemsForwards.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCycleHeldItemsForwards;
                    @CycleHeldItemsForwards.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCycleHeldItemsForwards;
                    @CycleHeldItemsForwards.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnCycleHeldItemsForwards;
                    @Jump.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnJump;
                    @Aim.started -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAim;
                    @Aim.performed -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAim;
                    @Aim.canceled -= m_Wrapper.m_PlayerActionsActionsCallbackInterface.OnAim;
                }
                m_Wrapper.m_PlayerActionsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @ToggleCamera.started += instance.OnToggleCamera;
                    @ToggleCamera.performed += instance.OnToggleCamera;
                    @ToggleCamera.canceled += instance.OnToggleCamera;
                    @UsePassiveItem1.started += instance.OnUsePassiveItem1;
                    @UsePassiveItem1.performed += instance.OnUsePassiveItem1;
                    @UsePassiveItem1.canceled += instance.OnUsePassiveItem1;
                    @Fire1.started += instance.OnFire1;
                    @Fire1.performed += instance.OnFire1;
                    @Fire1.canceled += instance.OnFire1;
                    @Activate.started += instance.OnActivate;
                    @Activate.performed += instance.OnActivate;
                    @Activate.canceled += instance.OnActivate;
                    @CycleHeldItemsForwards.started += instance.OnCycleHeldItemsForwards;
                    @CycleHeldItemsForwards.performed += instance.OnCycleHeldItemsForwards;
                    @CycleHeldItemsForwards.canceled += instance.OnCycleHeldItemsForwards;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Aim.started += instance.OnAim;
                    @Aim.performed += instance.OnAim;
                    @Aim.canceled += instance.OnAim;
                }
            }
        }
        public PlayerActionsActions @PlayerActions => new PlayerActionsActions(this);
        public interface IPlayerMovementActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnCamera(InputAction.CallbackContext context);
        }
        public interface IPlayerActionsActions
        {
            void OnToggleCamera(InputAction.CallbackContext context);
            void OnUsePassiveItem1(InputAction.CallbackContext context);
            void OnFire1(InputAction.CallbackContext context);
            void OnActivate(InputAction.CallbackContext context);
            void OnCycleHeldItemsForwards(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnAim(InputAction.CallbackContext context);
        }
    }
}
