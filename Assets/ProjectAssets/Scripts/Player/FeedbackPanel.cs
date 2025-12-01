using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackPanel : MonoBehaviour
{
    [SerializeField] private List<ComponentData> _componentsData;
    private List<GameObject> _componentInfoUI;

    [SerializeField] private TextMeshProUGUI _textMeshPrefab;
    [SerializeField] private RectTransform _textsParent;

    private void Start()
    {
        _componentInfoUI = new List<GameObject>();

        for (int i = 0; i < _componentsData.Count; i++)
        {
            TextMeshProUGUI newTxt = Instantiate(_textMeshPrefab, _textsParent);
            newTxt.text = _componentsData[i].Name;

            _componentInfoUI.Add(newTxt.gameObject);
        }
    }
    public void SetComponentFeedback(ComponentData component)
    {
        for (int i = 0; i < _componentsData.Count; i++)
        {
            if (_componentsData[i].Name == component.Name)
            {
                if (_componentInfoUI[i].TryGetComponent<ComponentInfoUI>(out ComponentInfoUI found))
                {
                    found.CheckComponentAsValue(true);
                }
                break;
            }
        }
    }
}