using UnityEngine;

[CreateAssetMenu(fileName = "New PC Configuration", menuName = "PC Assembly/PC Configuration")]
public class PCConfiguration : ScriptableObject
{
    public string pcName;
    public Sprite pcIcon;
    public ComponentSet compatibleComponents;
}