using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cameraToControl;

    private Vector3 startPos;
    private Vector3 endPos;
    [SerializeField] private float speed;

    [SerializeField] private Vector2 leftBottom; //(-10;-10)
    [SerializeField] private Vector2 rightTop; //(10;10)

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = cameraToControl.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            endPos = cameraToControl.ScreenToWorldPoint(Input.mousePosition);
            var direction = endPos - startPos;
            cameraToControl.transform.position += direction * speed * Time.deltaTime; //(12;-12)
            var clampedX = Mathf.Clamp(cameraToControl.transform.position.x, leftBottom.x, rightTop.x);
            var clampedY = Mathf.Clamp(cameraToControl.transform.position.y, leftBottom.y, rightTop.y);
            cameraToControl.transform.position = new Vector2(clampedX, clampedY);
            //(-10;-10) (твоя позиция) //(10;10)
        }
    }
}
