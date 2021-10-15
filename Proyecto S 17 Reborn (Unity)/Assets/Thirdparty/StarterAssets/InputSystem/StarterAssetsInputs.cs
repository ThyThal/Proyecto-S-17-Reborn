using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[SerializeField] private InputActionReference _movementInput;
		[SerializeField] private InputActionReference _jumpInput;
		[SerializeField] private InputActionReference _attackInput;
		[SerializeField] private InputActionReference _aimInput;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool useSpellUtility;
		public bool useSpellDefensive;
		public bool useSpellOffensive;
		public bool attacking;
		public bool cancelSpell;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnSpellUtility(InputValue value)
		{
			SpellUtilityInput(value.isPressed);
		}
		public void OnSpellDefensive(InputValue value)
		{
			SpellDefensiveInput(value.isPressed);
		}
		public void OnSpellOffensive(InputValue value)
		{
			SpellOffensiveInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
        {
			AttackInput(value.isPressed);
        }

		public void OnSpellCancel(InputValue value)
        {
			SpellCancelInput(value.isPressed);
        }
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}
		public void SpellUtilityInput(bool newSpellState)
		{
			useSpellUtility = newSpellState;
		}
		public void SpellDefensiveInput(bool newSpellState)
		{
			useSpellDefensive = newSpellState;
		}
		public void SpellOffensiveInput(bool newSpellState)
		{
			useSpellOffensive = newSpellState;
		}

		public void AttackInput(bool newAttackState)
        {
			attacking = newAttackState;
        }

		public void SpellCancelInput(bool newSpellState)
        {
			cancelSpell = true;
        }

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		public void DisablePlayerActions()
		{
			_attackInput.action.Disable();
			_movementInput.action.Disable();
			_jumpInput.action.Disable();
			_aimInput.action.Disable();
		}

		public void EnablePlayerActions()
		{
			_attackInput.action.Enable();
			//_movementInput.action.Enable();
			//_jumpInput.action.Enable();
			_aimInput.action.Enable();
		}

		public void DisableMovement()
        {
			_movementInput.action.Disable();
		}

		public void FinishedShieldSpell()
        {
			_attackInput.action.Enable();
			_movementInput.action.Enable();
			_jumpInput.action.Enable();
			_aimInput.action.Enable();
		}
#endif
	}
}

