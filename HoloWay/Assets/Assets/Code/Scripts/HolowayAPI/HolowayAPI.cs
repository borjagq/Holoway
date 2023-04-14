using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Collections;

namespace holowayapi
{

    public struct HoloFile
    {
        
        public HoloFile(string file_id, string file_type, string file_name)
        {
            id = file_id;
            type = file_type;
            name = file_name;
        }

        public string id { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public override string ToString() => $"({name} of type {type} ({id}))";

    }

    public class HolowayAPI : MonoBehaviour
    {

        public string last_response = "I CAN NOT STAND THIS PUTA";

        private string priv_key = "";
        private string api_key = "";

        public HolowayAPI(){}

        public void add_params(string priv_key, string api_key)
        {
            this.priv_key = priv_key;
            this.api_key = api_key;
        }

        public (string, string, string, string) check_credential(string token)
        {

            // Returns: status, user_id, name, refreshed_token, msg

            // Get the current timestamp.
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int epoch_time = (int) t.TotalSeconds;
            string timestamp = epoch_time.ToString();

            // Get the current signature
            string message = token + api_key + timestamp;

            // Get the signature.
            string signature = sign_message(priv_key, message);

            // Prepare the query.
            GetQueryCreator query_string = new GetQueryCreator();

            // Add values.
            query_string.Add("token", token);
            query_string.Add("api_key", api_key);
            query_string.Add("timestamp", timestamp);
            query_string.Add("signature", signature);
            string get_vars = query_string.ToString() ?? "";

            // Make the API call.
            string url = $"https://azure.borjagq.com/check_credentials/?{get_vars}";

            // Call the request.
            StartCoroutine(api_request(url));

            string response = last_response;

            Debug.Log("Hello from the other side: " + response);

            // Convert them to a key-value dict.
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            if(values == null) {
                throw new ArgumentNullException();
            }

            // Get those values.
            string status = (values.ContainsKey("status")) ? values["status"] : "";
            string email = (values.ContainsKey("email")) ? values["email"] : "";
            string refreshed_token = (values.ContainsKey("refreshed_token")) ? values["refreshed_token"] : "";
            string msg = (values.ContainsKey("msg")) ? values["msg"] : "";

            return (status, email, refreshed_token, msg);

        }

        public (string, string, string) create_login()
        {

            // Return: status, code, msg

            // Get the current timestamp.
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int epoch_time = (int) t.TotalSeconds;
            string timestamp = epoch_time.ToString();

            // Get the current signature
            string message = api_key + timestamp;

            // Get the signature.
            string signature = sign_message(priv_key, message);

            // Prepare the query.
            GetQueryCreator query_string = new GetQueryCreator();

            // Add values.
            query_string.Add("api_key", api_key);
            query_string.Add("timestamp", timestamp);
            query_string.Add("signature", signature);
            string get_vars = query_string.ToString() ?? "";

            // Make the API call.
            string url = $"https://azure.borjagq.com/create_login/?{get_vars}";

            Debug.Log("IF YOU KNOW HOW I FEEL WHY WOULD YOU DO THAT" + url);

            // Call the request.
            StartCoroutine(api_request(url));

            string response = last_response;

            Debug.Log("Hello from the other side: " + response);

            /*while (response == "I CAN NOT STAND THIS PUTA") {
                continue;
            }
*/
            // Convert them to a key-value dict.
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            if(values == null) {
                throw new ArgumentNullException();
            }

            // Get those values.
            string status = (values.ContainsKey("status")) ? values["status"] : "";
            string code = (values.ContainsKey("code")) ? values["code"] : "";
            string msg = (values.ContainsKey("msg")) ? values["msg"] : "";

            return (status, code, msg);

        }

        public (string, List<HoloFile>, string) list_files(string dir_id, string token)
        {

            // Return: status, files, msg

            // Get the current timestamp.
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int epoch_time = (int) t.TotalSeconds;
            string timestamp = epoch_time.ToString();

            // Get the current signature
            string message = dir_id + token + api_key + timestamp;

            // Get the signature.
            string signature = sign_message(priv_key, message);

            // Prepare the query.
            GetQueryCreator query_string = new GetQueryCreator();

            // Add values.
            query_string.Add("api_key", api_key);
            query_string.Add("dir_id", dir_id);
            query_string.Add("token", token);
            query_string.Add("timestamp", timestamp);
            query_string.Add("signature", signature);
            string get_vars = query_string.ToString() ?? "";

            // Make the API call.
            string url = $"https://azure.borjagq.com/list_files/?{get_vars}";

            // Call the request.
            StartCoroutine(api_request(url));

            string response = last_response;

            Debug.Log("Hello from the other side: " + response);

            // Convert them to a key-value dict.
            JObject json_parser = JObject.Parse(response);

            // Get those values.
            string status = (json_parser["status"] ?? "").ToString();
            string msg = (json_parser["msg"] ?? "").ToString();

            // Convert the returned files to HoloFiles.
            #nullable enable
            JToken ?file_parser = json_parser["files"];
            List<HoloFile> files = new List<HoloFile>();
            if (file_parser is not null)
            {
                files = (file_parser.ToObject<List<HoloFile>>()) ?? new List<HoloFile>();
            }
            #nullable disable

            // For one JsonConvert.DeserializeObject<Account>(json)

            return (status, files, msg);

        }

        public (string, List<HoloFile>, string) list_files(string token)
        {

            return list_files("", token);

        }

        public (string, string, string, string) retrieve_login(string code)
        {

            // Return: status, user_id, token, msg

            // Get the current timestamp.
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int epoch_time = (int) t.TotalSeconds;
            string timestamp = epoch_time.ToString();

            // Get the current signature
            string message = code + api_key + timestamp;

            // Get the signature.
            string signature = sign_message(priv_key, message);

            // Prepare the query.
            GetQueryCreator query_string = new GetQueryCreator();

            // Add values.
            query_string.Add("api_key", api_key);
            query_string.Add("code", code);
            query_string.Add("timestamp", timestamp);
            query_string.Add("signature", signature);
            string get_vars = query_string.ToString() ?? "";

            // Make the API call.
            string url = $"https://azure.borjagq.com/retrieve_login/?{get_vars}";

            // Call the request.
            StartCoroutine(api_request(url));

            string response = last_response;

            Debug.Log("Hello from the other side: " + response);

            // Convert them to a key-value dict.
            var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
            if(values == null) {
                throw new ArgumentNullException();
            }

            // Get those values.
            string status = (values.ContainsKey("status")) ? values["status"] : "";
            string email = (values.ContainsKey("email")) ? values["email"] : "";
            string token = (values.ContainsKey("token")) ? values["token"] : "";
            string msg = (values.ContainsKey("msg")) ? values["msg"] : "";

            return (status, email, token, msg);

        }

        private IEnumerator api_request(string url)
        {

            // Build the instance.
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Call the actual request.
            yield return request.SendWebRequest();
            
            // Get the contents.
            if (request.error != null)
            {
                Debug.Log("Error While Sending: " + request.error);
                Debug.Log("this is the line" + request.downloadHandler.text);
            }
            else
            {
                last_response = request.downloadHandler.text;
                Debug.Log("YESI YESI");
            }

        }

        public (string, string) share_file(string file_id, List<string> emails, string token)
        {

            // Return: status, msg

            // Get the current timestamp.
            TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
            int epoch_time = (int) t.TotalSeconds;
            string timestamp = epoch_time.ToString();

            // Get the current signature
            string message = file_id + String.Join("", emails.ToArray()) + token + api_key + timestamp;

            // Get the signature.
            string signature = sign_message(priv_key, message);

            // Prepare the query.
            GetQueryCreator query_string = new GetQueryCreator();

            // Add values.
            // Add values.
            query_string.Add("api_key", api_key);
            query_string.Add("file_id", file_id);
            query_string.Add("token", token);
            query_string.Add("timestamp", timestamp);
            query_string.Add("signature", signature);

            foreach (var email in emails)
            {
                query_string.Add("emails", email);
            }
            string get_vars = query_string.ToString() ?? "";

            // Make the API call.
            string url = $"https://azure.borjagq.com/share_file/?{get_vars}";

            // Call the request.
            StartCoroutine(api_request(url));

            string response = last_response;

            Debug.Log("Hello from the other side: " + response);

            // Convert them to a key-value dict.
            JObject json_parser = JObject.Parse(response);

            // Get those values.
            string status = (json_parser["status"] ?? "").ToString();
            string msg = (json_parser["msg"] ?? "").ToString();

            // Convert the returned files to HoloFiles.
            #nullable enable
            JToken ?file_parser = json_parser["files"];
            List<HoloFile> files = new List<HoloFile>();
            if (file_parser is not null)
            {
                files = (file_parser.ToObject<List<HoloFile>>()) ?? new List<HoloFile>();
            }
            #nullable disable

            // For one JsonConvert.DeserializeObject<Account>(json)

            return (status, msg);

        }

        private string sign_message(string priv_key, string message)
        {

            // Transform the message to bytes.
            byte[] msg_bytes = Encoding.UTF8.GetBytes(message);

            // Obtains the hash for this message using the SHA256 algorithm (sota).
            using SHA256 alg = SHA256.Create();
            byte[] hash = alg.ComputeHash(msg_bytes);

            RSAParameters sharedParameters;
            byte[] signature;

            // Generate signature
            using (RSA rsa = System.Security.Cryptography.RSA.Create())
            {

                // Load the private key.
                rsa.FromXmlString(File.ReadAllText(priv_key));
                sharedParameters = rsa.ExportParameters(false);

                // Get the signer with the padding settings.
                RSAPKCS1SignatureFormatter signer = new(rsa);
                signer.SetHashAlgorithm(nameof(SHA256));

                signature = signer.CreateSignature(hash);

            }

            return Convert.ToBase64String(signature);

        }

    }

}