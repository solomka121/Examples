using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SnowBrush : MonoBehaviour
{
    public CustomRenderTexture snowHeightMap;
    public Material heightMapUpdate;

    public float secondsToRestore;
    private float _timeToRestore;
    
    private Camera _mainCamera;

    private void Start()
    {
        snowHeightMap.Initialize();
        _mainCamera = Camera.main;
    }

    void Update()
    {
        _timeToRestore -= Time.deltaTime;
        if (_timeToRestore < 0)
        {
            heightMapUpdate.SetFloat( "_Restore" , (float)1 / 250);
            _timeToRestore = secondsToRestore / 250;
        }
        else
        {
            heightMapUpdate.SetFloat( "_Restore" , 0);
        }
        
        MousePaint();
    }

    public void MousePaint()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray , out RaycastHit hit))
            {
                Vector2 hitTextureCoord = hit.textureCoord;
                heightMapUpdate.SetVector("_DrawPosition", hitTextureCoord);
            }
        }
        else
        {
            heightMapUpdate.SetVector("_DrawPosition", new Vector4(-1 , -1 , 0 , 0));
        }
    }
}
