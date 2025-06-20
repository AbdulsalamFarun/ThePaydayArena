using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

public class PlayerStateMachine : StateMachine
{

    [field: SerializeField] public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField] public CinemachineCamera targetCam { get; private set; }
    [field: SerializeField] public CinemachineCamera FreeLookCam { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public PlayerHealth PlayerHealth { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }

    [field: SerializeField] public float RotationDamping { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }


    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;
        SwitchState(new PlayerFreeLookState(this));
    }
}
