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
    private Vector3 _startPos;
    [SerializeField]
    private GameObject _dentObj;
    private DentBall _dent;

    // Start is called before the first frame update
    public void InitRound()
    {
        // game clear, roll select value
        this.transform.position = _startPos;
        _rig.velocity = new Vector3(0, _bounce, 0);
        _savedVelocity = _rig.velocity;
    }
    void Start()
    {
        _dent = _dentObj.GetComponent<DentBall>();
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
        //stop dent
        _dent.StopDent();
        _colParticle.Play();
        // 세로 -> 가로
    }

    // 가로 -> 세로

    private void OnCollisionExit(Collision collision)
    {
        // StartDent
        _dent.StartDent();
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > _floor + _limitHeight)
            _rig.velocity = -_savedVelocity;
    }
}
