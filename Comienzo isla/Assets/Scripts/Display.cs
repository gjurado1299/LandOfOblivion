using UnityEngine;
using UnityEngine.UI;

public class Display : MonoBehaviour
{

    [SerializeField] public Transform _displayAnchor;
    [SerializeField] public Camera _camera;
    [SerializeField] public RectTransform _display;
    [SerializeField] public Canvas _canvas;

    private Vector2 _textureDimensions;

    [SerializeField] private float _rotationSensitivity = 5f;
    [SerializeField] private float _minRotationX = -360f;
    [SerializeField] private float _maxRotationX = 360f;
    [SerializeField] private float _minRotationY = 0f;
    [SerializeField] private float _maxRotationY = 0f;
    private float _rotationX;
    private float _rotationY;
    private Quaternion _originalRotation;

    public Transform marker;

    // Start is called before the fi rst frame update
    void Start()
    {
        _textureDimensions = new Vector2(_display.GetComponent<RawImage>().texture.width,
                                         _display.GetComponent<RawImage>().texture.height);
        _originalRotation = _displayAnchor.GetChild(0).localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 scaledSizeDelta = _display.sizeDelta * _canvas.scaleFactor;
        Vector3 bottomLeftCorner = new Vector3(_display.position.x - (scaledSizeDelta.x / 2), _display.position.y - (scaledSizeDelta.y / 2));
        CastRay(_camera, bottomLeftCorner, scaledSizeDelta, _textureDimensions);*/
    }

    private RaycastHit CastRay(Camera camera, Vector3 bottomLeftCorner, Vector3 sizeDelta, Vector2 textureDimensions){

        Vector3 relativeMousePosition = Input.mousePosition - bottomLeftCorner;
        relativeMousePosition.x = (relativeMousePosition.x / sizeDelta.x) * textureDimensions.x;
        relativeMousePosition.y = (relativeMousePosition.y / sizeDelta.y) * textureDimensions.y;

        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(relativeMousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
            Debug.DrawLine(camera.transform.position, hit.point, Color.gray );
            marker.position = hit.point;
        }

        return hit;
    }

    private void RotatePivot(Transform pivot){
        _rotationX += Input.GetAxis("Mouse X") * _rotationSensitivity;
        _rotationY += Input.GetAxis("Mouse Y") * _rotationSensitivity;
        _rotationX = Mathf.Clamp(_rotationX, _minRotationX, _maxRotationX);
        _rotationY = Mathf.Clamp(_rotationY, _minRotationY, _maxRotationY);

        Quaternion xQuaternion = Quaternion.AngleAxis(_rotationX, Vector2.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(_rotationY, Vector2.left);

        pivot.localRotation = _originalRotation * xQuaternion * yQuaternion;
    }
}
