using UnityEngine;

public interface IHardwareComponent
{
    void SnapToSlot(Transform slotTransform);
    void Deactivate();
}
public interface ISlotCondition
{
    bool IsMet(GameObject component);
}
public enum AlignmentDirection { Up, Down, Forward, Back, Right, Left }