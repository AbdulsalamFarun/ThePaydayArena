using UnityEngine;
using UnityEngine.InputSystem;

public class UIShowSequence : MonoBehaviour
{
    public GameObject controllerUI;
    public GameObject keyboardMouseUI;

    private int step = 0;

    private void Start()
    {
        controllerUI.SetActive(true);
        keyboardMouseUI.SetActive(false);

        Time.timeScale = 0f; // يوقف اللعبة أول ما تشتغل

    }

    private void Update()
    {
        if (IsAnyInputPressed())
        {
            if (step == 0)
            {
                keyboardMouseUI.SetActive(true);
                controllerUI.SetActive(false);
                step = 1;
            }
            else if (step == 1)
            {
                controllerUI.SetActive(false);
                keyboardMouseUI.SetActive(false);
                step = 2;

                Time.timeScale = 1f; // يرجّع اللعبة تشتغل

            }
        }
    }

    private bool IsAnyInputPressed()
    {
        return (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            || (Mouse.current != null && (Mouse.current.leftButton.wasPressedThisFrame || Mouse.current.rightButton.wasPressedThisFrame))
            || IsAnyGamepadButtonPressed();
    }

    private bool IsAnyGamepadButtonPressed()
    {
        if (Gamepad.current == null)
            return false;

        var g = Gamepad.current;

        return g.buttonSouth.wasPressedThisFrame || g.buttonNorth.wasPressedThisFrame ||
               g.buttonEast.wasPressedThisFrame || g.buttonWest.wasPressedThisFrame ||
               g.startButton.wasPressedThisFrame || g.selectButton.wasPressedThisFrame ||
               g.leftShoulder.wasPressedThisFrame || g.rightShoulder.wasPressedThisFrame ||
               g.leftTrigger.wasPressedThisFrame || g.rightTrigger.wasPressedThisFrame ||
               g.leftStickButton.wasPressedThisFrame || g.rightStickButton.wasPressedThisFrame ||
               g.dpad.up.wasPressedThisFrame || g.dpad.down.wasPressedThisFrame ||
               g.dpad.left.wasPressedThisFrame || g.dpad.right.wasPressedThisFrame;
    }
}
