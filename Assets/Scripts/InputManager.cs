using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager instance;
    public static InputManager Instance
    {
        get => instance;
        set {
            if (instance != null)
                Destroy(value.gameObject);
            else
            {
                instance = value;
                DontDestroyOnLoad(value.gameObject);
            }
        }
    }
    public event Action<Vector2> OnPointerDown, OnPointerUp;

    private void Awake()
    {
        Instance = this;
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