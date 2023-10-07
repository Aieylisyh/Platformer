using System.Collections;
using UnityEngine;

public class ChatTrigger : MonoBehaviour
{
    public ChatPrototype chat;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChatSystem.instance.ShowChat(chat);
        }
    }
}