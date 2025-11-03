using UnityEngine;

public class ComponentInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject _checkMark;
    public void CheckComponentAsValue(bool value)
    {
        _checkMark.SetActive(value);
    }
}