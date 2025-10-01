using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class SlotBase : MonoBehaviour
{
    public event Action<GameObject> OnAttached;
    public event Action<GameObject> OnDetached;

    [SerializeField] protected NewSlotCollider[] colliders; //Update the class name!!

    protected List<ISlotCondition> conditions = new List<ISlotCondition>();
    protected GameObject currentComponent;
    protected bool[] slotsOccupied;

    protected virtual void Start()
    {
        slotsOccupied = new bool[colliders.Length];
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].Initialize(this, i);
        }
    }
    public void ReportCollision(int slotIndex, GameObject component)
    {
        slotsOccupied[slotIndex] = true;
        TryAttach(component);
    }
    public void ReportCollisionEnd(int slotIndex, GameObject component)
    {
        slotsOccupied[slotIndex] = false;
        if (slotsOccupied.All(o => o == false)) //Elegant
        {
            currentComponent = null;
            OnDetached?.Invoke(component);
        }
    }
    protected virtual void TryAttach(GameObject component)
    {
        if (currentComponent != null) return;

        if (AllSlotsOccupied() && conditions.All(c => c.IsMet(component)))
        {
            Attach(component);
        }
    }
    protected virtual void Attach(GameObject component)
    {
        var hw = component.GetComponent<IHardwareComponent>();
        if (hw != null)
        {
            hw.SnapToSlot(transform);
            hw.Deactivate();
            currentComponent = component;
            OnAttached?.Invoke(component);
        }
    }
    protected bool AllSlotsOccupied()
    {
        return slotsOccupied.All(o => o == true);
    }
    public void AddCondition(ISlotCondition condition)
    {
        if (!conditions.Contains(condition))
            conditions.Add(condition);
    }
}