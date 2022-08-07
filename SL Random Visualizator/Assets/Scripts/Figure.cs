using UnityEngine;
using UnityEngine.UI;

public class Figure : MonoBehaviour
{
    [SerializeField] private Slider _liveScale;
    [SerializeField] private Text _timeForDeath;
    [SerializeField] private ParticleSystem _destroyEffect;

    private SpriteRenderer _sr;

    public float TimeOfLive;
    void Start()
    {
        _liveScale.maxValue = TimeOfLive;
        this._sr = this.GetComponent<SpriteRenderer>();
    }
    private void SpawnDestroyEffect()
    {
        Vector2 figurePos = gameObject.transform.position;
        Vector2 spawnPosition = new Vector2(figurePos.x, figurePos.y);
        GameObject effect = Instantiate(_destroyEffect.gameObject, spawnPosition, Quaternion.identity);

        var mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this._sr.color;
        Destroy(effect, _destroyEffect.main.startLifetime.constant);
    }

    void Update()
    {
        if (TimeOfLive > 0)
        {
            TimeOfLive -= Time.deltaTime;
            _liveScale.value = TimeOfLive;

            _timeForDeath.text = TimeOfLive.ToString("F1");
        }
        if (TimeOfLive <= 0)
        {
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
    }
}
