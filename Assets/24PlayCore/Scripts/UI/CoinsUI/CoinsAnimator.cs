using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinsAnimator : MonoSingleton<CoinsAnimator>
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private PoolObject coinPrefab;
    [SerializeField]
    private float radius;
    [SerializeField]
    private float delayTime;
    [SerializeField]
    private float popUpTime;
    [SerializeField]
    private float moveDelay;
    [SerializeField]
    private Ease popupEase;
    [SerializeField]
    private Ease moveEase;
    [SerializeField]
    private float moveSpeed = 1000f;

    public void Animate(Vector3 spawnPosition, System.Action onComplete)
    {
        StartCoroutine(AnimateRoutine(10, spawnPosition, onComplete));
    }

    private IEnumerator AnimateRoutine(int count, Vector3 spawnPosition, System.Action onComplete)
    {
        yield return new WaitForSecondsRealtime(delayTime);
        var coinsIEnumerators = new IEnumerator[count];
        for (int i = 0; i < count; i++)
        {
            var coin = PoolManager.Instance.ReuseObject(coinPrefab, target);
            coinsIEnumerators[i] = ProcessCoinRoutine(coin, spawnPosition, i + 1);
        }
        yield return this.WaitAll(coinsIEnumerators);
        onComplete?.Invoke();
    }

    private IEnumerator ProcessCoinRoutine(PoolObject coin, Vector3 spawnPosition, int waitFrames)
    {
        for (int i = 0; i < waitFrames; i++)
        {
            yield return null;
        }
        yield return PopUpCoinRoutine(coin, spawnPosition);
        yield return MoveToDestinationRoutine(coin);
        coin.Destroy();
    }

    private IEnumerator PopUpCoinRoutine(PoolObject coin, Vector3 spawnPosition)
    {
        var endPosition = spawnPosition + (Vector3)Random.insideUnitCircle * radius * GetScreenCoef();
        coin.transform.DOMove(endPosition, popUpTime).From(spawnPosition).SetEase(popupEase);
        yield return new WaitForSeconds(popUpTime);
        coin.transform.DOScale(1f, 2f * popUpTime).From(1.25f).SetEase(Ease.OutElastic);
    }

    private IEnumerator MoveToDestinationRoutine(PoolObject coin)
    {
        var distance = Vector3.Distance(coin.transform.position, target.position);
        var moveTime = distance / (moveSpeed * GetScreenCoef());
        coin.transform.DOMove(target.position, moveTime).SetEase(moveEase).SetDelay(moveDelay);
        yield return new WaitForSeconds(moveDelay + moveTime);
    }

    private float GetScreenCoef()
    {
        return (float)Screen.width / 1080;
    }
}
