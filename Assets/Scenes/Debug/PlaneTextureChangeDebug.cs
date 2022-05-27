using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneTextureChangeDebug : MonoBehaviour
{
    private SetUnitychanDebug setUnitychan;

    private string imgPath; 

    private Texture2D captureTexture;

    private Texture2D revertTexture;

    private Vector3 preHitPosition;

    [SerializeField] GameObject unityChanPlane;
    // Start is called before the first frame update
    void Start(){
        setUnitychan = GetComponent<SetUnitychanDebug>();
        imgPath = "unityChan.png";
    }

    // Update is called once per frame
    void Update()
    {
        if (setUnitychan.isUnitychan){
            if (Vector3.Distance(preHitPosition, setUnitychan.hitPosition) > 0.01f){
                Debug.Log("Destination Change!");
                CaptureScreenShot(imgPath);
                Debug.Log(UnityEngine.Application.persistentDataPath);
                byte[] bytes = File.ReadAllBytes(ReplaceDelimiter(UnityEngine.Application.persistentDataPath + "/" + imgPath));
                captureTexture = new Texture2D(2, 2);
                captureTexture.LoadImage(bytes);

                // foreach (var plane in planeManager.trackables)
                // {
                //     plane.gameObject.SetActive(false);
                // }
                var material = unityChanPlane.GetComponent<MeshRenderer>();
                Debug.Log(material, revertTexture);
                TextureChange();
                material.material.SetTexture("_MainTex", revertTexture);
                Debug.Log(material.material.GetTexture("_MainTex"));
            }
        }
    }

    public string ReplaceDelimiter(string targetPath) {
        return targetPath.Replace("\\", "/");
    }
    private void CaptureScreenShot(string filePath)
    {
        ScreenCapture.CaptureScreenshot(UnityEngine.Application.persistentDataPath + "/" + filePath);
    }

    private void TextureChange(){
        if(captureTexture == null){
            return;
        }

        revertTexture = new Texture2D(captureTexture.width, captureTexture.height, captureTexture.format, captureTexture.mipmapCount == -1);
 
        for (var x = 0; x < captureTexture.width; x++)
        {
            for (var y = 0; y < captureTexture.height; y++)
            {
                var color = captureTexture.GetPixel(x, y);
                for (var i = 0; i < 3; i++)
                    color[i] = 1 - color[i];
 
                revertTexture.SetPixel(x, y, color);
            }
        }
 
        revertTexture.Apply();
    }
}
