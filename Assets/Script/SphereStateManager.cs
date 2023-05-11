using UnityEngine;

public class SphereStateManager : MonoBehaviour
{
    public static SphereStateManager Instance { get; private set; }

    [HideInInspector] public Mesh[][] MeshStates = new Mesh[4][];
    [HideInInspector] public Material[][] MaterialStates = new Material[4][];
    [HideInInspector] public bool[] HasCompositeStates = new bool[4];
    private int[] materialIndices = new int[4];
    private int[] meshIndices = new int[4];
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

        for (int i = 0; i < 4; i++)
        {
            MeshStates[i] = new Mesh[0];
            MaterialStates[i] = new Material[0];
            HasCompositeStates[i] = false;
        }
    }

    public void SetMeshStates(int sphereIdentifier, Mesh[] meshes)
    {
        MeshStates[sphereIdentifier] = meshes;
    }

    public Mesh[] GetMeshStates(int sphereIdentifier)
    {
        return MeshStates[sphereIdentifier];
    }

    public void SetMaterialStates(int sphereIdentifier, Material[] materials)
    {
        MaterialStates[sphereIdentifier] = materials;
    }

    public Material[] GetMaterialStates(int sphereIdentiifer)
    {
        return MaterialStates[sphereIdentiifer];
    }
    
    public void SetHasCompositeState(int sphereIdentifier)
    {
        HasCompositeStates[sphereIdentifier] = true;
    }

    public bool GetHasCompositeState(int sphereIdentifier)
    {
        return HasCompositeStates[sphereIdentifier];
    }
    
    public int GetMaterialIndex(int sphereIdentifier)
    {
        return materialIndices[sphereIdentifier];
    }
    
    public void SetMaterialIndex(int sphereIdentifier, int index)
    {
        materialIndices[sphereIdentifier] = index;
    }
    
    public int GetMeshIndex(int sphereIdentifier)
    {
        return meshIndices[sphereIdentifier];
    }
    
    public void SetMeshIndex(int sphereIdentifier, int index)
    {
        meshIndices[sphereIdentifier] = index;
    }
}