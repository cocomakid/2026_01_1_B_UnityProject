using NUnit.Framework.Internal;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public CubeGenerator[] generatedCubes = new CubeGenerator[5];                   // 클래스 배열

    public float timer = 0.0f;                                                      // 시간 타이머 설정 float
    public float interval = 3.0f;                                                   // 3초마다 땅 생성

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;                                                    // 타이머 시간을 늘린다
        if (timer >= interval)                                                      // 인터벌 시간 이상일 때
        {
            RandomizeCubeActivation();                                              // 함수 호출
            timer = 0.0f;                                                           // 타이머 초기화
        }
    }

    public void RandomizeCubeActivation()
    {
        for (int i = 0; i < generatedCubes.Length; i++)                             // 각 큐브를 랜덤하게 활성
        {
            int randomNum = Random.Range(0, 2);                                     // 랜덤 : 0 또는 1

            if (randomNum == 1)
            {
                generatedCubes[i].GenCube();                                        // 큐브 클래스의 생성 함수를 호출
            }
        }
    }
}
