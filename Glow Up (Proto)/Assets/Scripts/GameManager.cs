using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class GameManager : SingletonDontDestroy<GameManager>
{
    public PlayerController player { get; private set; }
    public GlowBox currentGlowBox { get; set; }
    public GameObject healthIcon;
    public Transform healthIconsParent;

    public CinemachineVirtualCamera cmVcam { get; private set; }

    public static event Action OnGameStart;
    public static event Action OnGameEnd;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();

        OnGameStart += StartGame;

        OnGameEnd += Restart;
    }
    void Start()
    {
        GameStart();
    }

    public void ChangeCameraTarget(Transform target)
    {
        cmVcam.m_Follow = target;
    }
    
    public void GameStart()
    {
        OnGameStart?.Invoke();
    }
    public void GameEnd()
    {
        OnGameEnd?.Invoke();
    }
    private void StartGame()
    {
        player = FindObjectOfType<PlayerController>();
        currentGlowBox = FindObjectOfType<GlowBox>();

        cmVcam = FindObjectOfType<CinemachineVirtualCamera>();

        // Later on, place this as a function controlled by the event "GameStart"
        player.InstantiateHealthIcons(healthIcon, healthIconsParent);
    }
    private void Restart()
    {
        LevelLoader.instance.Reload();
        GameStart();
    }
}
