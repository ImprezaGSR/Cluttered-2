using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCube : MonoBehaviour
{
    public float Speed;
    // Update is called once per frame
    void Update()
    {
        if (VirtualInputManager.Instance.Static)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0f,90f,0f);
        }
        if (VirtualInputManager.Instance.MoveLeft && VirtualInputManager.Instance.MoveRight)
        {
            return;
        }
        if (VirtualInputManager.Instance.MoveUp && VirtualInputManager.Instance.MoveDown)
        {
            return;
        }
        if (VirtualInputManager.Instance.MoveRight){
            this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
        if (VirtualInputManager.Instance.MoveLeft){
            this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Euler(0f,180f,0f);
        }
        if (VirtualInputManager.Instance.MoveUp){
            this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Euler(270f,0f,0f);
        }
        if (VirtualInputManager.Instance.MoveDown){
            this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Euler(90f,0f,0f);
        }
    }
}
