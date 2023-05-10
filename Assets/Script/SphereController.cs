using System;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


public class SphereController : MonoBehaviour
{
    public int sphereIdentifier;
    public Material DefaultMaterial;
    public Material[] AdditionalMaterials;
    public Mesh DefaultMesh;
    public Mesh[] AdditionalMeshes;
    
    private SphereStateManager _sphereStateManager; 
    private Renderer _renderer;
    private MeshFilter _meshFilter;
    
    private Material[] materials;
    private Mesh[] meshes;
    
    private int materialIndex = 0;
    private int meshIndex = 0;
    
    void Start()
    {
        _sphereStateManager = SphereStateManager.Instance;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            materialIndex = (materialIndex + 1) % materials.Length;
            ApplyMaterialAndMesh();
            _sphereStateManager.SetMaterialIndex(sphereIdentifier, materialIndex);
        }
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            meshIndex = (meshIndex + 1) % meshes.Length;
            ApplyMaterialAndMesh();
            _sphereStateManager.SetMeshIndex(sphereIdentifier, meshIndex);
        }
    }

    void ApplyMaterialAndMesh()
    {
        _renderer.material = materials[materialIndex];
        _meshFilter.mesh = meshes[meshIndex];
    }
}
