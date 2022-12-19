using System;
using Codebase.BattleManager;
using Codebase.CameraLogic;
using DG.Tweening;
using UnityEngine;

namespace Codebase.Actions
{
  public class ActionDragger : MonoBehaviour
  {
    private Vector3 _startDragPoint;
    private Vector3 _cameraStartPosition;
    private Quaternion _cameraStartRotation;
    private Vector3 _mouseOffset;
    private float _mouseZCoordinate;

    private bool _isEnemy;
    private bool _isPlayer;

    private Collider _enemy;
    private Collider _player;
    
    private IActionAttack _currentActionAttack;
    private EnemyTurn _enemyTurn;
    private Camera _camera;
    private CameraMover _cameraMover;

    
    private void Awake()
    {
      _camera = Camera.main;
      _cameraMover = _camera.GetComponent<CameraMover>();
      _enemyTurn = FindObjectOfType<EnemyTurn>();
    }

    private void Start()
    {
      _startDragPoint = transform.position;
      _cameraStartPosition = _camera.transform.position;
      _cameraStartRotation = _camera.transform.rotation;
      _enemyTurn.EnemyTurnStarted += DestroyAction;
    }

    private void OnMouseDown()
    {
       _mouseZCoordinate = _camera.WorldToScreenPoint(gameObject.transform.position).z;
       _mouseOffset = transform.position - GetMouseAsWorldPoint();
    }

    private void OnMouseDrag()
    {
      _cameraMover.MoveCameraForDrag();
      Cursor.visible = false;
      transform.position = GetMouseAsWorldPoint() + _mouseOffset + new Vector3(0,1,0);
    }

    

    private Vector3 GetMouseAsWorldPoint()
    {
      Vector3 mousePoint = Input.mousePosition;
      
      mousePoint.z = _mouseZCoordinate;
      
      return _camera.ScreenToWorldPoint(mousePoint);
      
    }

    private void OnMouseUp()
    {
      Cursor.visible = true;
      
      if (_isEnemy && CompareTag("AttackAction"))
      {
        GetComponent<IActionAttack>().MakeAction(_enemy.transform);
        NonHighlightUnit(_enemy);
        DestroyAction();
      }
      else if (_isPlayer && CompareTag("DefenceAction"))
      {
        GetComponent<IActionDefence>().MakeAction(_player.transform);
        NonHighlightUnit(_player);
        DestroyAction();
      }
      
      _cameraMover.ResetCamera();
      gameObject.transform.position = _startDragPoint;
    }
    

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Enemy") && CompareTag("AttackAction"))
      {
        HighlightUnit(other);
        _isEnemy = true;
        _enemy = other;
      } 
        
      else if (other.CompareTag("Player") && CompareTag("DefenceAction"))
      {
        HighlightUnit(other);
        _isPlayer = true;
        _player = other;
      }
    }

    private static void HighlightUnit(Collider other) => 
      other.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);

    private void OnTriggerExit(Collider other)
    {
      if (other.CompareTag("Enemy") && CompareTag("AttackAction"))
      {
        NonHighlightUnit(other);
        _isEnemy = false;
        _enemy = null;
      } 
        
      else if (other.CompareTag("Player") && CompareTag("DefenceAction"))
      {
        NonHighlightUnit(other);
        _isPlayer = false;
        _player = null;
      }
        
    }

    private static void NonHighlightUnit(Collider other) => 
      other.transform.DOScale(new Vector3(1, 1, 1), 0.5f);

    private void DestroyAction() => 
      Destroy(gameObject);

    private void OnDestroy() => 
      _enemyTurn.EnemyTurnStarted -= DestroyAction;
  }
}