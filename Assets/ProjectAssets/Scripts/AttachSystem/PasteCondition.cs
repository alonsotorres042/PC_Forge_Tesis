using UnityEngine;

public class PasteCondition : ISlotCondition
{
    private readonly ThermalPaste paste;

    public PasteCondition(ThermalPaste paste)
    {
        this.paste = paste;
    }

    public bool IsMet(GameObject component)
    {
        return paste != null && paste.IsApplied;
    }
}