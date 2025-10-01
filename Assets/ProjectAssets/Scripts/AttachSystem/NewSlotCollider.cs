using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class NewSlotCollider : MonoBehaviour
{
    [SerializeField] private string requiredTag;
    [SerializeField] private AlignmentDirection alignmentDirection = AlignmentDirection.Forward;
    [SerializeField] private float alignmentThreshold;  // ATTENTION!!

    private SlotBase slot;
    private int slotIndex;
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
    }

    public void Initialize(SlotBase slot, int index)
    {
        this.slot = slot;
        this.slotIndex = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(requiredTag) && ValidateAlignment(other))
        {
            slot.ReportCollision(slotIndex, other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag))
        {
            slot.ReportCollisionEnd(slotIndex, other.transform.parent.gameObject);
        }
    }

    private bool ValidateAlignment(Collider other)
    {
        Vector3 slotDirection = GetAlignmentVector();
        Vector3 toComponent = (other.transform.position - transform.position).normalized;
        return Vector3.Dot(slotDirection, toComponent) >= alignmentThreshold;
    }

    private Vector3 GetAlignmentVector()
    {
        switch (alignmentDirection)
        {
            case AlignmentDirection.Up: return transform.up;
            case AlignmentDirection.Down: return -transform.up;
            case AlignmentDirection.Forward: return transform.forward;
            case AlignmentDirection.Back: return -transform.forward;
            case AlignmentDirection.Right: return transform.right;
            case AlignmentDirection.Left: return -transform.right;
            default: return transform.forward;
        }
    }
}