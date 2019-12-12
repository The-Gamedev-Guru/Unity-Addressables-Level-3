using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SkyboxManager : MonoBehaviour
{
    [SerializeField] private List<AssetReference> _skyboxMaterials;

    private AsyncOperationHandle<Material>  _currentSkyboxMaterialOperationHandle;

    public void SetSkybox(int skyboxIndex)
    {
        StartCoroutine(SetSkyboxInternal(skyboxIndex));
    }

    private IEnumerator SetSkyboxInternal(int skyboxIndex)
    {
        if (_currentSkyboxMaterialOperationHandle.IsValid())
        {
            Addressables.Release(_currentSkyboxMaterialOperationHandle);
        }
        
        var skyboxMaterialReference = _skyboxMaterials[skyboxIndex];
        _currentSkyboxMaterialOperationHandle = skyboxMaterialReference.LoadAssetAsync<Material>();
        // You also have this option:
        // _currentSkyboxMaterialOperationHandle = Addressables.LoadAssetAsync<Material>("Skybox" + skyboxIndex);

        yield return _currentSkyboxMaterialOperationHandle;
        RenderSettings.skybox = _currentSkyboxMaterialOperationHandle.Result;
    }
}
