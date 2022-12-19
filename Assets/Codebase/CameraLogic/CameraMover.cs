using System;
using DG.Tweening;
using UnityEngine;

namespace Codebase.CameraLogic
{
  public class CameraMover : MonoBehaviour
  {
    [SerializeField] private Camera _camera;
    
    private Vector3 _cameraStartPosition;
    private Quaternion _cameraStartRotation;

    private void Awake()
    {
      _cameraStartPosition = _camera.transform.position;
      _cameraStartRotation = _camera.transform.rotation;
    }


    public void MoveCameraForDrag()
    {
      _camera.transform.DORotate(new Vector3(90, -90, 0), 0.5f);
      _camera.transform.DOMove(new Vector3(0, 15, 0), 0.5f);
    }

    public void ResetCamera()
    {
      _camera.transform.DOMove(_cameraStartPosition, 0.5f);
      _camera.transform.DORotateQuaternion(_cameraStartRotation, 0.5f);
    }
  }
}