using UnityEngine;
using System.Collections;
using System;

public class CamScript : MonoBehaviour
{
	[SerializeField]
	RenderTexture survillanceCameraTexture;  
	[SerializeField]
	Camera survillanceCamera; 
	

	
	public IEnumerator SaveCameraView()
	{
		yield return new WaitForEndOfFrame();
		
		
		RenderTexture rendText = RenderTexture.active;
		RenderTexture.active = survillanceCamera.targetTexture;
		
		
		survillanceCamera.Render();
		
		
		Texture2D cameraImage = new Texture2D(survillanceCamera.targetTexture.width, survillanceCamera.targetTexture.height, TextureFormat.RGB24, false);
		cameraImage.ReadPixels(new Rect(0, 0, survillanceCamera.targetTexture.width, survillanceCamera.targetTexture.height), 0, 0);
		cameraImage.Apply();
		RenderTexture.active = rendText;
		
		byte[] bytes = cameraImage.EncodeToPNG();
		
		System.IO.File.WriteAllBytes(Application.persistentDataPath + "/camera_image.png", bytes);
	}
}