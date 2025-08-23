using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StarsEffect : BaseMonoBehaviour
{
    [SerializeField]
    private List<GameObject> stars; // Gán 3 ngôi sao trong inspector

    [SerializeField]
    private Stars soStars;

    protected override void Awake()
    {
        base.Awake();
        this.LoadStars();
        this.LoadSOStars();
    }

    private void LoadSOStars()
    {
        this.soStars = Resources.Load<Stars>("SO/Stars");
        if (soStars == null)
            throw new Exception("Khoong tim thay SO Stars");
    }

    private void LoadStars()
    {
        foreach (Transform child in transform) // duyệt qua các Transform con
        {
            stars.Add(child.gameObject); // lấy GameObject của từng Transform
        }
    }

    public void PlayStarEffect(int starCount)
    {
        StartCoroutine(ShowStars(starCount));
    }

    private IEnumerator ShowStars(int starCount)
    {
        for (int i = 0; i < stars.Count; i++)
        {
            stars[i].transform.localScale = Vector3.zero; // ẩn hết ban đầu
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < stars.Count; i++)
        {
            GameObject star = stars[i];
            if (i < starCount)
            {
                star.GetComponent<Image>().sprite = soStars.stars[0];
            }
            else star.GetComponent<Image>().sprite = soStars.stars[1];

            // Bật sao
            star.SetActive(true);

            // Hiệu ứng phóng to + nảy nhẹ
            star.transform.DOScale(0.4f, 0.4f)
                .SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    star.transform.DOScale(0.4f, 0.2f);
                    star.transform.DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f);
                });
            yield return new WaitForSeconds(0.5f); // delay giữa các sao
        }
    }
}
