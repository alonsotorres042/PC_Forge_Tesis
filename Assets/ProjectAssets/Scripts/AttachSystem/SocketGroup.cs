using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class SocketGroup : MonoBehaviour
{
    public ComponentData TargetComponent;
    public Transform TargetTransform;

    [Range(0f, 1f)]
    [SerializeField] private float _rotationThreshold;
    [SerializeField] private float _distanceThreshold;
    [SerializeField] private Transform _trackedComponent; //Tracked to calculate distance and rottion range

    [SerializeField] private List<SocketController> _sockets = new();

    public UnityEvent OnEverySocketEnabled;
    public UnityEvent OnAssembled;

    private void Awake()
    {
        ComponentManager.Instance.RegisterSocketGroup(this);
    }
    private void Start()
    {
        RegisterSockets();
    }
    private void Update()
    {
        if (_trackedComponent == null) return;

        if (TargetTransform == null) return;

        //Calculated distance
        Vector2 distanceVector = TargetTransform.position - _trackedComponent.transform.position;
        float positionOffset = distanceVector.magnitude;

        //Calculated rotation
        float angle = Quaternion.Angle(transform.rotation, _trackedComponent.rotation);
        float rotationOffset = angle / 180f;

        if (_distanceThreshold >= positionOffset && _rotationThreshold >= rotationOffset)
        {
            if (_trackedComponent.TryGetComponent<AssemblyComponent>(out AssemblyComponent otherComponent))
            {
                if (!otherComponent.enabled) return;

                if (otherComponent.IsAssembled) return;

                if (otherComponent.Data != TargetComponent) return;

                otherComponent.Assemble(TargetTransform);

                for (int i = 0; i < _sockets.Count; i++)
                {
                    _sockets[i].SetConnectionEnable(false);
                    _sockets[i].SetAssemble(true);
                }

                _trackedComponent = null;
            }
            
            OnAssembled?.Invoke();
        }

    }
    public void RegisterSockets()
    {
        _sockets.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<SocketController>(out SocketController newSocket))
            {
                _sockets.Add(newSocket);
            }
        }
    }
    public void SetTracked(Transform tracked)
    {
        if (_trackedComponent == tracked) return;

        _trackedComponent = tracked;
    }
    public bool EvaluateSocketConnections(bool state)
    {
        for (int i = 0; i < _sockets.Count; i++)
        {
            if (_sockets[i].IsConnected != state)
            {
                return false;
            }
        }

        if (state)
            OnEverySocketEnabled?.Invoke();

        return true;
    }
    public bool EvaluateSocketConnectionsEnable(bool state)
    {
        for (int i = 0; i < _sockets.Count; i++)
        {
            if (_sockets[i].CanConnect != state)
            {
                return false;
            }
        }

        if (state)
            OnEverySocketEnabled?.Invoke();

        return true;
    }
}