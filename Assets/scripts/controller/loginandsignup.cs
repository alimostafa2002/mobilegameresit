using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginAndSignup : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_Text emailInfo;
    public TMP_Text jwtInfo;
    public Button registerButton;
    public Button loginButton;

    private UserService userService;

    void Start()
    {
        userService = new UserService();

        registerButton.onClick.AddListener(OnRegisterClick);
        loginButton.onClick.AddListener(OnLoginClick);
    }

    public void OnRegisterClick()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            emailInfo.text = "Please enter both email and password.";
            return;
        }

        User newUser = new User { email = email, password = password };
        StartCoroutine(userService.Register(newUser,
            success => { emailInfo.text = "Registration successful!"; },
            error => { emailInfo.text = "Registration failed: " + error; }
        ));
    }

    public void OnLoginClick()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            jwtInfo.text = "Please enter both email and password.";
            return;
        }

        User newUser = new User { email = email, password = password };
        StartCoroutine(userService.Login(newUser,
            success =>
            {
                jwtInfo.text = "JWT Token: " + success;
                SceneManager.LoadScene("mainmenu");
            },
            error => { jwtInfo.text = "Login failed: " + error; }
        ));
    }

    // === Inner service and data classes ===
    [System.Serializable]
    public class User
    {
        public string email;
        public string password;
    }

    public class UserService
    {
        private string registerUrl = "http://localhost:8000/api/users/register";
        private string loginUrl = "http://localhost:8000/api/users/login";

        public IEnumerator Register(User user, System.Action<string> onSuccess, System.Action<string> onError)
        {
            yield return SendPostRequest(registerUrl, user, onSuccess, onError);
        }

        public IEnumerator Login(User user, System.Action<string> onSuccess, System.Action<string> onError)
        {
            yield return SendPostRequest(loginUrl, user, onSuccess, onError);
        }

        private IEnumerator SendPostRequest(string url, User user, System.Action<string> onSuccess, System.Action<string> onError)
        {
            string json = JsonUtility.ToJson(user);
            using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    onError?.Invoke(request.error);
                }
                else
                {
                    onSuccess?.Invoke(request.downloadHandler.text);
                }
            }
        }
    }
}
