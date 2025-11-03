using System.Collections.Generic;
using UnityEngine;

public enum ComponentType { Cooler, CPU, GPU, Motherboard, RAM, ThermalPaste }
[CreateAssetMenu(fileName = "ComponentData", menuName = "Scriptable Objects/ComponentData")]
public class ComponentData : ScriptableObject
{
    public ComponentType ComponentType;

    public string Name;
    public List<string> Features;
}