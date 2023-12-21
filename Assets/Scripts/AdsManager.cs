using System.Collections;
using UnityEngine;
using Agava.YandexGames;

public class AdsManager : MonoBehaviour
{
    [SerializeField] private float _freezeScale = 2f;
    [SerializeField] private float _freezeTime = 3f;
    [SerializeField] private float _delayBetweenInterstitional;
    [SerializeField] private ReductionSlider _reductionSlider;
    [SerializeField] private GameObject _banner;

    private float _timesLeft = 0;

    private float _timesForSlider = 0;

    private bool _canBeShow = true;

    private void Update()
    {
        _timesLeft += Time.deltaTime;

        if (_timesLeft > _delayBetweenInterstitional)
        {
            _banner.SetActive(true);
            Time.timeScale = 0.5f;

            _timesForSlider += Time.deltaTime * 2;

            _reductionSlider.Set(_timesForSlider, _freezeTime);

            if (_canBeShow)
            {
                StartCoroutine(InterstitionalShow());
                _canBeShow = false;
            }
        }
    }

    public void StartAd()
    {
        _timesLeft = 0;
        Time.timeScale = 0.5f;

        _timesForSlider += Time.deltaTime * _freezeScale;

        _reductionSlider.Set(_timesForSlider, _freezeTime);

        StartCoroutine(InterstitionalShow());
    }

    private IEnumerator InterstitionalShow()
    {
        yield return new WaitForSeconds(_freezeTime/_freezeScale);


        if (YandexGamesSdk.IsInitialized)
            InterstitialAd.Show(Pause, InterstitialClose);
    }

    private void InterstitialClose(bool wasShown) => Resume();

    private void Pause()
    {
        AudioManager.Instance.PauseAll();
        Time.timeScale = 0f;
        _timesForSlider = 0;
    }

    private void Resume()
    {
        _timesLeft = 0;
        _canBeShow = true;
        _banner.SetActive(false);
        Time.timeScale = 1f;
        AudioManager.Instance.ResetAll();
    }
}