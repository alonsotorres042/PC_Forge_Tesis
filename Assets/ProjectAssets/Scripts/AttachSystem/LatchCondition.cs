using UnityEngine;
using System.Linq;

public class LatchCondition : ISlotCondition
{
    private readonly LatchController[] latches;

    public LatchCondition(LatchController[] latches) //Testing
    {
        this.latches = latches;
    }

    public bool IsMet(GameObject component)
    {
        return latches.All(l => l.IsLatchOpen);
    }
}