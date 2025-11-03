using UnityEngine;

[CreateAssetMenu(fileName = "New Component Set", menuName = "PC Assembly/Component Set")]
public class ComponentSet : ScriptableObject
{
    public ComponentItem CPU;
    public ComponentItem RAM;
    public ComponentItem GPU;
    public ComponentItem Cooler;
}