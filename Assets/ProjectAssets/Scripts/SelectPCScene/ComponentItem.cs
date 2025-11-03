using UnityEngine;

[CreateAssetMenu(fileName = "New Component", menuName = "PC Assembly/Component")]
public class ComponentItem : ScriptableObject
{
    public string componentName;
    [TextArea] public string description;
    public Sprite icon;
    public GameObject componentPrefab;
}