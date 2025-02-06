using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;

public static class WebRequestHandler
{
    /// <summary>
    /// Generic methode for get requests
    /// </summary>
    /// <typeparam name="TResoult">Object type</typeparam>
    /// <param name="url">Url to request location</param>
    /// <returns>Parsed json into object of given type</returns>
    public static async Task<TResoult> GetRequest<TResoult>(string url)
    {
        try
        {
            using var request = UnityWebRequest.Get(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            var response = request.downloadHandler.text;

            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log($"Request succesful");
            else
                Debug.LogError($"Request at {url} :: failed: {request.error}");

            try
            {
                return JsonUtility.FromJson<TResoult>(response);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Can't parse json!");
                Debug.LogError($"Error: {e.Message}");
                return default;
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error: " + e.Message);
            throw;
        }
    }

    /// <summary>
    /// Generic methode for post requests
    /// </summary>
    /// <param name="url">Url to request location</param>
    /// <param name="jsonData">Data body of the request in form of json string</param>
    /// <returns>Returns true if request was successful</returns>
    public static async Task<bool> PostRequest(string url, string jsonData)
    {
        try
        {
            using var request = new UnityWebRequest(url, "POST");
            byte[] conntent = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(conntent);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();


            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Request sent!");
                return true;
            }
            else
            {
                Debug.LogError($"Request at {url} :: failed: {request.error}");
                return false;
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error: " + e.Message);
            throw;
        }
    }

    public static async Task<bool> PathchRequest(string url, string endpoint, int id, string jsonData)
    {
        try
        {
            string urlWithId = $"{url}/{endpoint}/{id}";
            string json = JsonUtility.ToJson(jsonData);
            byte[] conntent = System.Text.Encoding.UTF8.GetBytes(json);

            using var request = new UnityWebRequest(url, "PATCH");
            request.uploadHandler = new UploadHandlerRaw(conntent);
            request.downloadHandler = new DownloadHandlerBuffer();

            request.SetRequestHeader("Content-Type", "application/json");
            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Data upaded at {urlWithId}");
                return true;
            }
            else
            {
                Debug.LogError($"Request at {url} :: failed: {request.error}");
                return false;
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error: " + e.Message);
            throw;
        }

    }

    public static async Task<bool> DeleteRequest(string url, int id)
    {
        try
        {
            string urlWithId = $"{url}/{id}";
            using var request = UnityWebRequest.Delete(url);

            request.SetRequestHeader("Content-Type", "application/json");
            var operation = request.SendWebRequest();

             while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Data deleted at {urlWithId}");
                return true;
            }
            else
            {
                 Debug.LogError($"Request at {url} :: failed: {request.error}");
                return false;
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Error: " + e.Message);
            throw;
        }
    }
}
