using UnityEngine;
using Cinemachine;

public class Room : PlaceName
{
    public Enemy[] enemies;
    public Pot[] pots;
    public Door[] doors;
    public GameObject[] lights;
    public GameObject virtualCamera;
    public GameObject target;
    public Collider2D boundary;
    public bool camTrigger;
    private bool enemiesCleared = false;
    [SerializeField] private bool doorsOpened = false;
    [SerializeField] private bool inCombat;
    [SerializeField] private bool doNotSpawn = false;


    public virtual void Start()
    {
        virtualCamera = gameObject.transform.Find("CM vcam").gameObject;
        target = GameObject.FindWithTag("Player");
        camTrigger = false;
        CinemachineVirtualCamera vcam = virtualCamera.GetComponent<CinemachineVirtualCamera>();
        CinemachineConfiner confiner = virtualCamera.GetComponent<CinemachineConfiner>();
        confiner.m_BoundingShape2D = boundary;
        vcam.Follow = GameObject.FindWithTag("Player").transform;
        if (boundary.bounds.Contains(target.transform.position))
        {
            virtualCamera.SetActive(true);
            showText();
        }

        else
        {
            virtualCamera.SetActive(false);
            DeSpawnGameObjects();
        }

    }

    private void Update()
    {
        if (boundary.bounds.Contains(target.transform.position) && !camTrigger)
        {
            if (!virtualCamera.activeInHierarchy)
            {
                virtualCamera.SetActive(true);
            }
            if (!doorsOpened)
            {
                foreach (Door door in doors)
                {
                    if (door.thisDoorType == DoorType.enemy && enemiesCleared)
                    {
                        door.Open();
                        FindObjectOfType<AudioManager>().Play("door-open");
                        doorsOpened = true;
                    }
                }
            }

            if (inCombat)
            {
                if (checkRoomCleared() && doors.Length > 0 && hasEnemyDoor())
                {
                    enemiesCleared = true;
                    doNotSpawn = true;
                    inCombat = false;
                }
            }

        }
        else
            if (virtualCamera.activeInHierarchy)
            virtualCamera.SetActive(false);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            if (!doNotSpawn)
            {
                for (int i = 0; i < enemies.Length; i++)
                {
                    ChangeActivation(enemies[i], true);
                }
            }
           for(int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }
            showText();

            if (enemies.Length > 0 && enemies != null)
            {
                inCombat = true;
            }
        }
    }
    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], false);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], false);
            }
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(false);
            }
        }
    }

    public void ChangeActivation(Component component, bool activation)
    {
        component.gameObject.SetActive(activation);
    }

    protected void SpawnGameObjects()
    {
            for (int i = 0; i < enemies.Length; i++)
            {
                ChangeActivation(enemies[i], true);
            }
            for (int i = 0; i < pots.Length; i++)
            {
                ChangeActivation(pots[i], true);
            }
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }
    }

    protected void DeSpawnGameObjects()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            ChangeActivation(enemies[i], false);
        }
        for (int i = 0; i < pots.Length; i++)
        {
            ChangeActivation(pots[i], false);
        }
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
        }
    }

    protected bool checkRoomCleared()
    {
        bool isCleared = true;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy == true)
            {
                isCleared = false;
            }
        }
        return isCleared;
    }

    protected bool hasEnemyDoor()
    {
        bool hasEnemyDoor = false;
        foreach (Door door in doors)
        {
            if (door.thisDoorType == DoorType.enemy)
            {
                hasEnemyDoor = true;
            }
        }
        return hasEnemyDoor;
    }
}
