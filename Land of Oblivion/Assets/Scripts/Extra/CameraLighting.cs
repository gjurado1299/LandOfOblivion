using UnityEngine;

public class CameraLighting : MonoBehaviour
{
    [SerializeField] private Light _ignoreLight = null;

    private void OnPreCull(){
        EnableLight(false);
    }

    private void OnPreRender(){
        EnableLight(false);
    }

    private void OnPostRender(){
        EnableLight(true);
    }

    private void EnableLight(bool value){
        if(_ignoreLight != null){
            _ignoreLight.enabled = value;
        }
    }
}
