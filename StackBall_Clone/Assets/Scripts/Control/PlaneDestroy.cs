using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> _list;
    private Vector3 _center;
    [SerializeField]
    private float _second;
    [SerializeField]
    private float _force;
    void Start()
    {
        _list = this.gameObject.GetComponent<CreatePlane>()._planeList;
        _center = new Vector3(0, _list[0].transform.position.y, 0);
    }

    private IEnumerator DestroyWaitTime(GameObject gameObject, bool isRight, bool[] isRunnings, int idx)
    {
        Rigidbody rig = gameObject.GetComponent<Rigidbody>();
        float time = 0;
        Quaternion rotValue = Quaternion.Euler(
            Random.Range(0, 360) * Time.deltaTime,
            Random.Range(0, 360) * Time.deltaTime,
            Random.Range(0, 360) * Time.deltaTime);

        isRunnings[idx] = true;
        rig.useGravity = true;
        while (time < _second && gameObject != null)
        {
            Vector3 origin = gameObject.transform.position;
            if (isRight)
                gameObject.transform.position = new Vector3(
                    origin.x + _force * 2 * Time.deltaTime,
                    origin.y + _force * Time.deltaTime,
                    origin.z);
            else
                gameObject.transform.position = new Vector3(
                    origin.x + -1 * _force * 2 * Time.deltaTime,
                    origin.y + _force * Time.deltaTime,
                    origin.z);
            gameObject.transform.rotation =
                gameObject.transform.rotation * rotValue;
            time += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        isRunnings[idx] = false;
    }

    private IEnumerator DestroySelf(bool[] isRunnings)
    {
        int idx = 0;

        while (idx < isRunnings.Length)
        {
            if (isRunnings[idx] == false)
                idx++;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void DestroyPlane(GameObject collision)
    {
        bool[] isRunnings = new bool[_list.Count];
        for (int i = 0; i < _list.Count; i++)
        {
            if (collision == _list[i])
            {
                Destroy(collision);
                isRunnings[i] = false;
            }
            else
            {
                Vector3 dir =
                _list[i].GetComponent<TilePosition>().
                _posObject.transform.position - _center;
                if (dir.x > 0)
                    StartCoroutine(DestroyWaitTime(_list[i], true, isRunnings, i));
                else
                    StartCoroutine(DestroyWaitTime(_list[i], false, isRunnings, i));
            }
        }
        StartCoroutine(DestroySelf(isRunnings));
    }
}
