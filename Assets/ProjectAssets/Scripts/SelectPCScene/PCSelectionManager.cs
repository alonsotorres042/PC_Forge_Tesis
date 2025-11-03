using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class PCSelectionManager : MonoBehaviour
{
    [Header("PC Configurations")]
    [SerializeField] private PCConfiguration[] availablePCs;

    [Header("UI References")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Image pcIconImage;
    [SerializeField] private TMP_Text pcNameText;

    [Header("Current Selection")]
    [SerializeField] private PCConfiguration currentPC;

    [Header("Events")]
    public UnityEvent<PCConfiguration> OnPCSelectionChanged;
    public UnityEvent OnPCConfirmed;

    private int currentIndex = 0;

    private void OnEnable()
    {
        nextButton.onClick.AddListener(NextPC);
        previousButton.onClick.AddListener(PreviousPC);
        confirmButton.onClick.AddListener(ConfirmSelection);
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
        confirmButton.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        UpdateCurrentPC(0);
    }

    private void NextPC()
    {
        currentIndex = (currentIndex + 1) % availablePCs.Length;
        UpdateCurrentPC(currentIndex);
    }

    private void PreviousPC()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = availablePCs.Length - 1;
        UpdateCurrentPC(currentIndex);
    }

    private void UpdateCurrentPC(int index)
    {
        currentIndex = index;
        currentPC = availablePCs[currentIndex];

        if (pcIconImage != null && currentPC.pcIcon != null)
        {
            pcIconImage.sprite = currentPC.pcIcon;
        }

        if (pcNameText != null)
        {
            pcNameText.text = currentPC.pcName;
        }

        OnPCSelectionChanged?.Invoke(currentPC);
    }

    private void ConfirmSelection()
    {
        OnPCConfirmed?.Invoke();
    }

    public PCConfiguration GetCurrentPC()
    {
        return currentPC;
    }
}