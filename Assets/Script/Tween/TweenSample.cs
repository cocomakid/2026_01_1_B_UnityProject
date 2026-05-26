using UnityEngine;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

public class TweenSample : MonoBehaviour
{
    [Header("펀치 스캐일 예시")]
    public RectTransform punchUITarget;                         // UI 타겟
    public GameObject punchObjectTarget;                        // 오브젝트 타겟

    [Header("숫자 연출 에시")]
    public TMP_Text countText;
    public int currentValue = 0;
    public int addValue = 100;

    private int targetValue;

    [Header("색 변형 연출 예시")]
    public Color flashColor = Color.yellow;

    private Color originalColor;

    [Header("페이드 UI 그룹")]
    public CanvasGroup fadeTarget;

    void Start()
    {
        originalColor = countText.color;
        fadeTarget.alpha = 0f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayPunchUIScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayPunchObjectScale();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayUIShake();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            PlayCountUP();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            PlayColorFlash();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            PlayFade();
        }
    }

    public void PlayPunchUIScale()
    {
        if (punchUITarget == null)
            return;

        punchUITarget.DOKill(); // 이전 실행 중이던 Tween 이 있으면 정리한다
        punchUITarget.localScale = Vector3.one; // 크기가 이상하게 남아 있을 수 있으므로 기본 크기로 초기화 한다
        punchUITarget.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);    // 방향, 크기, 시간, 진동 횟수, 탄성
    }

    public void PlayPunchObjectScale()
    {
        if (punchObjectTarget == null)
            return;

        punchObjectTarget.transform.DOKill(); // 이전 실행 중이던 Tween 이 있으면 정리한다
        punchObjectTarget.transform.localScale = Vector3.one; // 크기가 이상하게 남아 있을 수 있으므로 기본 크기로 초기화 한다
        punchObjectTarget.transform.DOPunchScale(Vector3.one * 0.3f, 0.25f, 8, 1.0f);    // 방향, 크기, 시간, 진동 횟수, 탄성
    }

    public void PlayUIShake()
    {
        if (punchUITarget == null)
            return;
        punchUITarget.DOKill(); // 이전 실행 중이던 Tween 이 있으면 정리한다
        punchUITarget.DOShakeAnchorPos(0.3f, 20f, 20, 90f);    // 시간, 강도, 진동 횟수, 랜덤성
    }

    public void PlayCountUP()
    {
        if (countText == null)
            return;

        int startValue = currentValue;                          // 시작 숫자
        targetValue += addValue;              // 목표 숫자

        DOTween.Kill("CountTween", true);                             // 기존 숫자 Tween 이 남아 잇으면 겹치니 정리한다

        // DOTween.to는 숫자처럼 직접 Tween 함수가 없는 값을 움직일 때 사용한다
        DOTween.To(
            () => currentValue,                                   // 현재 값
            value =>                                            // 중간 값이 바뀔때 실행 되는 부분
            {
                currentValue = value;
                countText.text = currentValue.ToString();
            },
            targetValue,                                        // 목표 값
            0.5f                                                // 걸리는 시간
        )
        .SetEase(Ease.OutQuad)
        .SetId("CountTween");
    }

    public void PlayColorFlash()
    {
        if (countText == null)
            return;

        countText.DOKill();         // 이전 Tween 정리

        countText.color = originalColor;    // 이전 Tween 중간 색상이 남았을 수 있으므로 원래 색으로 초기화

        countText.DOColor(flashColor, 0.1f)
            .OnComplete(() =>
            {
                countText.DOColor(originalColor, 0.2f);     // 완료 되면 원래 색으로 되돌린다
            });
    }

    public void PlayFade()
    {
        if (fadeTarget == null)
            return;

        fadeTarget.DOKill();            // 이전 연출 정리
        fadeTarget.alpha = 0f;          // 처음에는 안보이게 설정

        DG.Tweening.Sequence seq = DOTween.Sequence();      // 여러 Tween을 순서대로 실행 할 때 사용 한다

        seq.Append(fadeTarget.DOFade(1f, 0.2f));        // 0.2초 동안 나타난다
        seq.AppendInterval(0.5f);                       // 0.5초 동안 유지한다
        seq.Append(fadeTarget.DOFade(0f, 0.3f));        // 0.3초 동안 사라진다
    }

}
