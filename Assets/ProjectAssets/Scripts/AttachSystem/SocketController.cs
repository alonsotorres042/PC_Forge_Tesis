using UnityEngine;

public class SocketController : MonoBehaviour
{    
    private SocketGroup _group;

    public bool CanConnect;
    public bool IsConnected;
    public bool IsAssembled;

    private void Start()
    {
        _group = transform.parent.GetComponent<SocketGroup>();
    }

    public void SetConnectionEnable(bool set)
    {
        CanConnect = set;

        if (_group.EvaluateSocketConnectionsEnable(true))
            _group.OnEverySocketEnabled?.Invoke();
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

        SetConnection(true);

        if (!_group.EvaluateSocketConnections(true)) return;
        
        _group.SetTracked(other.transform);
    }
    public void RequestDesconnection(Collider other)
    {
        SetConnection(false);

        if (!_group.EvaluateSocketConnections(false)) return;
        
        _group.SetTracked(null);
    }
}