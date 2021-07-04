using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class ContactUs : MonoBehaviour
{
    DataLoader dataloader = new DataLoader();
    DataModel datamodel = new DataModel();
     //  string name, string email, string mobile,string subject, string bod
    //Text
    public Text Name, Email, Mobile, Subject, Body , errorMsg;
    public GameObject ContactPanal;
    public GameObject SubscribePopup;
    public GameObject CancelBtn;
    public GameObject CancelPopup;
    #region ContactUs
    public void ContactUsClick()
    {
        ContactPanal.SetActive(true);

        //if (SubscriptionController.Instance.SubscribeData.can_cancel)
        //{
        //    CancelBtn.SetActive(true);
        //}
    }

    public void ContactUsSend()
    {

        if(ContactValidation() != "sucsses")
        {
            errorMsg.text = ContactValidation();
            errorMsg.color = Color.red;
            return;
        }

        datamodel.DataBase_ContactUs(Name.text, Email.text, Mobile.text, Subject.text,Body.text);
        Clear();
        errorMsg.text = "Data sent successfully";
        errorMsg.color = Color.green;
    }

    public void Close()
    {
        ContactPanal.SetActive(false);
        Clear();
    }

    void Clear()
    {
        Name.text = "";
        Email.text = "";
        Mobile.text = "";
        Subject.text = "";
        Body.text = "";
    }
    #endregion

    private string ContactValidation()
    {
        string errorMsg;
        Debug.Log(Name.text);
        if (IsEmpty(Name.text) || IsEmpty(Email.text) || IsEmpty(Mobile.text) || IsEmpty(Subject.text) || IsEmpty(Body.text))
        {
            errorMsg = "Please fill all Data";
        }
        else
        {
            if (IsValidEmailAddress(Email.text))
            {
                errorMsg = "sucsses";
            }
            else
            {
                errorMsg = "Email is not Vaild";
            }
        }
        return errorMsg;
    }


     public bool IsValidEmailAddress(string s)
    {
        var regex = new Regex(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
        return regex.IsMatch(s);
    }

    public bool IsEmpty(string s)
    {
        if(s == "")
        {
            return true; 
        }
        else
        {
            return false;
        }
    }

    public void UnSubscribeBox()
    {
        SubscribePopup.SetActive(true);
    }

 
    public void Quit()
    {
        Application.Quit();
    }
}
