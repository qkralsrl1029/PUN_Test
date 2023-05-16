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

    private void Update()
    {
        if (state == State.None)
            VerticalInput = 0;
        else if (state == State.Up)
            VerticalInput = 1;
        else
            VerticalInput = -1;
    }

    public void OnMoveUpButtonPressed()
    {
        state = State.Up;
    }

    public void OnMoveUpButtonUp()
    {
        if (state == State.Up)
            state = State.None;
    }

    public void OnMoveDownButtonPressed()
    {
        state = State.Down;
    }

    public void OnMoveDownButtonUp()
    {
        if (state == State.Down)
            state = State.None;
    }
}
