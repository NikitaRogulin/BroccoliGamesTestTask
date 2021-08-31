using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text spriteNameText;
    [SerializeField] private CameraController cameraController;

    public void SetName()
    {
        spriteNameText.text = cameraController.GetNameHit();
    }
}

