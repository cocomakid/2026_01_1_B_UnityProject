using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;                                // 최대 생명력
    public int curentLives;                                 // 현재 생명력

    public float invincibleTime = 1.0f;                     // 피격 후 무적 시간(반복 피격 방지)
    public bool isinvincible = false;                       // 무적 여부의 값

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curentLives = maxLives;                             // 생명력 초기화


    }

    private void OnTriggerEnter(Collider other)             // 트리거 영역 안에 들어왔나를 검사하는 함수
    {
        if (other.CompareTag("rocket"))                     // 미사일과 충돌 하면
        {
            curentLives--;
            Destroy(other.gameObject);                      // 미사일 오브젝트를 없앤다

            if (curentLives <= 0)                           // 체력이 0 이하일 경우
            {
                GameOver();                                 // 종료 함수를 호출한다
            }
        }
    }

    void GameOver()
    {
        gameObject.SetActive(false);                        // 플레이어 비활성화
        Invoke("RestartGame", 3.0f);                        // 3초 후 현재 씬 재시작
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);// 현재 씬 재시작
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
