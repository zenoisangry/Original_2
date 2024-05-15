using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static PlayerControls _playercontrols;
    public static PlayerControls Playercontrols
    {
        get
        {
            if (_playercontrols != null)
                return _playercontrols;
            _playercontrols = new PlayerControls();
            return _playercontrols;
        }
    }
    public static Vector3 MovementInput
    {
        get
        {
            Vector2 input = Playercontrols.Player.Movement.ReadValue<Vector2>();  // (X, Y) => (X, Z)
            return new Vector3(input.x, 0, input.y).normalized;
        }
    }
}
