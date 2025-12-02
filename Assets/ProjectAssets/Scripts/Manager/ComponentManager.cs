using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ComponentManager : NonPersistentSingleton<ComponentManager>
{
    [Tooltip("Only component in this list will set an advice at UI")]
    [SerializeField] private List<ComponentData> _targetAssemblies; //The component that should be assembled to complete this scene
    [SerializeField] private bool _showAdvice;
    [SerializeField] private TextMeshProUGUI _adviceText;

    private int _currentAdviceIndex;

    public UnityEvent OnEveryComponentAssembled;

    public void RegisterComponent(ComponentData newData)
    {
        if(!_targetAssemblies.Contains(newData))
            _targetAssemblies.Add(newData);
    }
    public void UnregisterComponent(ComponentData newData)
    {
        if(_targetAssemblies.Contains(newData))
            _targetAssemblies.Remove(newData);

        if (_targetAssemblies.Count == 0)
            OnEveryComponentAssembled?.Invoke();
            
    }
    public void SetAdviceByIndex(int index)
    {
        if (!_showAdvice) return;

        if (_adviceText == null) return;

        if (index < 0 || index >= _targetAssemblies.Count)
            return;

        _adviceText.text = _targetAssemblies[index].AssemblyAdvice;
    }
    public void SetAdviceByData(ComponentData data)
    {
        if (!_showAdvice) return;

        if (_targetAssemblies.Contains(data))
            _adviceText.text = data.AssemblyAdvice;
    }
}