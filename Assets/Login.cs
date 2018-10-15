using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;

public class Login : MonoBehaviour
{

    public static string email = "";
    public static string password = "";

    private FirebaseAuth auth;
    //private string createaccounturl = "";
    //private string loginurl = "";
    private string comfirmpassword = "";
    private string cpassword = "";
    private string cemail = "";


    public float X;
    public float Y;
    public float Width;
    public float Height;

    private string latitude;
    private string longitude;
    private string imgurl;

    public string currentmenu = "Login";


    void Start()
    {


        auth = FirebaseAuth.DefaultInstance;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        if (currentmenu == "Login")
        {
            LoginGUI();

        }
        else if (currentmenu == "CreateAccount")
        {
            CreateAccountGUI();
        }
        else if (currentmenu == "menu")
        {
            MenuGUI();
        }
        else if (currentmenu == "ads")
        {
            //ads
        }

    }

    void LoginGUI()
    {
        GUI.Box(new Rect(0, 0, (Screen.width), (Screen.height)), "Login");



        GUI.Label(new Rect(260, 150, 250, 25), "Email");
        email = GUI.TextField(new Rect(260, 175, 250, 25), email);

        GUI.Label(new Rect(260, 210, 250, 25), "Password");
        password = GUI.PasswordField(new Rect(260, 235, 250, 25), password, "*"[0], 10);

        if (GUI.Button(new Rect(250, 340, 120, 25), "Create Account"))
        {
            currentmenu = "CreateAccount";
        }

        if (GUI.Button(new Rect(450, 340, 120, 25), "Login"))
        {
            signIn(email, password);
            //currentmenu = "menu";
        }
    }

    void CreateAccountGUI()
    {
        GUI.Box(new Rect(0, 0, (Screen.width), (Screen.height)), "Create Account");



        GUI.Label(new Rect(260, 150, 250, 25), "Email");
        cemail = GUI.TextField(new Rect(260, 175, 250, 25), cemail);

        GUI.Label(new Rect(260, 210, 250, 25), "Password");
        cpassword = GUI.PasswordField(new Rect(260, 235, 250, 25), cpassword, "*"[0], 10);

        GUI.Label(new Rect(260, 260, 250, 25), "Comfirm Password");
        comfirmpassword = GUI.PasswordField(new Rect(260, 285, 250, 25), comfirmpassword, "*"[0], 10);

        if (GUI.Button(new Rect(250, 340, 120, 25), "Create Account"))
        {
            Debug.Log("creating");
            if (comfirmpassword == cpassword)
            {
                //  StartCoroutine();
                Debug.Log("equal");
                createAccountfire(cemail, cpassword);
                Debug.Log("Added");
            }
        }

        if (GUI.Button(new Rect(450, 340, 120, 25), "Back"))
        {
            currentmenu = "Login";
        }

    }

    void MenuGUI()
    {
        GUI.Box(new Rect(0, 0, (Screen.width), (Screen.height)), "Main Menu");

        if (GUI.Button(new Rect(300, 120, 120, 25), "Map"))
        {
            SceneManager.LoadScene("ARMapAdSupported");
        }

        if (GUI.Button(new Rect(300, 180, 120, 25), "Publish Ads "))
        {
            currentmenu = "ads";
        }

        if (GUI.Button(new Rect(300, 240, 120, 25), "Sign out"))
        {
            signout();
            currentmenu = "Login";
        }
    }

    void createAccountfire(string cemail, string cpassword)
    {
        //Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        auth.CreateUserWithEmailAndPasswordAsync(cemail, cpassword).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    void signIn(string kemail, string kpassword)
    {
        auth.SignInWithEmailAndPasswordAsync(kemail, kpassword).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            currentmenu = "menu";
        });
    }

    void signout()
    {
        auth.SignOut();
        currentmenu = "Login";
    }


}

