using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class CollectablesManager : MonoBehaviour
{

    public static CollectablesManager Instance;
    private int _collectedScorePoints = 0;
    private int _highscore;
    [SerializeField] private int _collectedSpaceJumps;
    [SerializeField] private int _maxSpaceJumps;
    [SerializeField] private TextMeshProUGUI _scorePointText;
    [SerializeField] private TextMeshProUGUI _highscorePointText;
    [SerializeField] private TextMeshProUGUI _spaceJumpsText;
    [SerializeField] private int _spawnIntervalSeconds;
    [SerializeField] private int _maxExistingCollectables;
    [SerializeField] private List<Collectable> _collectablePrefabs;


    void Awake()
    {
        Instance = this;
        _highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    void Start()
    {
        UpdateScoreUi();
        UpdateSpaceJumpsUi();
        StartCoroutine(SpawnRandomCollectable());
    }


    public void Collect(CollectableType collectable)
    {
        if (collectable == CollectableType.SCORE_POINT)
        {
            CollectScorePoint();
        }
        else if (collectable == CollectableType.SPACE_JUMP)
        {
            CollectSpaceJump();
        }
    }

    private void CollectSpaceJump()
    {
        _collectedSpaceJumps = Mathf.Min(_collectedSpaceJumps + 1, _maxSpaceJumps);
        UpdateSpaceJumpsUi();
    }

    public bool TryToUseSpaceJump()
    {
        if (_collectedSpaceJumps <= 0)
        {
            return false;
        }
        _collectedSpaceJumps--;
        UpdateSpaceJumpsUi();
        return true;
    }

    private void CollectScorePoint()
    {
        _collectedScorePoints += 10;
        UpdateHighscore();
        UpdateScoreUi();
    }

    private void UpdateHighscore()
    {
        if(_collectedScorePoints > _highscore)
        {
            _highscore = _collectedScorePoints;
            PlayerPrefs.SetInt("highscore", _highscore);
        }
    }

    private void UpdateScoreUi()
    {
        _scorePointText.text = _collectedScorePoints.ToString();
        _highscorePointText.text = _highscore.ToString();
    }

    private void UpdateSpaceJumpsUi()
    {
        _spaceJumpsText.text = $"{_collectedSpaceJumps}/{_maxSpaceJumps}";
    }

    private IEnumerator SpawnRandomCollectable()
    {
        while (true)
        {
            while (transform.childCount < _maxExistingCollectables)
            {
                Vector3 spawnPosition = LevelGenerator.Instance.GetRandomCubePosition();
                Collectable collectable = Instantiate(_collectablePrefabs.PickRandom(), spawnPosition + Vector3.up, Quaternion.identity);
                collectable.transform.SetParent(this.transform);
            }
            yield return new WaitForSeconds(_spawnIntervalSeconds);
        }
    }

}

public enum CollectableType
{
    SCORE_POINT = 0,
    SPACE_JUMP = 1
}