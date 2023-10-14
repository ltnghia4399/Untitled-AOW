using UnityEngine;

namespace BOC.Camera
{
    public class CameraController : MonoBehaviour
    {

        private static CameraController instance;

        [SerializeField] Transform cameraTransform;
        [SerializeField] float panSpeed = 1f;
        [SerializeField] float panTime = 5f;
        [SerializeField] Vector3 zoomAmount = new Vector3(0, 10, 10);
        [SerializeField] float panSideBorderThickness = 200f;
        [SerializeField] float panLeftBorderThickness = 600f;
        [SerializeField] float panRightBorderThickness = 1200f;
        [SerializeField] Vector2 panLimitVertical = new Vector2(-5f,10f);
        [SerializeField] Vector2 panLimitHorizontal = new Vector2(-1f,10f);

        [SerializeField] Vector2 dragLimit;
        [SerializeField] Vector3 zoomMin = new Vector3(1,40f, -24f);
        [SerializeField] Vector3 zoomMax = new Vector3(1,80f, -64f);

        [SerializeField] Vector3 dragStartPosition;
        [SerializeField] Vector3 dragCurrentPosition;

        [SerializeField] Vector3 rotateStartPosition;
        [SerializeField] Vector3 rotateCurrentPosition;

        Vector3 newPosition;
        Vector3 newZoom;
        Quaternion newRotation;

        public Transform followTaget;

        private UnityEngine.Camera cam;

        public static CameraController Instance { get => instance; set => instance = value; }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            cam = GetComponentInChildren<UnityEngine.Camera>();
        }

        private void Start()
        {
            newPosition = transform.position;
            newZoom = cameraTransform.localPosition;
            newRotation = transform.rotation;
        }

        private void Update()
        {
            
            if (followTaget != null)
            {
                newPosition = followTaget.position;
            }
            else
            {
                HandleMouseInput();
                HandleMovementInput();
            }
            
            HandleMouseRotation();
            HandleMouseZoomInput();
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                followTaget = null;
            }
        }

        private void HandleMouseInput()
        {

            if (Input.GetMouseButtonDown(0))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                float entry;

                if (plane.Raycast(ray, out entry))
                {
                    dragCurrentPosition = ray.GetPoint(entry);

                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }


            //change Comment old code : 29/05/2023 Start
            //print(Input.mousePosition.x);

            ////Zoom in
            //if (Input.mouseScrollDelta.y == 1 && newZoom.z <= zoomAmount.z)
            //{
            //    newZoom -= zoomAmount;
            //    //print(Input.mouseScrollDelta.y);
            //}

            ////Zoom out
            //if (Input.mouseScrollDelta.y == -1 && newZoom.z > zoomAmount.z)
            //{
            //    newZoom += zoomAmount;
            //    //print(Input.mouseScrollDelta.y);
            //}

            ////Pan up
            //if (Input.mousePosition.y >= Screen.height - panSideBorderThickness
            //    && Input.mousePosition.x <= panRightBorderThickness
            //    && Input.mousePosition.x >= panLeftBorderThickness)
            //{
            //    if (newPosition.z < panLimitVertical.y)
            //        newPosition += (Vector3.forward * panSpeed);
            //    //print(Input.mousePosition.y);
            //}

            ////Pan down
            //if (Input.mousePosition.y <= panSideBorderThickness
            //    && Input.mousePosition.x <= panRightBorderThickness
            //    && Input.mousePosition.x >= panLeftBorderThickness)
            //{
            //    if (newPosition.z > panLimitVertical.x)
            //        newPosition += (Vector3.forward * -panSpeed);
            //    //print(Input.mousePosition.y);
            //}

            //change  Comment old code : 29/05/2023 End

            ////Pan right
            //if (Input.mousePosition.x >= Screen.width - panSideBorderThickness
            //&& Input.mousePosition.y >= panBottomBorderThickness
            //&& Input.mousePosition.y <= panTopBorderThickness)
            //{
            //    if (newPosition.x < panLimitHorizontal.y)
            //        newPosition += (Vector3.right * panSpeed);
            //    //print(Input.mousePosition.y);
            //}

            ////Pan left
            //if (Input.mousePosition.x <= panSideBorderThickness
            //&& Input.mousePosition.y >= panBottomBorderThickness
            //&& Input.mousePosition.y <= panTopBorderThickness)
            //{
            //    if (newPosition.x > panLimitHorizontal.x)
            //        newPosition += (Vector3.right * -panSpeed);
            //    //print(Input.mousePosition.y);
            //}
        }

        private void HandleMouseZoomInput()
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
            }
        }

        private void HandleMouseRotation()
        {
            if (Input.GetMouseButtonDown(1))
            {
                rotateStartPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(1))
            {
                rotateCurrentPosition = Input.mousePosition;
                Vector3 difference = rotateStartPosition - rotateCurrentPosition;
                rotateStartPosition = rotateCurrentPosition;
                newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
            }
        }

        private void HandleMovementInput()
        {
            //Pan left
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (newPosition.z > panLimitHorizontal.x)
                    //newPosition += (transform.right * -panSpeed);
                    newPosition += (Vector3.forward * -panSpeed);
            }

            //Pan right
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (newPosition.z < panLimitHorizontal.y)
                    //newPosition += (transform.right * panSpeed);
                    newPosition += (Vector3.forward * panSpeed);
            }

            //Zoom In
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (newZoom.z > zoomAmount.z)
                    newZoom += zoomAmount;
            }

            //Zoom out
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (newZoom.z <= zoomAmount.z)
                    newZoom -= zoomAmount;
            }
        }

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * panTime);
            newPosition.x = Mathf.Clamp(newPosition.x, -dragLimit.x, dragLimit.x);
            newPosition.z = Mathf.Clamp(newPosition.z, -dragLimit.y, dragLimit.y);
    
            if (newZoom.y <= zoomMin.y && newZoom.z >= zoomMin.z)
            {
                newZoom = new Vector3(newZoom.x, zoomMin.y, zoomMin.z);
            }

            if (newZoom.y >= zoomMax.y && newZoom.z <= zoomMax.z)
            {
                newZoom = new Vector3(newZoom.x, zoomMax.y, zoomMax.z);
            }

            if (followTaget != null)
            {
                cameraTransform.LookAt(followTaget,Vector3.up);
            }

            cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * panTime);


            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * panTime);
        }
    }

}
