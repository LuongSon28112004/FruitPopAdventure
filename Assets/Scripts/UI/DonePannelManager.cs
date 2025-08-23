using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DonePanelManager : BaseMonoBehaviour
{
    [Header("WinPannelManager Components")]
    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private GameObject confetti;

    [SerializeField]
    private TMP_Text wellDoneText;

    [SerializeField]
    private GameObject[] stars;

    [SerializeField]
    private TMP_Text scoreText;

    [SerializeField]
    private Button replayButton; // đổi sang Image nếu chỉ hiển thị

    [SerializeField]
    private Button DoneButton;

    [SerializeField]
    private List<ParticleSystem> confettiEffects;
    [SerializeField]
    private Stars soStar;
    [SerializeField]
    private int starCountYellow;

    public int StarCountYellow { get => starCountYellow; set => starCountYellow = value; }

    protected override void Awake()
    {
        this.LoadSOStars();
    }
    private void LoadSOStars()
    {
        this.soStar = Resources.Load<Stars>("SO/Stars");
        if (soStar == null)
            throw new Exception("Khoong tim thay SO Stars");
    }

    public void Show(int score, bool isWin)
    {
        // // Dừng game
        // Time.timeScale = 0f;

        panel.SetActive(true);
        confetti.SetActive(true);

        // Reset trạng thái ban đầu
        if (isWin)
            wellDoneText.text = "COMPLETE";
        else
            wellDoneText.text = "FAIL";

        wellDoneText.transform.localScale = Vector3.zero;
        foreach (var star in stars)
            star.transform.localScale = Vector3.zero;
        scoreText.alpha = 0;
        replayButton.transform.localScale = Vector3.zero;
        DoneButton.transform.localScale = Vector3.zero;

        // Stop toàn bộ confetti nếu đang chạy
        foreach (var fx in confettiEffects)
        {
            var main = fx.main;
            main.simulationSpeed = 0.85f;
            fx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        // Timeline hiệu ứng
        Sequence seq = DOTween.Sequence();

        // Zoom chữ Well Done
        seq.Append(
            wellDoneText.transform.DOScale(1, 0.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true) // chạy khi Time.timeScale = 0
        );

        // Bật confetti ngay sau khi Well Done hiện ra
        seq.AppendCallback(() =>
        {
            foreach (var fx in confettiEffects)
                fx.Play();
        });

        // Thêm từng ngôi sao bung ra + rung nhẹ
        for (int i = 0; i < stars.Length; i++)
        {
            int index = i;
            if (i < starCountYellow)
            {
                stars[index].GetComponent<SpriteRenderer>().sprite = soStar.stars[0];
            }
            else stars[index].GetComponent<SpriteRenderer>().sprite = soStar.stars[1];
            seq.Append(
                stars[index]
                    .transform.DOScale(1, 0.4f)
                    .SetEase(Ease.OutBack)
                    .SetUpdate(true)
                    .OnComplete(() =>
                    {
                        stars[index].transform
                            .DOPunchScale(Vector3.one * 0.2f, 0.3f, 5, 0.5f)
                            .SetUpdate(true);
                    })
            );
        }

        // Hiện điểm số (fade in + chạy đếm từ 0 → score)
        seq.Append(scoreText.DOFade(1, 0.3f).SetUpdate(true));
        seq.AppendCallback(() =>
        {
            int currentScore = 0;
            DOTween.To(
                () => currentScore,
                x =>
                {
                    currentScore = x;
                    scoreText.text = "Score: " + currentScore.ToString("N0");
                },
                score,
                1.5f
            )
            .SetEase(Ease.Linear)
            .SetUpdate(true);
        });

        // Hiện nút replay
        seq.Append(
            replayButton.transform.DOScale(0.05f, 0.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true)
        );

        // Cuối cùng hiện nút Done
        seq.Append(
            DoneButton.transform.DOScale(0.05f, 0.4f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true)
        );
    }

    public void HideWin()
    {
        panel.SetActive(false);

        // Tắt confetti
        foreach (var fx in confettiEffects)
        {
            fx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        // Cho game chạy lại
        Time.timeScale = 1f;
    }
}
