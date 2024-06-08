using UnityEngine;

public class BasicFillbarComponent : MonoBehaviour
{
    [SerializeField] private RectTransform _fillImage;
    [SerializeField] private float _width = 2.5f;
    [SerializeField] private float _minWidthValue = -1.8f;

    public void UpdateFillbar(float percentage)
    {
        var sizeDelta = _fillImage.sizeDelta;
        var newWidth = -(_width - _width * percentage);
        if (Mathf.Abs(newWidth - _width) < 0.001f)
        {
            newWidth = -_width;
        }
        else if (newWidth < _minWidthValue)
        {
            newWidth = _minWidthValue;
        }

        sizeDelta.x = newWidth;
        
        _fillImage.sizeDelta = sizeDelta;
    }

}
