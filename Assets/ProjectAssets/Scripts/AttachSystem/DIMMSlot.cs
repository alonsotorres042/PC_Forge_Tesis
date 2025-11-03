using UnityEngine;

public class DIMMSlot : SlotBase
{
    [SerializeField] private LatchController[] latches;

    protected override void Start()
    {
        base.Start();
        AddCondition(new LatchCondition(latches));
    }
}