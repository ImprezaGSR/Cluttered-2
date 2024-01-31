using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            VirtualInputManager.Instance.MoveRight = true;
            VirtualInputManager.Instance.Static = false;
        }else
        {
            VirtualInputManager.Instance.MoveRight = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            VirtualInputManager.Instance.MoveLeft = true;
            VirtualInputManager.Instance.Static = false;
        }else
        {
            VirtualInputManager.Instance.MoveLeft = false;
        }

        if (Input.GetKey(KeyCode.W))
        {
            VirtualInputManager.Instance.MoveUp = true;
            VirtualInputManager.Instance.Static = false;
        }else
        {
            VirtualInputManager.Instance.MoveUp = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            VirtualInputManager.Instance.MoveDown = true;
            VirtualInputManager.Instance.Static = false;
        }else
        {
            VirtualInputManager.Instance.MoveDown = false;
        }
        if (!Input.GetKey(KeyCode.A)&&!Input.GetKey(KeyCode.D)&&!Input.GetKey(KeyCode.W)&&!Input.GetKey(KeyCode.S))
        {
            VirtualInputManager.Instance.Static = true;
        }
    }
}
