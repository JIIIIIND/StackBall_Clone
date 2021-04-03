using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DentBall : MonoBehaviour
{
    private Coroutine _denting;
    [SerializeField]
    private GameObject _moveObj;

    public void StopDent()
    {
        if (_denting != null)
        {
            StopCoroutine(_denting);
            _denting = null;
        }
        transform.localScale = new Vector3(0.6f, 0.4f, 0.6f);
    }
    public void StartDent()
    {
        if (_denting != null)
            StopCoroutine(_denting);
        _denting = StartCoroutine(Dent());
    }

    private IEnumerator Dent()
    {
        float scaleValue = transform.localScale.y;
        while (scaleValue <= 0.6f)
        {
            scaleValue += Time.deltaTime;
            transform.localScale = new Vector3(
                transform.localScale.x - Time.deltaTime,
                scaleValue,
                transform.localScale.z - Time.deltaTime);
            yield return null;
        }
    }

    private void Update()
    {
        this.transform.position = _moveObj.transform.position;
    }
}
