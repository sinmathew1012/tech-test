using System;
using Script;
using UnityEngine;
using UnityEngine.UI;
public class ModificationPanel : MonoBehaviour
{
    public static ModificationPanel Instance { get; private set; }
    private SphereStateManager _sphereStateManager;
    public int CurrentSelectedId;
    public event Action<int, int> OnMaterialOptionClicked;
    public event Action<int, int> OnMeshOptionClicked;

    [SerializeField] private GameObject GO_ModificationPanel;

    [SerializeField] private Image[] Img_MaterialOptions = new Image[4];
    [SerializeField] private Image[] Img_MeshOptions = new Image[4];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UIStateManger.Instance.OnUIStateChanged += HandleUIStateChanged;
        UIStateManger.Instance.ChangeState(UIState.Idle);
    }
    private void OnDestroy()
    {
        UIStateManger.Instance.OnUIStateChanged -= HandleUIStateChanged;
    }

    private void HandleUIStateChanged(UIState newState)
    {
        if (newState == UIState.Idle)
        {
            GO_ModificationPanel.gameObject.SetActive(false);
        }
        else if (newState == UIState.Modifying)
        {
            GO_ModificationPanel.gameObject.SetActive(true);

        }
    }
    
    public void SelectSphere(SphereController sphereController)
    {
        CurrentSelectedId = sphereController.sphereIdentifier;
        for (int i = 0; i < Img_MaterialOptions.Length; i++)
        {
            Img_MaterialOptions[i].material = SphereStateManager.Instance.MaterialStates[CurrentSelectedId][i];
        }
        for (int i = 0; i < Img_MeshOptions.Length; i++)
        {
            Img_MeshOptions[i].sprite = SphereStateManager.Instance.MeshIconStates[CurrentSelectedId][i];
        }
    }
    
    public void OnClickMaterialOption(int optionId)
    {
        OnMaterialOptionClicked?.Invoke(CurrentSelectedId, optionId);
    }
    public void OnClickMeshOption(int optionId)
    {
        OnMeshOptionClicked?.Invoke(CurrentSelectedId, optionId);
    }

    public void OnClickGoBack()
    {
        UIStateManger.Instance.ChangeState(UIState.Idle);
        PlayerMovementController.Instance.TriggerMoveCamera(0);
    }
}
