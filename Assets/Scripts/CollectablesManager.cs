using System.Collections;
using System.Collections.Generic;
using Sisus.Init;
using TMPro;
using UnityEngine;

public class CollectablesManager : MonoBehaviour<LevelGenerator> {
    [SerializeField] private int collectedSpecialMobility;
    [SerializeField] private int maxSpecialMobility;
    [SerializeField] private TextMeshProUGUI scorePointText;
    [SerializeField] private TextMeshProUGUI highscorePointText;
    [SerializeField] private TextMeshProUGUI specialMobilityText;
    [SerializeField] private int spawnIntervalSeconds;
    [SerializeField] private int maxExistingCollectables;
    [SerializeField] private List<Collectable> collectablePrefabs;
    private int _collectedScorePoints = 0;
    private int _highscore;
    private LevelGenerator _levelGenerator;

    void Start() {
        UpdateScoreUi();
        UpdateSpecialMobilityUi();
        StartCoroutine(SpawnRandomCollectable());
    }


    protected override void OnAwake() {
        base.OnAwake();
        _highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    protected override void Init(LevelGenerator levelGenerator) {
        _levelGenerator = levelGenerator;
    }


    public void Collect(CollectableType collectable) {
        switch (collectable) {
            case CollectableType.SCORE_POINT:
                CollectScorePoint();
                break;
            case CollectableType.SPECIAL_MOBILITY:
                CollectSpecialMobility();
                break;
        }
    }

    private void CollectSpecialMobility() {
        collectedSpecialMobility = Mathf.Min(collectedSpecialMobility + 1, maxSpecialMobility);
        UpdateSpecialMobilityUi();
    }

    public bool TryToUseSpecialMobility() {
        if (collectedSpecialMobility <= 0) {
            return false;
        }

        collectedSpecialMobility--;
        UpdateSpecialMobilityUi();
        return true;
    }

    private void CollectScorePoint() {
        _collectedScorePoints += 10;
        UpdateHighscore();
        UpdateScoreUi();
    }

    private void UpdateHighscore() {
        if (_collectedScorePoints > _highscore) {
            _highscore = _collectedScorePoints;
            PlayerPrefs.SetInt("highscore", _highscore);
        }
    }

    private void UpdateScoreUi() {
        scorePointText.text = _collectedScorePoints.ToString();
        highscorePointText.text = _highscore.ToString();
    }

    private void UpdateSpecialMobilityUi() {
        specialMobilityText.text = $"{collectedSpecialMobility}/{maxSpecialMobility}";
    }

    private IEnumerator SpawnRandomCollectable() {
        while (true) {
            while (transform.childCount < maxExistingCollectables) {
                Vector3 spawnPosition = _levelGenerator.GetRandomCubePosition();
                Collectable collectable = Instantiate(collectablePrefabs.PickRandom(), spawnPosition + Vector3.up, Quaternion.identity);
                collectable.transform.SetParent(transform);
            }

            yield return new WaitForSeconds(spawnIntervalSeconds);
        }
    }
}

public enum CollectableType {
    SCORE_POINT = 0,
    SPECIAL_MOBILITY = 1
}