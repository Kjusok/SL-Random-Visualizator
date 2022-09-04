using UnityEngine;
using UnityEngine.UI;

public class Figure : MonoBehaviour
{
    [SerializeField] private Slider _liveScale;
    [SerializeField] private Text _timeForDeath;
    [SerializeField] private ParticleSystem _destroyEffect;

    private SpriteRenderer _sr;

    private float _timeOfLive;
    private void Start()
    {
        _liveScale.maxValue = _timeOfLive;
        _sr = GetComponent<SpriteRenderer>();
    }
    private void SpawnDestroyEffect()
    {
        Vector2 figurePos = gameObject.transform.position;
        var spawnPosition = new Vector2(figurePos.x, figurePos.y);
        GameObject effect = Instantiate(_destroyEffect.gameObject, spawnPosition, Quaternion.identity);

        var mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = _sr.color;
        Destroy(effect, _destroyEffect.main.startLifetime.constant);
    }
    public void Initialize(float liveTimeForFigure)
    {
        _timeOfLive = liveTimeForFigure;
    }
    private void Update()
    {
        if (_timeOfLive > 0)
        {
            _timeOfLive -= Time.deltaTime;
            _liveScale.value = _timeOfLive;

            _timeForDeath.text = _timeOfLive.ToString("F1");

            if (_timeOfLive <= 0)
            {
                SpawnDestroyEffect();
                Destroy(gameObject);
            }
        }
    }
}
