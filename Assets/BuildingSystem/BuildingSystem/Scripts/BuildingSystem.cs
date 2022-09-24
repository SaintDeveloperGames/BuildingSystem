using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class BuildingSystem : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private GameObject[] _buildingElements;
        [SerializeField] private float _gridSize;
        private GameObject _buildingObject;
        private RaycastHit _hit;
        private IBuildingBehaviors _currentBehaviors;
        private float _heightGrid;
        private int _indexBuildElements;       

        void Update()
        {
            InputControl();
            Move();                      
        }

        private void Move()
        {
            if (_buildingObject == null)
                return;
            if (Physics.Raycast(_mainCamera.transform.position + transform.forward * 3f, Vector3.down, out _hit))
            {
                MoveOnGrid(_hit);
            }
        }

        private void MoveOnGrid(RaycastHit hit)
        {
            var currentPos = hit.point;
            currentPos -= new Vector3(1, 0, 1);
            currentPos.x /= _gridSize;
            currentPos.z /= _gridSize;
            currentPos = new Vector3(Mathf.Round(currentPos.x), currentPos.y, Mathf.Round(currentPos.z));           
            currentPos.x *= _gridSize;
            currentPos.z *= _gridSize;
            currentPos += new Vector3(1, _heightGrid / 2, 1);
            _buildingObject.transform.position = Vector3.Lerp(_buildingObject.transform.position, currentPos, 30 * Time.deltaTime);
        }

        private void InputControl()
        {
            KeyboardControl();
            if (_buildingObject != null)
            {
                RotateBuilding(_buildingObject);
                _currentBehaviors.Green();
                _currentBehaviors.Red();
                MouseControl();
            }           
        }

        private void KeyboardControl()
        {
            for (int i = 0; i < _buildingElements.Length; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    if (_buildingObject != null)
                        Destroy(_buildingObject);
                    _buildingObject = GetElement(_buildingElements[i]);
                    CheckingBuildingElement();
                    _indexBuildElements = i;
                }
            }
        }

        private void MouseControl()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (_currentBehaviors.Normal())
                {
                    var rotation = _buildingObject.transform.rotation;
                    _buildingObject = null;
                    _buildingObject = GetElement(_buildingElements[_indexBuildElements]);
                    _buildingObject.transform.rotation = rotation;
                    CheckingBuildingElement();
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
                Destroy(_buildingObject);
        }

        private GameObject GetElement(GameObject _buildingElement)
        {
            return Instantiate(_buildingElement, _hit.point, Quaternion.identity);
        }

        private void RotateBuilding(GameObject _buildingElement)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _buildingElement.transform.eulerAngles += new Vector3(0, 90, 0);
            }
        }

        private void CheckingBuildingElement()
        {
            if (_buildingObject.TryGetComponent(out Foundation foundation))
            {
                SetBehaviors(foundation);
                SetHeightGrid(foundation.Height);
            }
            if (_buildingObject.TryGetComponent(out Wall wall))
            {
                SetBehaviors(wall);
                SetHeightGrid(wall.Height);
            }
            if (_buildingObject.TryGetComponent(out Floor floor))
            {
                SetBehaviors(floor);
                SetHeightGrid(floor.Height);
            }
        }

        private void SetBehaviors(IBuildingBehaviors buildingBehaviors)
        {
            _currentBehaviors = buildingBehaviors;
        }

        private void SetHeightGrid(float height)
        {
            _heightGrid = height;
        }
    }
}

