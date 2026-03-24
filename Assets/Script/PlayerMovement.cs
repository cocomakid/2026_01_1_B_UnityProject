using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;                                  // 이동 속도 변수 설정
    public float jumpForce = 5f;
    
    public Rigidbody rb;                                          // 플레이어 강체 선언

    public bool isGrounded = true;                                // 땅에 있는지 체크 하는 변수 (true/false)

    public int coinCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 움직임 입력
        float moveHorizontal = Input.GetAxis("Horizontal");         // 수평 이동
        float moveVertical = Input.GetAxis("Vertical");             // 수직 이동

        // 속도 값으로 직접 이동
        rb.linearVelocity = new Vector3 (moveHorizontal * moveSpeed, rb.linearVelocity.y, moveVertical * moveSpeed);

        if (Input.GetButtonDown("Jump") && isGrounded)              // 스페이스 버튼을 눌렀을때 와 isGrounded 가 True 일때
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);     // 위쪽으로 설정한 힘 만큼 물체에 힘을 준다
            isGrounded = false;                                         // 점프를 하는 순간 땅에서 떨어졌기 때문에 false로 한다
        }
    }

    private void OnCollisionEnter(Collision collision)              // 충돌 처리 함수
    {
        if (collision.gameObject.tag == "Ground")                   // 충돌이 일어난 물체의 Tag가 Ground 인 경우
        {
            isGrounded = true;                                      // 땅과 충돌하면 True로 변경한다
        }
    }

    private void OnTriggerEnter(Collider other)                     // 트리어 영역 안에 들어왔나를 검사하는 함수
    {
        if (other.CompareTag("Coin"))                               // 코인 트리거와 충돌 하면
            {
                coinCount++;                                        // 코인 변수 1을 올린다
                Destroy(other.gameObject);                          // 코인 오브젝트를 파괴한다
            }
    }
}
