using TechnicalTest.Manager;
using UnityEngine;

namespace TechnicalTest
{
    public class Sphere : MonoBehaviour
    {
        public int sphereId;
        private SphereData sphereData;

        /// <summary>
        /// default material shown when scene started,
        /// additional options for player to choose, they can choose default too
        /// </summary>
        [Header("Material")]
        public Material DefaultMaterial;
        public Material[] AdditionalMaterials;
        
        /// <summary>
        /// Mesh and Icons need to be in same order, icons for representing the mesh in modification UI
        /// </summary>
        [Header("Mesh")]
        public Mesh DefaultMesh;
        public Sprite DefaultMeshIcon;
        public Mesh[] AdditionalMeshes;
        public Sprite[] AdditionalMeshIcons;
    
        //dependencies
        private UIPanel uiPanel;
        private Renderer _renderer;
        private MeshFilter _meshFilter;


        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _meshFilter = GetComponent<MeshFilter>();
        }

        private void Start()
        {
            uiPanel = UIPanel.Instance;
            uiPanel.OnMaterialOptionClicked += TriggerChangeMaterial;
            uiPanel.OnMeshOptionClicked += TriggerChangeMesh;

            InitDataOrLoadFromExists();

            ApplyMesh();
            ApplyMaterial();
        }

        /// <summary>
        /// Try get existing SphereData from SphereDataManager
        /// Init SphereData from component's field, if not found
        /// </summary>
        private void InitDataOrLoadFromExists()
        {
            var sphereStateManager = SphereDataManager.Instance;
            if (!sphereStateManager.SphereDataArray[sphereId].HasBuilt)
            {
                ShuffleAdditionalMaterialsAndMeshes();
                CompositeAndBuildSphereData();
                sphereStateManager.SphereDataArray[sphereId] = sphereData;
            }
            else
            {
                sphereData = sphereStateManager.SphereDataArray[sphereId];
            }
        }

        private void OnDestroy()
        {
            uiPanel.OnMaterialOptionClicked -= TriggerChangeMaterial;
            uiPanel.OnMeshOptionClicked -= TriggerChangeMesh;
        }
    
        private void ShuffleAdditionalMaterialsAndMeshes()
        {
            /*
         * Fisher-yiates shuffle on AdditionalMeshes[], icons and materials update accordingly
         */
            var random = new System.Random();
            for (int i = AdditionalMeshes.Length - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Material tempMaterial = AdditionalMaterials[i];
                AdditionalMaterials[i] = AdditionalMaterials[j];
                AdditionalMaterials[j] = tempMaterial;
            
                Mesh tempMesh = AdditionalMeshes[i];
                AdditionalMeshes[i] = AdditionalMeshes[j];
                AdditionalMeshes[j] = tempMesh;

                Sprite tempIcon = AdditionalMeshIcons[i];
                AdditionalMeshIcons[i] = AdditionalMeshIcons[j];
                AdditionalMeshIcons[j] = tempIcon;
            }
        }
        
        /// <summary>
        /// Composite arrays, first slot for default, others for additional options
        /// i.e. Default[1]{ A } + Additional[3]{ B,C,D } = Mesh[4]{ A,B,C,D }. 
        /// 
        /// composited array provides easier access to other system
        /// </summary>
        private void CompositeAndBuildSphereData()
        {
            Mesh[] _meshes = new Mesh[4];
            Sprite[] _meshIcons = new Sprite[4];
            Material[] _materials = new Material[4];
        
            int numMaterials = 1 + AdditionalMaterials.Length;
            _materials = new Material[numMaterials];
            _materials[0] = DefaultMaterial;
            for (int i = 0; i < AdditionalMaterials.Length; i++)
            {
                _materials[i + 1] = AdditionalMaterials[i];
            }

            int numMeshes = 1 + AdditionalMeshes.Length;
            _meshes = new Mesh[numMeshes];
            _meshes[0] = DefaultMesh;
            _meshIcons = new Sprite[numMeshes];
            _meshIcons[0] = DefaultMeshIcon;
            for (int i = 0; i < AdditionalMeshes.Length; i++)
            {
                _meshes[i + 1] = AdditionalMeshes[i];
                _meshIcons[i + 1] = AdditionalMeshIcons[i];
            }

            BuiltSphereData(sphereId, _meshes, _meshIcons, _materials);
        }

        private void BuiltSphereData(int id, Mesh[] meshes, Sprite[] icons, Material[] materials)
        {
            SphereData newSphereData = new SphereData
            {
                Id = id,
                MeshLibrary = meshes,
                MeshIconLibrary = icons,
                MaterialLibrary = materials,
                HasBuilt = true
            };
            sphereData = newSphereData;
        }
    
        private void ApplyMesh()
        {
            _meshFilter.mesh = sphereData.GetCurrentMesh();
        }

        private void ApplyMaterial()
        {
            _renderer.material = sphereData.GetCurrentMaterial();
        }
    
        public void TriggerChangeMesh(int observerId, int optionId)
        {
            if (observerId != sphereId)
                return;
            sphereData.currentMeshId = optionId;
            ApplyMesh();
        }

        public void TriggerChangeMaterial(int observerId, int optionId)
        {
            if (observerId != sphereId)
                return;
            sphereData.currentMaterialId = optionId;
            ApplyMaterial();
        }
    }
}
