using UnityEngine;
using System.Collections.Generic;

public class SocketController : MonoBehaviour
{
    private List<SocketController> _partners = new();
    
    private SocketGroup _group;

    public bool CanConnect;
    public bool IsConnected;
    public bool IsAssembled;

    private void Start()
    {
        _group = transform.parent.GetComponent<SocketGroup>();
        RegisterPartners();
    }
    public void RegisterPartners()
    {
        _partners.Clear();
        
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if (transform.parent.GetChild(i) != transform && transform.parent.GetChild(i).TryGetComponent<SocketController>(out SocketController newSocket))
            {
                _partners.Add(newSocket);
            }
        }
    }
    public void SetConnectionEnable(bool set)
    {
        CanConnect = set;
    }
    public void SetConnection(bool set)
    {        
        if (!CanConnect) return;

        IsConnected = set;
    }
    public void SetAssemble(bool set)
    {
        IsAssembled = set;
    }
    public void RequestConnection(Collider other)
    {
        if (!CanConnect) return;

        if (!EvaluatePartnersConnection()) return;

        if (other.TryGetComponent<AssemblyComponent>(out AssemblyComponent otherComponent))
        {
            if (otherComponent.Data != _group._targetComponent) return;

            otherComponent.Assemble(_group.TargetTransform);
            
            SetAssemble(true);
        }
        
    }
    public bool EvaluatePartnersConnection()
    {
        for (int i = 0; i < _partners.Count; i++)
        {
            if (!_partners[i].IsConnected)
            {
                return false;
            }
        }
        return true;
    }
}