using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("UI 요소 - 인스펙터 창에서 연결")]
    public GameObject DialoguePanel;                // 대화창 전체 패널
    public Image characterImage;                    // 캐릭터 이미지 표시하는 imageUI
    public TextMeshProUGUI characternameText;       // 캐릭터 이름 표시하는 텍스트
    public TextMeshProUGUI dialogueText;            // 대화 내용을 표시하는 텍스트
    public Button nextButton;                       // 다음 버튼

    [Header("기본 설정")]
    public Sprite defaultCharacterImage;           // 캐릭터 이미지가 없을때 사용할 기본 이미지

    [Header("타이핑 효과 설정")]                    
    public float typingSpeed = 0.05f;               // 글자 하나당 출력 속도
    public bool skipTypingOnClick = true;           // 클릭시 타이핑 즉시 완료 할 지 여부

    // 내부 변수들
    private DialogueDataSO currentDialogue;         // 현재 진행 중인 대화 데이터
    private int currentLineIndex = 0;                // 현재 몇 번째 대화 중인지
    private bool isDialogueActive = false;          // 대화가 진행 중인지 확인하는 플래그
    private bool isTyping = false;                  // 현재 타이핑 효과가 진행 중인지 확인
    private Coroutine typingCoroutine;              // 타이핑 효과 코루틴 등록

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DialoguePanel.SetActive(false);             // 대화창 숨기기
        nextButton.onClick.AddListener(HandleNextnput);         // 다음 버튼에 새로운 입력 처리 연걸
    }

    // Update is called once per frame
    void Update()
    {
        if (isDialogueActive && Input.GetKeyUp(KeyCode.Space))
        {
            HandleNextnput();                                   // 다음 입력 처리 (타이핑 중이면 완료, 아니면 다음줄)
        }
    }

    IEnumerator TypeText(string textToType)             // 타이핑 할 전체 텍스트
    {
        isTyping = true;                    // 타이핑 시작
        dialogueText.text = "";             // 텍스트 초기화

        // 텍스트를 한 글자씩 추가
        for (int i = 0; i < textToType.Length; i++)
        {
            dialogueText.text += textToType[i];             // 한글자씩 추가
            yield return new WaitForSeconds(typingSpeed);   // 대기 시간 설정
        }
        isTyping = false;
    }

    private void CompleteTyping()                   // 타이핑 효과를 즉시 완료 하는 함수
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);         // 코루틴 중지
        }

        isTyping = false;                           // 타이핑 상태 해제

        // 현재 줄의 전체 텍스트를 즉시 표시
        if (currentDialogue != null & currentLineIndex < currentDialogue.dialogueLines.Count)
	    {
            dialogueText.text = currentDialogue.dialogueLines[currentLineIndex];
	    }
    }

    void ShowCurrentLine()          // 현재 대화 줄의 내용을 타이핑 효과와 함께 화면에 표시하는 함수
    {
        if (currentDialogue != null && currentLineIndex < currentDialogue.dialogueLines.Count)       // 대화 데이터와 인덱스가 유효한지 확인
        {
            if(typingCoroutine != null)         // 이전 타이칭 효과가 있다면
            {
                StopCoroutine (typingCoroutine);
            }

            // 현재 줄의 대화 내용으로 타이핑 효과 시작
            string currentText = currentDialogue.dialogueLines [currentLineIndex];
            typingCoroutine = StartCoroutine(TypeText(currentText));
        }
    }

    public void ShowNextLine()      // 다음 대화 줄로 이동 시키는 함수 (타이핑이 완료된 후에만 호출)
    {
        currentLineIndex++;         // 다음줄로 인데스 증가

        // 마지막 대화 였는지 확인
        if (currentLineIndex >= currentDialogue.dialogueLines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();                                          // 대화가 남았으면 다음 줄 표시
        }
    }

    void EndDialogue()                      // 대화를 완전히 종료 하는 함수
    {
        if (typingCoroutine != null)                // 타이핑 효과 정리
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        isDialogueActive = false;               // 대화 비활성화
        isTyping = false;                       // 타이핑 상태 해제
        DialoguePanel.SetActive(false);         // 대화창 숨기기
        currentLineIndex = 0;                   // 인덱스 초기화
    }

    public void HandleNextnput()                // 스페이스바 or 버튼 클릭시 호출 되는 입력 처리 함수
    {
        if (isTyping && skipTypingOnClick)      // 타이핑 중이라면 즉시 완료
        {
            CompleteTyping();
        }
        else if (!isTyping)                     // 타이핑 완료 상태면 다음 줄로 
        {
            ShowNextLine();
        }
    }

    public void SkipDialogue()                  // 대화 전체를 바로 스킵하는 함수
    {
        EndDialogue();
    }

    public bool IsDialogueActive()              // 대화가 진행 중인지 확인 하는 함수
    {
        return isDialogueActive;
    }

    public void StartDialogue(DialogueDataSO dialogue)
    {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;

        // 대화 시작 준비
        currentDialogue = dialogue;             // 현재 대화 데이터 설정
        currentLineIndex = 0;                   // 첫 번째 대화 부터 시작
        isDialogueActive = true;                // 대화 활성 화 플래그 On

        // UI 업데이트
        DialoguePanel.SetActive(true);                      // 대화창 보이기
        characternameText.text = dialogue.characterName;    // 캐릭터 이름 표시

        if (characterImage != null)
        {   
            if (dialogue.characterImage != null)
            {
                characterImage.sprite = dialogue.characterImage;        // 대화 데이터의 이미지 사용
            }
            else
            {
                characterImage.sprite = defaultCharacterImage;          // 기본 이미지 사용
            }
        }

        ShowCurrentLine();                                              // 첫번째 대화 내용 표시    
    }
}
