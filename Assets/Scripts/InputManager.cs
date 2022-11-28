using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager instance;
    public static InputManager Instance => instance;
    public event Action OnPointerDown, OnPointerUp;

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
        if(Input.GetButtonDown("Fire1")) OnPointerDown?.Invoke();
        if(Input.GetButtonUp("Fire1")) OnPointerUp?.Invoke();
    }
}