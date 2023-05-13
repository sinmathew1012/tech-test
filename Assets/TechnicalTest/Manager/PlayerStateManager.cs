using System;

namespace TechnicalTest.Manager
{
    public enum PlayerState
    {
        Idle,
        Modifying
    }
    public static class PlayerStateManager
    {
        /// <summary>
        /// Player state change when:
        /// 1. player click on a sphere collider to start zoom in - Modify 
        /// 2. player unselect/close ui panel to resume to wide angle - Idle
        /// </summary>
        public static event Action<PlayerState> OnPlayerStateChanged;
        public static PlayerState CurrentState { get; private set; } = PlayerState.Idle;
        public static int CurrentSelectedSphereId = 0;

        public static void ChangeState(PlayerState newState)
        {
            CurrentState = newState;
            OnPlayerStateChanged?.Invoke(CurrentState);
        }
    }
}