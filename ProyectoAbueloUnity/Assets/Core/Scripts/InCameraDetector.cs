using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCameraDetector : MonoBehaviour
{
    [SerializeField] private Transform[] _checkPoints;

    private Camera _camera;
    private MeshRenderer _renderer;
    private Plane[] _cameraFrustrum;
    private Collider _collider;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            CheckIfItsOnCamera();
        }
    }

    private void CheckIfItsOnCamera()
    {
        var bounds = _collider.bounds;
        _cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(_camera);
        if (GeometryUtility.TestPlanesAABB(_cameraFrustrum, bounds))
        {
            ThrowRay();
        }
        else
        {
            _renderer.material.color = Color.green;
        }
    }

    private void ThrowRay()
    {
        foreach(Transform checkpoint in _checkPoints)
        {
            Vector3 direction = checkpoint.transform.position - _camera.transform.position;
            if(Physics.Raycast(_camera.transform.position, direction, out RaycastHit hit, Mathf.Infinity))
            {

                if(hit.transform.gameObject.Equals(gameObject))
                {
                    _renderer.material.color = Color.red;
                    break;
                }
                else
                {
                    _renderer.material.color = Color.green;
                }
            }
            else
            {
                _renderer.material.color = Color.green;
            }
        }
    }
}
