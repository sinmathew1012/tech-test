using System;
using UnityEngine;

public enum UIState
{
    Idle,
    Modifying
}
public class UIStateManger : MonoBehaviour
{
    public static UIStateManger Instance { get; private set; }
    public event Action<UIState> OnUIStateChanged;
    private UIState currentState = UIState.Idle;

    public UIState CurrentState
    {
        get { return CurrentState; }
    }

    private void Awake()
    {
        #region singleton logic
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        #endregion
    }
    
    public void Start()
    {
        // ChangeState(UIState.Idle);
    }

    public void ChangeState(UIState newState)
    {
        currentState = newState;
        OnUIStateChanged?.Invoke(currentState);
    }
}
