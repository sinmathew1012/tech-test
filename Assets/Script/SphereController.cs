using System;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


public class SphereController : MonoBehaviour
{
    public int sphereIdentifier;
    [Header("Material")]
    public Material DefaultMaterial;
    public Material[] AdditionalMaterials;
    [Header("Mesh")]
    public Mesh DefaultMesh;
    public Sprite DefaultMeshIcon;
    public Mesh[] AdditionalMeshes;
    public Sprite[] AdditionalMeshIcons;
    
    private SphereStateManager _sphereStateManager;
    private ModificationPanel modificationPanel;
    private Renderer _renderer;
    private MeshFilter _meshFilter;
    
    private Material[] materials;
    private Mesh[] meshes;
    private Sprite[] meshIcons;
    
    private int materialIndex = 0;
    private int meshIndex = 0;
    
    void Start()
    {
        _sphereStateManager = SphereStateManager.Instance;
        modificationPanel = ModificationPanel.Instance;
        _renderer = GetComponent<Renderer>();
        _meshFilter = GetComponent<MeshFilter>();
        if (!_sphereStateManager.GetHasCompositeState(sphereIdentifier))
        {
            CompositeMaterialsAndMeshes();
            _sphereStateManager.SetMaterialStates(sphereIdentifier, materials);
            _sphereStateManager.SetMeshStates(sphereIdentifier, meshes);
            _sphereStateManager.SetMeshIcons(sphereIdentifier, meshIcons);
            _sphereStateManager.SetHasCompositeState(sphereIdentifier);
        }
        else
        {
            materials = _sphereStateManager.GetMaterialStates(sphereIdentifier);
            meshes = _sphereStateManager.GetMeshStates(sphereIdentifier);
            meshIcons = _sphereStateManager.GetMeshIcons(sphereIdentifier);
        }

        materialIndex = _sphereStateManager.GetMaterialIndex(sphereIdentifier);
        meshIndex = _sphereStateManager.GetMeshIndex(sphereIdentifier);

        ApplyMaterialAndMesh();
        modificationPanel.OnMaterialOptionClicked += ChangeMaterial;
        modificationPanel.OnMeshOptionClicked += ChangeMesh;
        
    }

    private void OnDestroy()
    {
        modificationPanel.OnMaterialOptionClicked -= ChangeMaterial;
        modificationPanel.OnMeshOptionClicked -= ChangeMesh;
    }

    private void CompositeMaterialsAndMeshes()
    {
        int numMaterials = 1 + AdditionalMaterials.Length;
        materials = new Material[numMaterials];
        materials[0] = DefaultMaterial;
        for (int i = 0; i < AdditionalMaterials.Length; i++)
        {
            materials[i + 1] = AdditionalMaterials[i];
        }

        int numMeshes = 1 + AdditionalMeshes.Length;
        meshes = new Mesh[numMeshes];
        meshes[0] = DefaultMesh;
        meshIcons = new Sprite[numMeshes];
        meshIcons[0] = DefaultMeshIcon;
        for (int i = 0; i < AdditionalMeshes.Length; i++)
        {
            meshes[i + 1] = AdditionalMeshes[i];
            meshIcons[i + 1] = AdditionalMeshIcons[i];
        }
    }

    void ApplyMaterialAndMesh()
    {
        _renderer.material = materials[materialIndex];
        _meshFilter.mesh = meshes[meshIndex];
    }

    public void ChangeMesh(int observerId, int optionId)
    {
        if (observerId != sphereIdentifier)
            return;
        meshIndex = optionId;
        ApplyMaterialAndMesh();
        _sphereStateManager.SetMeshIndex(sphereIdentifier, meshIndex);
    }

    public void ChangeMaterial(int observerId, int optionId)
    {
        if (observerId != sphereIdentifier)
            return;
        materialIndex = optionId;
        ApplyMaterialAndMesh();
        _sphereStateManager.SetMaterialIndex(sphereIdentifier, materialIndex);
    }
}
