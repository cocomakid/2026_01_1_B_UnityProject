using UnityEngine;

public class MyBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collisoion)
    {
        Debug.Log(collisoion.gameObject.name + " 와 충돌");

        if (collisoion.gameObject.tag == "Ground")
        {
            Debug.Log("땅과 충돌");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리어 안에 들어옴");
    }
    
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("트리어 밖으로 나감");
    }

}
