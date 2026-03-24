using UnityEngine;
using UnityEngine.UI;

public class MyJump : MonoBehaviour
{
    public Rigidbody rigidbody;                                 // 강체 (형태와 크기가 고정된 고체) 물리 현상이 동작 하게 해주는 컴포넌트
    public float power = 20f;                                   // 변수 힘을 선언 함
    public float timer = 0;
    public Text TextUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.time;                              // 타이머를 상승 시킨다.
        TextUI.text = timer.ToString();                         // 타이머 숫자를 문자열 변수로 변경한 후 표시 한다.

        if (Input.GetKeyDown(KeyCode.Space))                    // 스페이스 키를 눌렀을때
        {
            power = power + Random.Range(-100, 200);            // power 를 랜덤으로 변경 시킨다. ( -100 ~ 200 ) 사이의 값을 더한다.
            rigidbody.AddForce(transform.up * power);           // 변수 (power)의 위쪽으로 힘을 준다.
        }

        if (this.gameObject.transform.position.y > 5 || this.gameObject.transform.position.y < -3)
        {
            // 오브젝트의 y 좌표점 위치가 5보다 크거나 -3보다 작을 경우
            Destroy(this.gameObject);   // 오브젝트 삭제
        }
    }
}
