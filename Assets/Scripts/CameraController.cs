using UnityEngine;

public partial class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cameraToControl;
    [SerializeField] private Map map;

    [SerializeField] private float maxSizeCamera;
    [SerializeField] private float minSizeCamera;

    [SerializeField] private float speedSensZoom;
    [SerializeField] private float speed;

    private Vector3 startPos;
    private Vector3 endPos;

    private float halfheightCamera;
    private float halfwidthCamera;

    private Vector3 rayDirection = new Vector3(0, 0, 1);

    public string GetNameHit()
    {
        var leftTopBorderCamera = new Vector3(cameraToControl.transform.position.x - halfwidthCamera, cameraToControl.transform.position.y + halfheightCamera, cameraToControl.transform.position.z);
        var isHit = Physics2D.Raycast(leftTopBorderCamera, rayDirection);
        if (isHit)
            return isHit.collider.gameObject.GetComponent<SpriteRenderer>().sprite.name;
        return "";
    }

    private void Start()
    {
        SetCameraOnMapCenter();
        SetHalfCameraSize();
    }

    private void Update()
    {
        DragCamera();
        ZoomCamera();
    }

    private void SetCameraOnMapCenter()
    {
        cameraToControl.transform.position = new Vector3((map.LeftTopBorder.x + map.RightBottomBorder.x) / 2, (map.LeftTopBorder.y + map.RightBottomBorder.y) / 2, cameraToControl.transform.position.z);
    }

    private void DragCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = cameraToControl.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            endPos = cameraToControl.ScreenToWorldPoint(Input.mousePosition);
            var direction = endPos - startPos;
            cameraToControl.transform.position += direction * speed * Time.deltaTime;

            ClampedCamera();
        }
    }

    private void ZoomCamera()
    {
        var currentScroll = Input.mouseScrollDelta.y;
        if (currentScroll != 0)
        {
            cameraToControl.orthographicSize = cameraToControl.orthographicSize + currentScroll * speedSensZoom;
            cameraToControl.orthographicSize = Mathf.Clamp(cameraToControl.orthographicSize, minSizeCamera, maxSizeCamera);

            SetHalfCameraSize();
            ClampedCamera();
        }
    }

    private void ClampedCamera()
    {
        var clampedX = Mathf.Clamp(cameraToControl.transform.position.x, map.LeftTopBorder.x + halfwidthCamera, map.RightBottomBorder.x - halfwidthCamera);
        var clampedY = Mathf.Clamp(cameraToControl.transform.position.y, map.RightBottomBorder.y + halfheightCamera, map.LeftTopBorder.y - halfheightCamera);
        cameraToControl.transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    private void SetHalfCameraSize()
    {
        halfheightCamera = cameraToControl.orthographicSize;
        halfwidthCamera = halfheightCamera * cameraToControl.aspect;
    }
}
