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
    private GameObject _colParticle;
    [SerializeField]
    private GameObject _traceObj;
    [SerializeField]
    private float _limitHeight;
    [SerializeField]
    private float _floor;
    [SerializeField]
    private Vector3 _startPos;
    [SerializeField]
    private GameObject _dentObj;
    private DentBall _dent;
    [SerializeField]
    private Material[] _materials;
    [SerializeField]
    private AudioSource _hitSound;

    // Start is called before the first frame update
    public void InitRound()
    {
        this.transform.position = _startPos;
        _rig.velocity = new Vector3(0, _bounce, 0);
        _savedVelocity = _rig.velocity;
        DataManager.Instance.gameData._ballColor =
            _materials[Random.Range(0, _materials.Length)].color;
        while (DataManager.Instance.gameData._ballColor ==
            DataManager.Instance.gameData._planeColor)
            DataManager.Instance.gameData._ballColor =
            _materials[Random.Range(0, _materials.Length)].color;
        _dent.ChangeBallColor();
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
        if (collision.gameObject.tag == "Finish")
        {
            this.gameObject.GetComponent<TouchEvent>().GameClear();
        }
        else
        {
            _rig.velocity = _savedVelocity;
            _floor = collision.transform.position.y;
            _dent.StopDent();
            GameObject particle = Instantiate(_colParticle, collision.contacts[0].point, Quaternion.Euler(-90, 0, 0));
            ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
            main.startColor = DataManager.Instance.gameData._ballColor;
            GameObject trace = Instantiate(_traceObj, collision.contacts[0].point, Quaternion.Euler(-90, 0, 0));
            trace.GetComponent<SpriteRenderer>().color = DataManager.Instance.gameData._ballColor;
            trace.transform.parent = collision.gameObject.transform;
            trace.transform.position = new Vector3(
                trace.transform.position.x,
                trace.transform.position.y + 0.1f,
                trace.transform.position.z);
            _hitSound.Play();
        }
    }

    private void CheckTouch()
    {
        if (Input.touchCount > 0 && !DataManager.Instance.gameData._gameEnd)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved ||
                touch.phase == TouchPhase.Stationary)
                _rig.velocity = new Vector3(0, _downVelocity, 0);
            else if (touch.phase == TouchPhase.Canceled ||
                touch.phase == TouchPhase.Ended)
                _rig.velocity = _savedVelocity;
        }
        if (!DataManager.Instance.gameData._gameEnd)
        {
            if (Input.GetMouseButton(0))
                _rig.velocity = new Vector3(0, _downVelocity, 0);
            if (Input.GetMouseButtonUp(0))
                _rig.velocity = _savedVelocity;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _dent.StartDent();
    }
    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y >= _floor + _limitHeight)
            _rig.velocity = -_savedVelocity;
    }
}
