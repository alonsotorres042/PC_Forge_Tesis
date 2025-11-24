using System.Collections.Generic;
using UnityEngine;

public class AssemblyComponent : MonoBehaviour
{
    public ComponentData Data;

    [SerializeField] private List<Behaviour> _disableComponents;
    
    private Rigidbody _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    public void Assemble(Transform target)
    {
        foreach (Behaviour behaviour in _disableComponents)
        {
            behaviour.enabled = false;
        }
        _rigidbody.isKinematic = true;

        transform.SetParent(target);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //transform.localScale = target.localScale;
    }
}