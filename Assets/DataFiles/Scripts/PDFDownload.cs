using UnityEngine;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine.UI;


public class PDFDownload : MonoBehaviour
{
    public string url = "https://example.com/myfile.txt";
    public Text resultText;
    public void DownloadFile()
    {
        StartCoroutine(DownloadFileCoroutine());
    }

    IEnumerator DownloadFileCoroutine()
    {
        // Create a request for the URL.
        WebRequest request = WebRequest.Create(url);

        try
        {
            // Send the request to the server and wait for the response.
            WebResponse response = request.GetResponse();
            request.Timeout = 10000;
            // Check the status code to make sure the request was successful.
            HttpStatusCode statusCode = ((HttpWebResponse)response).StatusCode;
            if (statusCode != HttpStatusCode.OK)
            {
                throw new WebException("Server returned status code " + statusCode);
            }

            // Read the response stream and convert it to a byte array.
            Stream responseStream = response.GetResponseStream();
            MemoryStream memoryStream = new MemoryStream();
            byte[] buffer = new byte[1024];
            int bytesRead = 0;
            while ((bytesRead = responseStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                memoryStream.Write(buffer, 0, bytesRead);
            }
            byte[] data = memoryStream.ToArray();

            // Save the file to disk.
            // string filePath = "D:/myfile.pdf";
            string filePath = Path.Combine(Application.persistentDataPath, "The_Alchemist.pdf");
            File.WriteAllBytes(filePath, data);

            // Display a message indicating where the file was saved.
            Debug.Log("File downloaded and saved to: " + filePath);

            // Display a message indicating where the file was saved.
            resultText.text = "File downloaded and saved to: " + filePath;

            // Clean up the response.
            responseStream.Close();
            response.Close();
        }
        catch (WebException e)
        {
            // Log any errors and display an error message.
            Debug.LogError("Error downloading file: " + e.ToString());
            resultText.text = "Error downloading file";
        }

        yield return null;
    }
}
