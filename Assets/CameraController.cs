using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _xOffset;
    [SerializeField] float _yOffset;
    [SerializeField] float _followSpeed = 10f;
    [SerializeField] bool _isXOffsetLocked;
    [SerializeField] bool _isYOffsetLocked;
    private void Update()
    {
        CameraFollow();
    }

    void CameraFollow()
    {
        float xTarget = _xOffset + _target.position.x;
        float yTarget = _yOffset + _target.position.y;

        float xNew = transform.position.x;
        if (!_isXOffsetLocked)
        {
            xNew = Mathf.Lerp(xNew, xTarget, _followSpeed);
        }
        float yNew = transform.position.y;
        if (!_isYOffsetLocked)
        {
            yNew = Mathf.Lerp(yNew, yTarget, _followSpeed);
        }
        transform.position = new Vector3(xNew, yNew, transform.position.z);

    }
}   

