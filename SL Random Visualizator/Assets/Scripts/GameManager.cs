using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum FigureType
{
    Square,
    Triangle,
    Circle 
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputField _minLifeTimeIputField;
    [SerializeField] private InputField _maxLifeTimeIputField;
    [SerializeField] private InputField _minCooldownIputField;
    [SerializeField] private InputField _maxCooldownIputField;
    [SerializeField] private Toggle _squareCheckBox;
    [SerializeField] private Toggle _triangleCheckBox;
    [SerializeField] private Toggle _circleCheckBox;
    [SerializeField] private RectTransform _gameBoard;

    [SerializeField] private Figure[] _figurePrefabs;

    private readonly Vector2 _minValue = new Vector2(0.1f, 0.1f);
    private readonly Vector2 _maxValue = new Vector2(0.95f, 0.9f);
    private Vector2 _gameBoardSize;
    private float _timerForFigure;
    private float _minLifeTime;
    private float _maxLifeTime;
    private float _minCooldown;
    private float _maxCooldown;

    private void Awake()
    {
        _minLifeTimeIputField.text = "5";
        _maxLifeTimeIputField.text = "5";
        _minCooldownIputField.text = "5";
        _maxCooldownIputField.text = "5";
        _gameBoardSize = _gameBoard.sizeDelta;
    }
    private void Start()
    {
        _minLifeTime = float.Parse(_minLifeTimeIputField.text);
        _maxLifeTime = float.Parse(_maxLifeTimeIputField.text);
        _minCooldown = float.Parse(_minCooldownIputField.text);
        _maxCooldown = float.Parse(_maxCooldownIputField.text);
        GetTypeOfSpawnFigureAndSpawn();
    }
    public void InputMinForLifeTime()
    {
        if (_minLifeTimeIputField.text == "")
        {
            _minLifeTime = 0;
            _minLifeTimeIputField.text = "0";
        }
        _minLifeTime = float.Parse(_minLifeTimeIputField.text);
        if (_minLifeTime < 0)
        {
            _minLifeTime = 0;
            _minLifeTimeIputField.text = "0";
        }
        if (_minLifeTime > _maxLifeTime)
        {
            _minLifeTime = _maxLifeTime;
            _minLifeTimeIputField.text = _maxLifeTimeIputField.text;
        }
    }
    public void InputMaxForLifeTime()
    {
        if (_maxLifeTimeIputField.text == "")
        {
            _maxLifeTime = 1;
            _maxLifeTimeIputField.text = "1";
        }
        _maxLifeTime = float.Parse(_maxLifeTimeIputField.text);
        if (_maxLifeTime <= 0)
        {
            _maxLifeTime = 1;
            _maxLifeTimeIputField.text = "1";
        }
        if (_maxLifeTime < _minLifeTime)
        {
            _maxLifeTime = _minLifeTime;
            _maxLifeTimeIputField.text = _minLifeTimeIputField.text;
        }
    }
    public void InputMinForCooldown()
    {
        if (_minCooldownIputField.text == "")
        {
            _minCooldown = 0;
            _minCooldownIputField.text = "0";
            _minCooldown = float.Parse(_minCooldownIputField.text);
        }
        _minCooldown = float.Parse(_minCooldownIputField.text);
        if (_minCooldown < 0)
        {
            _minCooldown = 0;
            _minCooldownIputField.text = "0";
        }
        if (_minCooldown > _maxCooldown)
        {
            _minCooldown = _maxCooldown;
            _minCooldownIputField.text = _maxCooldownIputField.text;
        }
    }
    public void InputMaxForCooldown()
    {
        if (_maxCooldownIputField.text == "")
        {
            _maxCooldown = 1;
            _maxCooldownIputField.text = "1";
            _maxCooldown = float.Parse(_maxCooldownIputField.text);
        }
        _maxCooldown = float.Parse(_maxCooldownIputField.text);
        if (_maxCooldown <= 0)
        {
            _maxCooldown = 1;
            _maxCooldownIputField.text = "1";
        }
        if (_maxCooldown < _minCooldown)
        {
            _maxCooldown = _minCooldown;
            _maxCooldownIputField.text = _minCooldownIputField.text;
        }
    }
    private void SpawnFigure(FigureType figureType)
    {
        var figure = Instantiate(_figurePrefabs[((int)figureType)],
                new Vector2(Random.Range(_minValue.x * _gameBoardSize.x, _maxValue.x * _gameBoardSize.x),
                Random.Range(_minValue.y * _gameBoardSize.y, _maxValue.y * _gameBoardSize.y)),
                Quaternion.identity);
        figure.transform.SetParent(_gameBoard.transform, false);

        _timerForFigure = Random.Range(_minCooldown, _maxCooldown);

        figure.Initialize(Random.Range(_minLifeTime, _maxLifeTime));
    }
    private void GetTypeOfSpawnFigureAndSpawn()
    {
        List<FigureType> figureAvailable = new List<FigureType>();

        if (_squareCheckBox.isOn)
        {
            figureAvailable.Add(FigureType.Square);
        }
        if (_triangleCheckBox.isOn)
        {
            figureAvailable.Add(FigureType.Triangle);
        }
        if (_circleCheckBox.isOn)
        {
            figureAvailable.Add(FigureType.Circle);
        }
        if (figureAvailable.Count > 0)
        {
            var figureType = figureAvailable[Random.Range(0, figureAvailable.Count)];
            Debug.Log(figureType);

            SpawnFigure(figureType);
        }
    }
    private void Update()
    {
        if (_timerForFigure > 0)
        {
            _timerForFigure -= Time.deltaTime;

            if (_timerForFigure <= 0)
            {
                GetTypeOfSpawnFigureAndSpawn();
            }
        }
        else
        {
            _timerForFigure = Random.Range(_minCooldown, _maxCooldown);
        }
    }
}
