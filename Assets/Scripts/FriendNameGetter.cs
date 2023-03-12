using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//�����ںͺ����йصİ�ť��
public class FriendNameGetter : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        string frdname=transform.parent.Find("Name").gameObject.GetComponent<Text>().text;
        if (transform.parent.parent.parent.parent.parent.name=="AddPage")
        {
            transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<FriendsController>().addFriendname=frdname;
            transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<FriendsController>().addFriendObj=transform.parent.gameObject;
            transform.parent.parent.parent.parent.parent.parent.Find("Validate").gameObject.SetActive(true);
        }
        else if (transform.parent.parent.parent.parent.parent.name=="Friends")
        {
            if (transform.Find("Name").gameObject.GetComponent<Text>().text=="Delete")
            {
                transform.parent.parent.parent.parent.parent.gameObject
                    .GetComponent<FriendsController>().DeleteFriend(frdname, transform.parent.gameObject);
            }
            else if (transform.Find("Name").gameObject.GetComponent<Text>().text=="Invite")
            {

            }
            else if (transform.Find("Name").gameObject.GetComponent<Text>().text=="Message")
            {

            }
        }
        else if (transform.parent.parent.parent.parent.parent.name=="ApplicationPage")
        {
            if (transform.Find("Name").gameObject.GetComponent<Text>().text=="Agree")
            {
                //ͬ�����Ӻ�������
                string res = WebController.Post("http://127.0.0.1:8080/api/accept_friend/", JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    {"nickname1",transform.parent.Find("Name").gameObject.GetComponent<Text>().text },
                    {"nickname2",AllMessageContainer.playerInfo.playerName }
                }));
                Debug.Log(transform.parent.Find("Name").gameObject.GetComponent<Text>().text+" "+AllMessageContainer.playerInfo.playerName);

                if (res==WebController.ServerNotFound)
                {
                    transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<FriendsController>()
                        .ApplicationPageError("Network Error! Please check your network connection and try again later.");
                }
                else if(res==WebController.Success)
                {
                    transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<FriendsController>()
                        .AgreeSuccess(transform.parent.Find("Name").gameObject.GetComponent<Text>().text);
                    Destroy(transform.parent.gameObject);
                }
            }
            else if (transform.Find("Name").gameObject.GetComponent<Text>().text=="Reject")
            {
                //�ܾ����Ӻ���
                string res = WebController.Post("http://127.0.0.1:8080/api/reject_friend/", JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    {"nickname1",transform.parent.Find("Name").gameObject.GetComponent<Text>().text },
                    {"nickname2",AllMessageContainer.playerInfo.playerName }
                }));

                if (res==WebController.ServerNotFound)
                {
                    transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<FriendsController>()
                        .ApplicationPageError("Network Error! Please check your network connection and try again later.");
                }
                else if (res==WebController.Success)
                {
                    transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<FriendsController>()
                        .RejectSuccess(transform.parent.Find("Name").gameObject.GetComponent<Text>().text);
                    Destroy(transform.parent.gameObject);
                }
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}