using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AssemblyComponent : MonoBehaviour
{
    public ComponentData Data;

    [SerializeField] private List<Behaviour> _disableComponents;

    public bool IsAssembled;
    public UnityEvent OnAssembled;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }
    public void Assemble(Transform target)
    {
        if (IsAssembled) return;

        foreach (Behaviour behaviour in _disableComponents)
        {
            behaviour.enabled = false;
        }

        _rigidbody.isKinematic = true;
        _boxCollider.isTrigger = true;

        transform.SetParent(target);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //transform.localScale = target.localScale;

        IsAssembled = true;

        OnAssembled?.Invoke();
    }
}