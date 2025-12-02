using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AssemblyComponent : MonoBehaviour
{
    public ComponentData Data;

    [SerializeField] private List<Behaviour> _disableComponents;

    public bool IsAssembled;
    public UnityEvent<ComponentData> OnAssembled;
    public UnityEvent<ComponentData> OnSelect;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private InteractableUnityEventWrapper _interactionWrapper;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _interactionWrapper = GetComponent<InteractableUnityEventWrapper>();
    }
    private void Start()
    {
        _interactionWrapper.WhenSelect.AddListener(() => OnSelect?.Invoke(Data));
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

        OnAssembled?.Invoke(Data);
    }
}