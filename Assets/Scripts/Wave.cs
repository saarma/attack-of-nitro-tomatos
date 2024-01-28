using UnityEngine;

[System.Serializable]
public class Wave
{
    public Enemy[] tomatoes;
    public float timeToNextEnemy;
    public float timeToNextWave;
    [HideInInspector] public int enemiesLeft;

    public bool FinalWave = false;

    public string WaveDescription = "";

}
