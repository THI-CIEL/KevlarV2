using LTX.Singletons;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    [field : SerializeField] public List<Transform> checkPoints {  get; private set; }
    [field : SerializeField] public Transform player { get; private set; }
    public Transform currentCheckPoint { get; private set; }

    private void Start()
    {
        currentCheckPoint = checkPoints[0];
    }

    public void ChangeCP(int index)
    {
        int cpIndex = checkPoints.IndexOf(currentCheckPoint);
        currentCheckPoint = checkPoints[cpIndex + index];
    }
    public void SetCP(int index)
    {
        currentCheckPoint = checkPoints[index];
    }
}
