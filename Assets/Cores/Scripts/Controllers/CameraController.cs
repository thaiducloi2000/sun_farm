using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    private Camera currentCamera;
    private bool _isClicking;
    // private bool _isFirstClick = false;
    private Vector3 _clickPosition;

    private Camera Camera
    {
        get
        {
            if (currentCamera == null)
            {
                currentCamera = Camera.main;
            }

            return currentCamera;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit(Input.mousePosition);
        }
    }

    private void RaycastHit(Vector2 mousePosition)
    {
        // if (Vector3.Distance(mousePosition, _clickPosition) > 0.5f) return;
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(mousePosition);
#if UNITY_EDITOR
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
#endif
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            IInteract objectHit = hit.collider.gameObject.GetComponent<IInteract>();
            if (objectHit != null)
            {
                objectHit.Interact();
            }
        }
    }
}