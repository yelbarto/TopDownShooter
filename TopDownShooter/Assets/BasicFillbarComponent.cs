using UnityEngine;

public class BasicFillbarComponent : MonoBehaviour
{
    [SerializeField] private RectTransform _fillImage;
    [SerializeField] private float _width = 2.5f;

    public void UpdateFillbar(float percentage)
    {
        var sizeDelta = _fillImage.sizeDelta;
        var newWidth = -(_width - _width * percentage);
        if (Mathf.Abs(newWidth + _width) < 0.001f)
        {
            newWidth = -_width - 0.1f;
        }

        sizeDelta.x = newWidth;
        
        _fillImage.sizeDelta = sizeDelta;
    }

}
