using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    public GameObject cleanBackgroundPrefab;

	void Start() {
        CreateBackground();
	}
    
    private void CreateBackground() {
        float cameraWidth = Camera.main.GetComponent<CameraController>().GetWidth();
        float cameraHeight = Camera.main.GetComponent<CameraController>().GetHeight();
        
        Sprite sprite = cleanBackgroundPrefab.GetComponent<SpriteRenderer>().sprite;
        int gridSize;
        if (cameraWidth > cameraHeight) {
            gridSize = Mathf.CeilToInt(cameraWidth / sprite.bounds.size.x);
        } else {
            gridSize = Mathf.CeilToInt(cameraHeight / sprite.bounds.size.y);
        }
        if (gridSize % 2 == 0) gridSize++;
        
        for (int i = 0; i < gridSize; ++i) {
            for (int j = 0; j < gridSize; ++j) {
                Vector2 pos = new Vector2(-sprite.bounds.size.x * gridSize / 2f + sprite.bounds.size.x / 2f + i * sprite.bounds.size.x,
                                          -sprite.bounds.size.y * gridSize / 2f + sprite.bounds.size.y / 2f + j * sprite.bounds.size.y);
                Instantiate(cleanBackgroundPrefab, pos, Quaternion.identity);
            }
        }
    }
}
