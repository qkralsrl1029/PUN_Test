using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputButton : MonoBehaviour
{
    public static float VerticalInput;

    public enum State
    {
        None,
        Down,
        Up
    }

    private State state = State.None;

    public void OnMoveUpButtonPressed()
    {

    }

    public void OnMoveUpButtonUp()
    {

    }

    public void OnMoveDownButtonPressed()
    {

    }

    public void OnMoveDownButtonUp()
    {

    }
}
