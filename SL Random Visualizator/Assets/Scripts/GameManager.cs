using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private InputField _minLiveTime;
    [SerializeField] private InputField _maxLiveTime;
    [SerializeField] private InputField _minCooldown;
    [SerializeField] private InputField _maxCooldown;
    [SerializeField] private GameObject[] _figures;
    [SerializeField] private Toggle _square;
    [SerializeField] private Toggle _triangel;
    [SerializeField] private Toggle _circle;

    public GameObject Canvas;

    private float _lastTimeForSquare = 0f;
    private float _lastTimeForTriangel = 0f;
    private float _lastTimeForCircle = 0f;
    private float _cooldownForSquare;
    private float _cooldownForTriangel;
    private float _cooldownForCircle;
    private float _livetimeForSquare;
    private float _livetimeForTriangel;
    private float _livetimeForCircle;

    private float _minLT;
    private float _maxLT;
    private float _minCD;
    private float _maxCD;

    public void InputMinOrMaxForLivetime()
    {
        float.TryParse(_minLiveTime.text, out _minLT);
        float.TryParse(_maxLiveTime.text, out _maxLT);
        if(_minLT >=0 && _maxLT > 0)
        {
            _livetimeForSquare = Random.Range(_minLT, _maxLT);
            _livetimeForTriangel = Random.Range(_minLT, _maxLT);
            _livetimeForCircle = Random.Range(_minLT, _maxLT);
        }
    }
    public void InputMinOrMaxForCooldown()
    {
        float.TryParse(_minCooldown.text, out _minCD);
        float.TryParse(_maxCooldown.text, out _maxCD);
        if(_minCD >= 0 && _maxCD > 0)
        {
            _cooldownForSquare = Random.Range(_minCD, _maxCD);
            _cooldownForTriangel = Random.Range(_minCD, _maxCD);
            _cooldownForCircle = Random.Range(_minCD, _maxCD);
        }
    }
    private void CreateSquare()
    {
        if (_square.isOn && Time.time>(_lastTimeForSquare + _cooldownForSquare) && _cooldownForSquare != 0 && _livetimeForSquare !=0)
        {
            var square = Instantiate(_figures[0], new Vector2(Random.Range(-807, 264), Random.Range(-371, 357)), Quaternion.identity);
            square.transform.SetParent(Canvas.transform, false);
            _lastTimeForSquare = Time.time;
            _cooldownForSquare = Random.Range(_minCD, _maxCD);
            _livetimeForSquare = Random.Range(_minLT, _maxLT);
            square.GetComponent<Figure>().TimeOfLive = _livetimeForSquare;
        }
    }
    private void CreateTriangle()
    {
        if (_triangel.isOn && Time.time > (_lastTimeForTriangel + _cooldownForTriangel) && _cooldownForTriangel != 0 && _livetimeForTriangel !=0)
        {
            var triangel = Instantiate(_figures[1], new Vector2(Random.Range(-807, 264), Random.Range(-371, 357)), Quaternion.identity);
            triangel.transform.SetParent(Canvas.transform, false);
            _lastTimeForTriangel = Time.time;
            _cooldownForTriangel = Random.Range(_minCD, _maxCD);
            _livetimeForTriangel = Random.Range(_minLT, _maxLT);
            triangel.GetComponent<Figure>().TimeOfLive = _livetimeForTriangel;
        }
    }
    private void CreateCircle()
    {
        if (_circle.isOn && Time.time > (_lastTimeForCircle + _cooldownForCircle) && _cooldownForCircle != 0 && _livetimeForCircle !=0)
        {
            _livetimeForCircle = Random.Range(_minLT, _maxLT);
            var circle = Instantiate(_figures[2], new Vector2(Random.Range(-807, 264), Random.Range(-371, 357)), Quaternion.identity);
            circle.transform.SetParent(Canvas.transform, false);
            _lastTimeForCircle = Time.time;
            _cooldownForCircle = Random.Range(_minCD, _maxCD);
            _livetimeForCircle = Random.Range(_minLT, _maxLT);
            circle.GetComponent<Figure>().TimeOfLive = _livetimeForCircle;
        }
    }
    void Update()
    {
        CreateSquare();
        CreateTriangle();
        CreateCircle();
    }
}
