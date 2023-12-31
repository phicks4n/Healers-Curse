using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speed : MonoBehaviour, IDataPersistence
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float prevSpeed;
    [SerializeField] private TMP_Text speed;

    private void Start()
    {
        currentSpeed = 20;
        prevSpeed = 0;
        this.speed.SetText(currentSpeed.ToString());
    }

    public void AddSpeed(int speed)
    {
        Reduce(prevSpeed);
        prevSpeed = speed;
        currentSpeed += speed;
        DataPersistenceManager.instance.SavePlayerStat(10, (int)currentSpeed);
        this.speed.SetText(currentSpeed.ToString());
    }

    public void Reduce(float prevSpeed)
    {
        currentSpeed -= prevSpeed;
        this.speed.SetText(currentSpeed.ToString());
    }

    public void SaveData(GameData data) 
    {
        data.speed = (int)currentSpeed;
    }
    public void LoadData(GameData data) 
    {
        currentSpeed = data.speed;
    }
}
