using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else if (this != Instance)
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private int _timeBetweenWave;
    [SerializeField]
    private int _creditPerWave;

    [SerializeField]
    private List<GameObject> _spawners;

    [Serializable]
    public struct EnemySpawnInfos
    {
        public GameObject Prefab;
        public float Cost;
        public int Reward;
        public float LevelUpStatMultiplier;
        public float MinSize;
        public float MaxSize;
    }

    [SerializeField]
    private List<EnemySpawnInfos> _enemiesInfos;

    [SerializeField]
    private GameObject _continueScreen;
    [SerializeField]
    private int _continueCost;

    [SerializeField]
    private GameObject[] _pickups;

    private bool _canWatchAdd = true;

    private bool _waitingForNextWave = false;

    private List<GameObject> _currentWave = new List<GameObject>(MAX_SPAWN_NUMBER);
    private GameObject _player;

    private int _playerCurrentHardCurrency;
    public int PlayerCurrentHardCurrency
    {
        get { return _playerCurrentHardCurrency; }
        set
        {
            _playerCurrentHardCurrency = value;
            PlayerUI.Instance.NotifyHardCurrency(_playerCurrentHardCurrency);
        }
    }
    private int _playerCurrentSoftCurrency;
    public int PlayerCurrentSoftCurrency
    {
        get { return _playerCurrentSoftCurrency; }
        set
        {
            _playerCurrentSoftCurrency = value;
            PlayerUI.Instance.NotifySoftCurrency(_playerCurrentSoftCurrency);
        }
    }

    private int _waveCount = 0;
    
    private const int MAX_SPAWN_NUMBER = 20;


    private void SpawnWave()
    {
        int credits = _waveCount * _creditPerWave;
        List<GameObject> availableSpawner = _spawners.GetOutOfSight(_player);
        int currentSpawnCount = 0;
        while (credits != 0)
        {
            int creditsCost = (int)Mathf.Ceil((float)credits / (MAX_SPAWN_NUMBER - currentSpawnCount));
            GameObject spawner = availableSpawner[UnityEngine.Random.Range(0, availableSpawner.Count)];
            EnemySpawnInfos bestChoice = _enemiesInfos[0];
            foreach (EnemySpawnInfos info in _enemiesInfos)
            {
                if(creditsCost%info.Cost == 0)
                {
                    if(bestChoice.Cost < info.Cost)
                    {
                        bestChoice = info;
                    }
                }
            }
            // Safe cast as creditsCost%info.Cost == 0
            int level = (int)(creditsCost / bestChoice.Cost);
            GameObject enemy = Instantiate(bestChoice.Prefab, spawner.transform.position, Quaternion.identity);
            enemy.transform.localScale = enemy.transform.localScale * UnityEngine.Random.Range(bestChoice.MinSize, bestChoice.MaxSize);
            enemy.GetComponent<AIAgent>().LevelUp(level, bestChoice.LevelUpStatMultiplier);
            enemy.GetComponent<EntityHealth>().SetDeathCallback(() => 
            {
                _currentWave.Remove(enemy);
                Destroy(enemy);
                PlayerCurrentSoftCurrency += bestChoice.Reward * level;
            });
            _currentWave.Add(enemy);
            currentSpawnCount++;
            credits -= creditsCost;
        }
    }

    public void ResetGame()
    {
        // only let the player watch an add the first continue
        _canWatchAdd = false;
        _continueCost *= 2;
        _player.GetComponent<EntityHealth>().Heal(int.MaxValue);
        while(_currentWave.Count > 0)
        {
            _currentWave[0].GetComponent<EntityHealth>().TakeDamage(int.MaxValue);
        }
    }


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("NewWaveCoroutine");
        PlayerCurrentSoftCurrency = 0;
        PlayerCurrentHardCurrency = 0;
        _player.GetComponent<EntityHealth>().SetDeathCallback(() =>
        {
            ShowContinueScreen();
        });
        _pickups = Resources.LoadAll<GameObject>("Prefabs/Pickups");

    }

    private void ShowContinueScreen()
    {
        _continueScreen.GetComponent<ContinueScreen>().Display(_continueCost, _canWatchAdd);
    }

    public void CollectSoftCurrency(int coinPerWave)
    {
        PlayerCurrentSoftCurrency += coinPerWave*_waveCount;
    }

    public void CollectHardCurrency(float starPerWave)
    {
        PlayerCurrentHardCurrency += Mathf.Max(1, (int)(starPerWave*_waveCount));
    }

    public void EndGame()
    {
        PlayerWallet.Instance.AddSoftCurrency(_playerCurrentSoftCurrency);
        PlayerWallet.Instance.AddHardCurrency(_playerCurrentHardCurrency);
        _canWatchAdd = true;
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(!_waitingForNextWave)
        {
            if(_currentWave.Count == 0)
            {
                StartCoroutine("NewWaveCoroutine");
            }
            else
            {
                PlayerUI.Instance.ChangeWaveInfoMessage($"Remaining enemies : {_currentWave.Count}");
            }
        }
    }

    IEnumerator NewWaveCoroutine()
    {
        _waitingForNextWave = true;
        _waveCount++;
        for(int i = _timeBetweenWave; i!=0;i--)
        {
            PlayerUI.Instance.ChangeWaveInfoMessage($"Next wave in {i}");
            yield return new WaitForSecondsRealtime(1);
        }
        SpawnWave();
        _waitingForNextWave = false;
    }
}
