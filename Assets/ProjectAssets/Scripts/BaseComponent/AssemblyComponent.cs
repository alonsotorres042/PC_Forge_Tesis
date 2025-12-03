using Oculus.Interaction;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AssemblyComponent : MonoBehaviour
{
    public ComponentData Data;

    [SerializeField] private SocketGroup _targetGroup;
    [SerializeField] private List<Behaviour> _disableComponents;

    public bool IsAssembled;
    private bool _isSelected;

    public UnityEvent<ComponentData> OnAssembled;
    public UnityEvent<ComponentData> OnSelect;

    private Rigidbody _rigidbody;
    private BoxCollider _boxCollider;
    private InteractableUnityEventWrapper _interactionWrapper;
    private LineRenderer _lineRenderer;

    [Header("Feedback Line")]
    public bool _enableLine;

    [Header("Feedback Mesh")]
    [SerializeField] private GameObject _meshObject;
    [SerializeField] private Material _meshMaterial;
    private GameObject _currentObject = null;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
        _interactionWrapper = GetComponent<InteractableUnityEventWrapper>();
        _lineRenderer = GetComponent<LineRenderer>();

    }
    private void Start()
    {
        _targetGroup = ComponentManager.Instance.SetSocketGroupbyData(Data);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Mesh")
            {
                _meshObject = transform.GetChild(i).gameObject;
                var newObj = Instantiate(_meshObject, _targetGroup.TargetTransform);
                _meshObject = newObj;
                _meshObject.SetActive(false);

                for (int j = _meshObject.transform.childCount - 1; j >= 0; j--)
                {
                    Transform child = _meshObject.transform.GetChild(i);
                    Destroy(child.gameObject);
                }

                _meshObject.GetComponent<MeshRenderer>().materials = new Material[] { _meshMaterial };
            }
        }
    }
    private void OnEnable()
    {
        _interactionWrapper.WhenSelect.AddListener(() => Select());
        _interactionWrapper.WhenUnselect.AddListener(() => Unselect());
    }
    private void OnDisable()
    {
        _interactionWrapper.WhenSelect.RemoveListener(() => Select());
        _interactionWrapper.WhenUnselect.RemoveListener(() => Unselect());
    }
    private void Update()
    {
        if (_enableLine && _isSelected)
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _targetGroup.TargetTransform.position);
        }
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
    public void Select()
    {
        _lineRenderer.enabled = true;
       _isSelected = true;
        _meshObject.SetActive(true);

        OnSelect?.Invoke(Data);
    }
    public void Unselect()
    {
        _lineRenderer.enabled = false;
        _isSelected = false;
        _meshObject.SetActive(false);
    }
}