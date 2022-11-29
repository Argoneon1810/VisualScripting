using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager instance;
    public static InputManager Instance => instance;
    public event Action<Vector2> OnPointerDown, OnPointerUp;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1")) OnPointerDown?.Invoke(GetMousePosition());
        if(Input.GetButtonUp("Fire1")) OnPointerUp?.Invoke(GetMousePosition());
    }

    public static Vector2 GetMousePosition()
    {
        return Input.mousePosition;
    }
}