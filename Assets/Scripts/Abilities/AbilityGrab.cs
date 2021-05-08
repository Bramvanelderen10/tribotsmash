using System.Collections;
using UnityEngine;
using Tribot;

public class AbilityGrab : Ability
{
    public float MaxGrabDuration = 1f;
    public float StunDuration = 0.2f;
    public float ThrowForce = 1500f;
    public float RotationSpeed = 0f;
    public bool IsGrabbing = false;
    public Vector3 TargetLocationOffset = new Vector3(0, 1f, 1.2f);
    public Vector3 HitBoxSize = new Vector3(0.5f, 0.55f, 0.5f);
    public Vector3 HitBoxCenter = new Vector3(0f, 1.2f, 0.8f);

    private GameObject _grabbedTarget;
    private Coroutine _cr = null;
    private AudioSource _audio;

    private CastEvent _castEvent;
    protected override void Start()
    {
        base.Start();
        _audio = GetComponent<AudioSource>();
    }

    public override void Cast()
    {
        if (!IsReady || IsCasting || !_player.IsOwner)
            return;

        var center = transform.parent.TransformPoint(HitBoxCenter);
        GameObject result = null;
        foreach (var coll in Physics.OverlapBox(center, HitBoxSize, transform.rotation))
        {
            if (coll.transform == transform.parent)
                continue;

            var obj = coll.gameObject;
            if (obj.GetComponent<Player>() == null)
                continue;

            if (result == null)
            {
                result = obj;
            }
            else
            {
                if (Vector3.Distance(obj.transform.position, transform.parent.position) < Vector3.Distance(result.transform.position, transform.parent.position))
                {
                    result = obj;
                }
            }
        }

        if (result != null)
        {
            var comp = result.GetComponent<Player>();

            if (comp.CanStun())
                photonView.RPC("RpcGrab", PhotonTargets.All, comp.Index);
        }
    }

    [PunRPC]
    void RpcGrab(int index)
    {
        var iplayer = Container<IPlayerManager>.Instance.Items.Find(x => x.Index == index);
        var result = iplayer.PlayerObject;

        var comp = result.GetComponent<Player>();
        if (comp.Stun(MaxGrabDuration, true))
        {
            Release = false;
            IsCasting = true;
            IsGrabbing = true;
            _cr = StartCoroutine(Overtime(MaxGrabDuration));
            _grabbedTarget = result;
            _grabbedTarget.GetComponent<Rigidbody>().useGravity = false;
            PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Grab);
            CastCallBack(this);
        }

        Timestamp = Time.time + Cooldown;
    }

    public override bool Cancel()
    {
        if (!IsCasting)
            return false;

        Timestamp = Time.time + Cooldown;
        IsCasting = false;
        IsGrabbing = false;

        if (_grabbedTarget == null)
            return true;

        _grabbedTarget.GetComponent<Rigidbody>().useGravity = true;
        _grabbedTarget.GetComponent<Player>().ClearStun();

        return true;
    }

    void Update()
    {
        if (IsCasting)
        {
            if (IsGrabbing)
            {
                if (_grabbedTarget == null)
                {
                    Cancel();
                }
                else
                {
                    _grabbedTarget.GetComponent<Rigidbody>()
                    .MovePosition(transform.parent.position + transform.parent.rotation * TargetLocationOffset);
                    var targetRotation = _grabbedTarget.transform.rotation.eulerAngles;
                    targetRotation.y = transform.parent.rotation.y + 180f;
                    _grabbedTarget.transform.rotation =
                        Quaternion.Lerp(_grabbedTarget.transform.rotation, Quaternion.Euler(targetRotation), RotationSpeed * Time.deltaTime);

                    if (Release)
                    {
                        Throw();
                    }
                }
            }
        }
    }

    void Throw()
    {
        if (!IsCasting || !IsGrabbing)
            return;

        var rb = _grabbedTarget.GetComponent<Rigidbody>();
        rb.AddForce(transform.parent.forward*ThrowForce);
        rb.useGravity = true;

        IsGrabbing = false;
        IsCasting = false;
        _grabbedTarget.GetComponent<Player>().ClearStun();
        StopCoroutine(_cr);
        PlayerState.SwitchState(PlayerStateMachine.PlayerStates.Idle);
    }

    IEnumerator Overtime(float time)
    {
        yield return new WaitForSeconds(time - 0.01f);

        Cancel();
        _player.Stun(StunDuration);
    }
}
