using UnityEngine;

public abstract class BuildingElement : MonoBehaviour
{

    [SerializeField] private Material _green;
    [SerializeField] private Material _red;
    [SerializeField] private Material _normal;
    [SerializeField] private float _height;

    public Material GreenMaterial
    {
        get { return _green; }
    }

    public Material RedMaterial
    {
        get { return _red; }
    }

    public Material NormalMaterial
    {
        get { return _normal; }
    }

    public float Height
    {
        get { return _height; }
    }

    public void SetMaterial(Material other)
    {
        transform.GetChild(0).GetComponent<MeshRenderer>().material = other;
    }
}
