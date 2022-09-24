using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foundation : BuildingElement, IBuildingBehaviors
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
        if (_colliders.Count > 0 || !_canBuilt)
        {
            SetMaterial(RedMaterial);
            return true;
        }           
        return false;
    }

    private void CheckingPlaceBuilding()
    {
        if (Physics.Raycast(_ray.transform.position, Vector3.down, out RaycastHit hit))
        {
            _canBuilt = hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
        }
        _colliders = new List<Collider>();
        Collider[] currentColliders = Physics.OverlapBox(_childe.transform.position, new Vector3(1f, 0.5f, 1f), transform.rotation,
            LayerMask.GetMask("Details"));
        _colliders.AddRange(currentColliders);
    }
}
