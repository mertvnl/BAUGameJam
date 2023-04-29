using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 InputXY { get; private set; }

    private const string HORIZONTAL_ID = "Horizontal";
    private const string VERTICAL_ID = "Vertical";

    private void Update()
    {
        GetInputs();
    }

    private void GetInputs()
    {
        InputXY = new Vector2(Input.GetAxisRaw(HORIZONTAL_ID), Input.GetAxisRaw(VERTICAL_ID));
    }
}
