using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Vector3 _savedVelocity;
    private Rigidbody _rig;
    [SerializeField]
    private float _downVelocity;
    [SerializeField]
    private float _bounce;
    [SerializeField]
    private ParticleSystem _colParticle;
    [SerializeField]
    private float _limitHeight;
    [SerializeField]
    private float _floor;
    [SerializeField]
    private float _startYPos;
    private Coroutine _dentBall;

    // Start is called before the first frame update
    public void InitRound()
    {
        // game clear, roll select value
        this.transform.position = new Vector3(0, _startYPos, -2.1f);
        _rig.velocity = new Vector3(0, _bounce, 0);
        _savedVelocity = _rig.velocity;
    }
    void Start()
    {
        _dentBall = null;
        _rig = GetComponent<Rigidbody>();
        InitRound();
    }

    private void OnTriggerEnter(Collider other)
    {
        _rig.velocity = new Vector3(0, _downVelocity, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        _rig.velocity = _savedVelocity;
        _floor = collision.transform.position.y;
        if (_dentBall != null)
        {
            StopCoroutine(_dentBall);
            _dentBall = null;
        }
        transform.localScale = new Vector3(0.6f, 0.4f, 0.6f);
        _colParticle.Play();
        // 세로 -> 가로
    }

    // 가로 -> 세로
    IEnumerator DentBall()
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
    private void OnCollisionExit(Collision collision)
    {
        //가로 -> 세로
        if (_dentBall != null)
            StopCoroutine(_dentBall);
        _dentBall = StartCoroutine(DentBall());
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > _floor + _limitHeight)
            _rig.velocity = -_savedVelocity;
    }
}
