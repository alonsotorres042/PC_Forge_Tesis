using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComponentManager : NonPersistentSingleton<ComponentManager>
{
    [Tooltip("Only component in this list will set an advice at UI")]
    [SerializeField] private List<ComponentData> _componentsToAdvice;
    [SerializeField] private TextMeshProUGUI _adviceText;

    private int _currentAdviceIndex;

    public void RegisterComponent(ComponentData newData)
    {
        if(!_componentsToAdvice.Contains(newData))
            _componentsToAdvice.Add(newData);
    }
    public void UnregisterComponent(ComponentData newData)
    {
        if(_componentsToAdvice.Contains(newData))
            _componentsToAdvice.Remove(newData);
    }
    public void SetAdviceByIndex(int index)
    {
        if (_adviceText == null) return;

        if (index < 0 || index >= _componentsToAdvice.Count)
            return;

        _adviceText.text = _componentsToAdvice[index].AssemblyAdvice;
    }
    public void SetAdviceByData(ComponentData data)
    {
        if (_componentsToAdvice.Contains(data))
            _adviceText.text = data.AssemblyAdvice;
    }
}