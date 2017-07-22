using UnityEngine;
using System.Collections;


public class EnemySpawner : MonoBehaviour {
#pragma warning disable 649
    [SerializeField]
	private float maxZSpawn;
    [SerializeField]
	private float minZSpawn;
    [SerializeField]
	private float xSpawn;
    [SerializeField]
	private float randomSpawnMinTime;
    [SerializeField]
	private float randomSpawnMaxTime;
    [SerializeField]
    private GameObject brownMonkeyPrefab;
    [SerializeField]
    private GameObject greenMonkeyPrefab;
    [SerializeField]
    private GameObject redMonkeyPrefab;
#pragma warning restore 649

    private float nextSpawnTime=0.0f;
	private float ySpawn = 0.4f;
    private float monkeyToSpawn;
	private Quaternion spawnRotation;

	private void Start () {
		spawnRotation = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f));
	}
	
	private void SetNextSpawnTime() {
		float spawnTimeInterval = Random.Range(randomSpawnMinTime, randomSpawnMaxTime);
		if (GvrViewer.Instance.VRModeEnabled) { 
	 	 spawnTimeInterval *= 1.1f;
		}

		nextSpawnTime = Time.time + spawnTimeInterval;
	}
	
    private GameObject DetermineMonkeyToSpawn()
    {
        monkeyToSpawn = Random.Range(0, 10);
        if(monkeyToSpawn >= 0&&monkeyToSpawn<=4)
        {
            return brownMonkeyPrefab;
        }
        else if(monkeyToSpawn >= 5 &&monkeyToSpawn<=8)
        {
            return greenMonkeyPrefab;
        }
        else
        {
            return redMonkeyPrefab;
        }
    }
	private void Update () {
		if (Time.time > nextSpawnTime && !GameMaster.Instance.getIsGameOver&&GameMaster.Instance.didFirstGameStart) {
           	Vector3 spawnPosition = new Vector3(xSpawn, ySpawn, Random.Range(minZSpawn, maxZSpawn));
			Instantiate(DetermineMonkeyToSpawn(), spawnPosition, spawnRotation);
			SetNextSpawnTime();
		}
	}
}
