using DG.Tweening;
using UnityEngine;

public class TweenCoin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 코인이 생성 되었을때 살짝 랜덤한 위치로 튀도록 목표 위치를 만든다
        Vector3 randomPosition = transform.position + new Vector3(
                Random.Range(-1f, 1f),
                0f,
                Random.Range(-1f, 1f)
            );

        // 코인이 바닥에 떨어지는 것처럼 점프 이동한다
        // DOJump (목표 위치, 점프 높이, 점프 횟수, 시간)
        transform.DOJump(randomPosition, 1.2f, 1, 0.4f).SetLink(gameObject);
        // SetLink는 오브젝트가 삭제 될 때 tween 도 같이 정리 되도록 도와준다

        // 코인이 떨어질 때 한 바퀴 회전하게 한다
        transform.DORotate(new Vector3(0f, 360f, 0f), 0.4f, RotateMode.FastBeyond360).SetLink(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
