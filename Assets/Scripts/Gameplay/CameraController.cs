using UnityEngine;

public class CameraController : MonoBehaviour {
    public float requiredSize;

	void Start() {
		ResetCamera();
	}
	
	void Update() {
		ResetCamera();
    }
    
    private void ResetCamera()
    {
		Camera.main.orthographicSize = GetSize();
    }

    private float GetSize() {
        if (Screen.width >= Screen.height) {
            return requiredSize / 2f;
        } else {
            return requiredSize / 2f * Screen.height / Screen.width;
        }
    }

    public float GetWidth() {
        return GetHeight() * Camera.main.aspect;
    }

    public float GetHeight() {
        return 2f * GetSize();
    }
}
