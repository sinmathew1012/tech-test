using System;
using Script;
using TMPro;
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
    [SerializeField] private TMP_Text[] Text_MaterialOptions = new TMP_Text[4];
    [SerializeField] private TMP_Text[] Text_MeshOptions = new TMP_Text[4];

    private void Awake()
    {
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
    }

    private void OnEnable()
    {
        UIStateManger.Instance.OnUIStateChanged += HandleUIStateChanged;
    }
    private void OnDisable()
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
    
    public void AssignSelection(SphereController sphereController)
    {
        Debug.Log(SphereStateManager.Instance);
        CurrentSelectedId = sphereController.sphereIdentifier;
        for (int i = 0; i < Text_MaterialOptions.Length; i++)
        {
            Text_MaterialOptions[i].text = SphereStateManager.Instance.MaterialStates[CurrentSelectedId][i].name;
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
    }
}