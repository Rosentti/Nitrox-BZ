using NitroxClient_BelowZero.GameLogic.FMOD;
using NitroxClient_BelowZero.Unity.Smoothing;
using NitroxModel.GameLogic.FMOD;
using UnityEngine;

namespace NitroxClient_BelowZero.MonoBehaviours;

public class MultiplayerExosuit : MultiplayerVehicleControl
{
    private float jetLoopingSoundDistance;

    private bool lastThrottle;
    private float timeJetsChanged;
    private Exosuit exosuit;

    protected override void Awake()
    {
        exosuit = GetComponent<Exosuit>();
        WheelYawSetter = value => exosuit.steeringWheelYaw = value;
        WheelPitchSetter = value => exosuit.steeringWheelPitch = value;
        base.Awake();
        SmoothRotation = new ExosuitSmoothRotation(gameObject.transform.rotation);

        this.Resolve<FMODWhitelist>().TryGetSoundData(exosuit.jumpJetsSound.Event, out SoundData jetSoundData);
        jetLoopingSoundDistance = jetSoundData.Radius;
    }

    internal override void Enter()
    {
        GetComponent<Rigidbody>().freezeRotation = false;
        exosuit.SetIKEnabled(true);
        exosuit.thrustIntensity = 0;
        base.Enter();
    }

    public override void Exit()
    {
        GetComponent<Rigidbody>().freezeRotation = true;
        exosuit.SetIKEnabled(false);
        exosuit.jumpJetsSound.Stop();
        exosuit.fxcontrol.Stop(0);
        base.Exit();
    }

    internal override void SetThrottle(bool isOn)
    {
        if (timeJetsChanged + 0.3f <= Time.time && lastThrottle != isOn)
        {
            timeJetsChanged = Time.time;
            lastThrottle = isOn;
            if (isOn)
            {
                exosuit.jumpJetsSound.Play();
                exosuit.fxcontrol.Play(0);
                exosuit.areFXPlaying = true;
            }
            else
            {
                exosuit.jumpJetsSound.Stop();
                exosuit.fxcontrol.Stop(0);
                exosuit.areFXPlaying = false;
            }
        }
    }

    private void Update()
    {
        if (exosuit.jumpJetsSound.IsPlaying())
        {
            if (exosuit.jumpJetsSound.EventInstance.hasHandle())
            {
                float volume = FMODSystem.CalculateVolume(transform.position, Player.main.transform.position, jetLoopingSoundDistance, 1f);
                exosuit.jumpJetsSound.EventInstance.setVolume(volume);
            }
        }
        else
        {
            if (exosuit.jumpJetsSound.EventInstance.hasHandle())
            {
                float volume = FMODSystem.CalculateVolume(transform.position, Player.main.transform.position, jetLoopingSoundDistance, 1f);
                exosuit.jumpJetsSound.EventInstance.setVolume(volume);
            }
        }
    }

    internal override void SetArmPositions(Vector3 leftArmPosition, Vector3 rightArmPosition)
    {
        base.SetArmPositions(leftArmPosition, rightArmPosition);
        Transform leftAim = exosuit.aimTargetLeft;
        Transform rightAim = exosuit.aimTargetRight;
        if (leftAim)
        {
            Vector3 leftAimPosition = leftAim.localPosition;
            leftAimPosition = new Vector3(leftAimPosition.x, leftArmPosition.y, leftAimPosition.z);
            leftAim.localPosition = leftAimPosition;
        }

        if (rightAim)
        {
            Vector3 rightAimPosition = rightAim.localPosition;
            rightAimPosition = new Vector3(rightAimPosition.x, rightArmPosition.y, rightAimPosition.z);
            rightAim.localPosition = rightAimPosition;
        }
    }
}
