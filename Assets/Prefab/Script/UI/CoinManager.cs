using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class CoinManager : MonoBehaviour
{
    public static CoinManager manage;
    public Transform rewardPrefab;
    public Transform destanition;
    public Transform parent;
    public int amount;

    private void Awake()
    {
        manage = this;
    }
    public void animate(int amount)
    {

        for (int i = 0; i < amount; i++)
        {

            Transform t = Instantiate(rewardPrefab, transform.position, Quaternion.identity, parent);
            Vector3 randomPosition = new Vector3(Random.Range(transform.position.x - 1, transform.position.x + 1),
                                                    Random.Range(transform.position.y - 1, transform.position.y + 1), 0);
            Sequence coinSquence = DOTween.Sequence();
            coinSquence.Append(t.DOMove(randomPosition, Random.Range(.3f, .6f)).SetEase(Ease.OutBack));
            coinSquence.Append(t.DOMove(destanition.position, Random.Range(.25f, .5f)).SetEase(Ease.Linear));
            coinSquence.OnComplete(() =>
            {
                Destroy(t.gameObject);
            });
        }
    }
}
