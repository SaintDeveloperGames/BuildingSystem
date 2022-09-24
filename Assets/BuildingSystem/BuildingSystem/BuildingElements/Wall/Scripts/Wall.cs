using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BuildingElement, IBuildingBehaviors
{
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
        Collider[] currentColliders = Physics.OverlapBox(_childe.transform.position, new Vector3(0.35f, 1.3f, 0.15f), transform.rotation, 
            LayerMask.GetMask("Ground", "Wall"));
        _colliders.AddRange(currentColliders);
        _canBuilt = _colliders.Count == 0;
    }
}
