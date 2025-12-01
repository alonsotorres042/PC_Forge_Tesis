using System.Collections.Generic;
using UnityEngine;

public enum ComponentType { Complement, Cooler, CPU, GPU, Motherboard, RAM, ThermalPaste, PowerSupply, HardDisk, FloppyDiskReader, OpticalDiskReader }
[CreateAssetMenu(fileName = "ComponentData", menuName = "Scriptable Objects/ComponentData")]
public class ComponentData : ScriptableObject
{
    public ComponentType ComponentType;
    public string Name;
    public List<string> Features;
    public string AssemblyAdvice = "No advice yet";
}