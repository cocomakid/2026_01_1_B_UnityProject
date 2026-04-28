using UnityEngine;

public class ZAxisMover : MonoBehaviour
{
    public float speed = 5.0f;                                  // 이동 속도
    public float timer = 5.0f;                                  // 타이머 설정

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);

        timer -= Time.deltaTime;                                // 시간을 카운트 다운 한다

        if (timer < 0)
        {
            Destroy(gameObject);                                // 자기 자신을 파괴 한다
        }
    }
}
