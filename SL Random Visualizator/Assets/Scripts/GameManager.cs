using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputField _minLifeTimeIputField;
    [SerializeField] private InputField _maxLifeTimeIputField;
    [SerializeField] private InputField _minCooldownIputField;
    [SerializeField] private InputField _maxCooldownIputField;
    [SerializeField] private Toggle _square;
    [SerializeField] private Toggle _triangle;
    [SerializeField] private Toggle _circle;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private RectTransform _gameBoard;

    [SerializeField] private GameObject[] _figurePrefabs;

    private Vector2 _minValue = new Vector2(x: 0.1f, y: 0.1f);
    private Vector2 _maxValue = new Vector2(x: 0.95f, y: 0.9f);
    private float _timerForFigure;
    private float _cooldownForFigure;
    private float _livetimeForFigure;
    private float _minLifeTime;
    private float _maxLifeTime;
    private float _minCooldown;
    private float _maxCooldown;
    private int _numberSpawnedFigure;

    private void Awake()
    {
        _minLifeTimeIputField.text = "5";
        _maxLifeTimeIputField.text = "5";
        _minCooldownIputField.text = "5";
        _maxCooldownIputField.text = "5";
    }
    public void InputMinForLifeTime()
    {
        _minLifeTime = float.Parse(_minLifeTimeIputField.text);
    }
    public void InputMaxForLifeTime()
    {
        _maxLifeTime = float.Parse(_maxLifeTimeIputField.text);
    }
    public void InputMinForCooldown()
    {
        _minCooldown = float.Parse(_minCooldownIputField.text);
    }
    public void InputMaxForCooldown()
    {
        _maxCooldown = float.Parse(_maxCooldownIputField.text);
    }
    private void SpawnSquare()
    {
        if (_square.isOn && _numberSpawnedFigure == 0)
        {
            SpawnFigure();
        }
    }
    private void SpawnTriangle()
    {
        if (_triangle.isOn && _numberSpawnedFigure == 1)
        {
            SpawnFigure();
        }
    }
    private void SpawnCircle()
    {
        if (_circle.isOn && _numberSpawnedFigure == 2)
        {
            SpawnFigure();
        }
    }
    private void SpawnFigure()
    {
        var figure = Instantiate(_figurePrefabs[_numberSpawnedFigure],
                new Vector2(Random.Range(_minValue.x * _gameBoard.sizeDelta.x, _maxValue.x * _gameBoard.sizeDelta.x),
                Random.Range(_minValue.y * _gameBoard.sizeDelta.y, _maxValue.y * _gameBoard.sizeDelta.y)), Quaternion.identity);
        figure.transform.SetParent(_gameBoard.transform, false);
        _cooldownForFigure = Random.Range(_minCooldown, _maxCooldown);
        _livetimeForFigure = Random.Range(_minLifeTime, _maxLifeTime);
        _timerForFigure = _cooldownForFigure;
        figure.GetComponent<Figure>().TimeOfLive = _livetimeForFigure;
    }
    void Update()
    {
        _timerForFigure -= Time.deltaTime;

        if (_timerForFigure <= 0 &&
            _maxLifeTime >= _minLifeTime &&
            _maxCooldown >= _minCooldown &&
            _minLifeTime >= 0 &&
            _minCooldown >= 0)
        {
            _numberSpawnedFigure = Random.Range(0, 3);
            SpawnSquare();
            SpawnTriangle();
            SpawnCircle();
        }
    }
}
