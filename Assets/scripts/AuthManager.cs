using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class AuthManager : MonoBehaviour
{
    [Header("UI References")]
    public InputField usernameField;
    public InputField passwordField;
    public Button loginButton;
    public Button signupButton;

    private string baseUrl = "http://localhost:3000/api/auth"; // Backend URL

    void Start()
    {
        loginButton.onClick.AddListener(OnLoginClicked);
        signupButton.onClick.AddListener(OnSignupClicked);
    }

    void OnLoginClicked()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        StartCoroutine(LoginRequest(username, password));
    }

    void OnSignupClicked()
    {
        string username = usernameField.text;
        string password = passwordField.text;
        StartCoroutine(SignUpRequest(username, password));
    }

    IEnumerator LoginRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/login", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Login success: " + www.downloadHandler.text);
            // Optionally parse JSON and store token
        }
        else
        {
            Debug.LogError("Login failed: " + www.downloadHandler.text);
        }
    }

    IEnumerator SignUpRequest(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        UnityWebRequest www = UnityWebRequest.Post(baseUrl + "/signup", form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Signup success: " + www.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Signup failed: " + www.downloadHandler.text);
        }
    }
}
