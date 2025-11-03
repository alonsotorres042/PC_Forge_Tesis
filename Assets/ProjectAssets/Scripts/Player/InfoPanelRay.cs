using TMPro;
using UnityEngine;


public class InfoPanelRay : MonoBehaviour
{
    [SerializeField] GameObject _panel;

    [SerializeField] float _rayDistance;
    [SerializeField] LayerMask _layers;

    [SerializeField] TextMeshProUGUI _componentName;
    [SerializeField] TextMeshProUGUI _componentFeatures;

    private AssemblyComponent _lastComponent;

    private LineRenderer _lineRenderer;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            SwitchPanelActive();
        }

        if (_lineRenderer.enabled)
        {
            _lineRenderer.SetPosition(0, transform.position);

            Vector3 targetPos = transform.position + transform.TransformDirection(Vector3.forward *_rayDistance);
            _lineRenderer.SetPosition(1, targetPos);
        }
    }

    private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _rayDistance, _layers))
        {
            if (hit.transform.TryGetComponent<AssemblyComponent>(out AssemblyComponent component))
            {
                if (_lastComponent != null && _lastComponent == component) return;

                ApplyUIInfo(component.Data);
                _lastComponent = component;
            }
        }
    }
    public void SwitchPanelActive()
    {
        _panel.SetActive(!_panel.activeSelf);
        _lineRenderer.enabled = !_lineRenderer.enabled;
    }
    public void ApplyUIInfo(ComponentData componentData)
    {
        _componentName.text = componentData.Name;

        _componentFeatures.text = "";

        for (int i = 0; i < componentData.Features.Count; i++)
        {
            _componentFeatures.text = _componentFeatures.text +  "- " + componentData.Features[i] + System.Environment.NewLine;
        }
    }
}