using UnityEngine;

namespace TechnicalTest.Manager
{
    /// <summary>
    /// SphereData store artists settings for a single Sphere
    /// Also used to return current state of the Sphere
    /// SphereData shared with same id across all levels, access via SphereDataManager
    /// </summary>
    public class SphereData
    {
        public int Id;
        public Mesh[] MeshLibrary;
        public Sprite[] MeshIconLibrary;
        public Material[] MaterialLibrary;
        public bool HasBuilt; 

        public int currentMaterialId = 0;
        public int currentMeshId = 0;

        public Material GetCurrentMaterial()
        {
            return MaterialLibrary[currentMaterialId];
        }
        public Mesh GetCurrentMesh()
        {
            return MeshLibrary[currentMeshId];
        }
    }
}