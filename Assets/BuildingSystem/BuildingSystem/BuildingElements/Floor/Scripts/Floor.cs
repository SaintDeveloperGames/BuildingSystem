using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : BuildingElement, IBuildingBehaviors
{
    [SerializeField] private Transform _ray;
    private bool _canBuilt;
    private Collider _collider;
    private Transform _childe;
    private List<Collider> _colliders = new List<Collider>();
    private void Start()
    {
        _childe = transform.GetChild(0);
        _collider = _childe.GetComponent<Collider>();
        _collider.enabled = false;
    }

    private void Update()
    {
        CheckingPlaceBuilding();
    }

    public bool Green()
    {
        if (_canBuilt)
            SetMaterial(GreenMaterial);
        return _canBuilt;
    }

    public bool Normal()
    {
        if (_canBuilt)
        {
            SetMaterial(NormalMaterial);
            _collider.enabled = true;
            this.enabled = false;
            return true;
        }
        return false;
    }

    public bool Red()
    {
        if (!_canBuilt)
        {
            SetMaterial(RedMaterial);
            return true;
        }
        return false;
    }

    private void CheckingPlaceBuilding()
    {
        _colliders = new List<Collider>();
        var canBuilt = true;
        if (Physics.Raycast(_ray.transform.position, Vector3.down, out RaycastHit hit))
        {
            canBuilt = hit.collider.gameObject.layer != LayerMask.NameToLayer("Floor");
            if (!canBuilt)
            {
                if (_childe.transform.position.y - hit.point.y > 0)
                    canBuilt = true;
            }
            ChangePosition(hit);
        }        
        Collider[] currentColliders = Physics.OverlapBox(_childe.transform.position, new Vector3(1.5f, 0.15f, 1.5f), transform.rotation, 
            LayerMask.GetMask("Wall", "Floor"));
        _colliders.AddRange(currentColliders);
        _canBuilt = _colliders.Count > 0 && canBuilt;
    }

    private void ChangePosition(RaycastHit hit)
    {
        _childe.transform.localPosition = (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) ? 
            new Vector3(0, 3f, 0) : new Vector3(0, 2.5f, 0);
    }
}
