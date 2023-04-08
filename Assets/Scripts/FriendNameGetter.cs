using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//挂载在和好友有关的按钮上
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
                OnlineMode.inviteWhoToPlay = transform.parent.name;
                transform.parent.parent.parent.parent.parent.gameObject.GetComponent<OnlineMode>().PressInviteButton();
            }
            else if (transform.Find("Name").gameObject.GetComponent<Text>().text=="Message")
            {
                FriendsController.lookWhoMessage = frdname;
                FriendsController.messageButtonHandler = transform.gameObject;
                transform.parent.parent.parent.parent.parent.gameObject.GetComponent<FriendsController>().PressMessageButton();
            }
        }
        else if (transform.parent.parent.parent.parent.parent.name=="ApplicationPage")
        {
            if (transform.Find("Name").gameObject.GetComponent<Text>().text=="Agree")
            {
                //同意添加好友请求
                string res = WebController.Post(WebController.rootIP + API_Local.acceptFriend, JsonConvert.SerializeObject(new Dictionary<string, string>
                {
                    {"nickname1",transform.parent.Find("Name").gameObject.GetComponent<Text>().text },
                    {"nickname2",AllMessageContainer.playerInfo.playerName }
                }));

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
                //拒绝添加好友
                string res = WebController.Post(WebController.rootIP + API_Local.rejectFrient, JsonConvert.SerializeObject(new Dictionary<string, string>
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
        else if(transform.parent.parent.parent.parent.parent.name == "InvitationPage")
        {
            OnlineMode.operateWhoInv = transform.parent.Find("Name").gameObject.GetComponent<Text>().text;
            if(transform.name == "Agree")
            {
                transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<OnlineMode>().AgreeInvite();
            }
            else if(transform.name == "Reject")
            {
                transform.parent.parent.parent.parent.parent.parent.gameObject.GetComponent<OnlineMode>().DisagreeInvite();
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
