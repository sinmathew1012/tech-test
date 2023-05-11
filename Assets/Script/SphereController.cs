using System;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;


public class SphereController : MonoBehaviour
{
    public int sphereIdentifier;
    public Material DefaultMaterial;
    public Material[] AdditionalMaterials;
    public Mesh DefaultMesh;
    public Mesh[] AdditionalMeshes;
    
    private SphereStateManager _sphereStateManager;
    private ModificationPanel modificationPanel;
    private Renderer _renderer;
    private MeshFilter _meshFilter;
    
    private Material[] materials;
    private Mesh[] meshes;
    
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
            _sphereStateManager.SetHasCompositeState(sphereIdentifier);
        }

        materials = _sphereStateManager.GetMaterialStates(sphereIdentifier);
        meshes = _sphereStateManager.GetMeshStates(sphereIdentifier);
        
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
        for (int i = 0; i < AdditionalMeshes.Length; i++)
        {
            meshes[i + 1] = AdditionalMeshes[i];
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
        // meshIndex = (meshIndex + 1) % meshes.Length;
        ApplyMaterialAndMesh();
        _sphereStateManager.SetMeshIndex(sphereIdentifier, meshIndex);
    }

    public void ChangeMaterial(int observerId, int optionId)
    {
        if (observerId != sphereIdentifier)
            return;
        materialIndex = optionId;
        // materialIndex = (materialIndex + 1) % materials.Length;
        ApplyMaterialAndMesh();
        _sphereStateManager.SetMaterialIndex(sphereIdentifier, materialIndex);
    }
}
