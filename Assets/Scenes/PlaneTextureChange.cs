using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;


public class PlaneTextureChange : MonoBehaviour
{
    private SetUnitychan setUnitychan;

    private string imgPath; 

    private Texture2D captureTexture;

    private Texture2D revertTexture;

    private Vector3 preHitPosition;

    // Start is called before the first frame update
    private ARPlaneManager planeManager;

    [SerializeField] Material captureMaterial;
    void Start(){
        setUnitychan = GetComponent<SetUnitychan>();
        planeManager = GetComponent<ARPlaneManager>();
        imgPath = "unityChan.png";
    }

    // Update is called once per frame
    void Update()
    {
        if (setUnitychan.isUnitychan){
            if (setUnitychan.isHit){
                // CaptureScreenShot(imgPath);
                StartCoroutine ("AndroidCapture");
                setUnitychan.isHit = false;
            }
        }
    }

    private void TextureChange(){
        if(captureTexture == null){
            Debug.Log("Capture Texture not Found...");
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

    IEnumerator AndroidCapture()
    {
        var dataPath = UnityEngine.Application.persistentDataPath + "/" + imgPath;
        Debug.Log("data path " + dataPath);
        if (File.Exists(dataPath) == true)
		{
			// ファイル削除
			File.Delete(dataPath);
			while (File.Exists(dataPath) == true)
			{
				yield return null;
			}
		}
	    // スクリーンショットを撮る
	    ScreenCapture.CaptureScreenshot(imgPath);
		while (File.Exists(dataPath) == false)
		{
			yield return null;
		}
        Debug.Log("Capture Success!");
        byte[] bytes = File.ReadAllBytes(UnityEngine.Application.persistentDataPath + "/" + imgPath);
        captureTexture = new Texture2D(2, 2);
        captureTexture.LoadImage(bytes);

        TextureChange();
        captureMaterial.SetTexture("_MainTex", revertTexture);

        foreach (var plane in planeManager.trackables)
        {
            var unityChanPlane = plane.gameObject;
            Debug.Log("Plane : " + unityChanPlane);
            var material = unityChanPlane.GetComponent<MeshRenderer>();
            material.material = captureMaterial;
        }
    }
}
