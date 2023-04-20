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

        private string priv_key = "";
        private string api_key = "";

        public HolowayAPI(){}

        public void add_params(string priv_key, string api_key)
        {
            this.priv_key = priv_key;
            this.api_key = api_key;
        }

        public IEnumerator check_credential(string token, Action<string, string, string, string, string> done)
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

            Debug.Log(url);

            // Build the instance.
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Call the actual request.
            yield return request.SendWebRequest();
            
            // Get the contents.
            if (request.error == null)
            {

                string response = request.downloadHandler.text;

                // Convert them to a key-value dict.
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                if(values == null) {
                    throw new ArgumentNullException();
                }

                // Get those values.
                string status = (values.ContainsKey("status")) ? values["status"] : "";
                string email = (values.ContainsKey("email")) ? values["email"] : "";
                string name = (values.ContainsKey("name")) ? values["name"] : "";
                string refreshed_token = (values.ContainsKey("refreshed_token")) ? values["refreshed_token"] : "";
                string msg = (values.ContainsKey("msg")) ? values["msg"] : "";

                done(status, email, name, refreshed_token, msg);

            }
            else
            {

                Debug.Log("ERROR -> " + request.error);
                Debug.Log(request.downloadHandler.text);

            }

        }

        public IEnumerator create_login(Action<string, string, string> done)
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

            // Build the instance.
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Call the actual request.
            yield return request.SendWebRequest();
            
            // Get the contents.
            if (request.error == null)
            {

                string response = request.downloadHandler.text;

                // Convert them to a key-value dict.
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                if(values == null) {
                    throw new ArgumentNullException();
                }

                // Get those values.
                string status = (values.ContainsKey("status")) ? values["status"] : "";
                string code = (values.ContainsKey("code")) ? values["code"] : "";
                string msg = (values.ContainsKey("msg")) ? values["msg"] : "";

                done(status, code, msg);

            }
            else
            {

                Debug.Log("ERROR -> " + request.error);
                Debug.Log(request.downloadHandler.text);

            }

        }

        public IEnumerator list_files(string dir_id, string token, Action<string, List<HoloFile>, string> done)
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

            // Build the instance.
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Call the actual request.
            yield return request.SendWebRequest();
            
            // Get the contents.
            if (request.error == null)
            {

                string response = request.downloadHandler.text;

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

                done(status, files, msg);

            }

        }

        public IEnumerator retrieve_login(string code, Action<string, string, string, string, string> done)
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

            // Build the instance.
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Call the actual request.
            yield return request.SendWebRequest();
            
            // Get the contents.
            if (request.error == null)
            {

                string response = request.downloadHandler.text;

                // Convert them to a key-value dict.
                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
                if(values == null) {
                    throw new ArgumentNullException();
                }

                // Get those values.
                string status = (values.ContainsKey("status")) ? values["status"] : "";
                string email = (values.ContainsKey("email")) ? values["email"] : "";
                string name = (values.ContainsKey("name")) ? values["name"] : "";
                string token = (values.ContainsKey("token")) ? values["token"] : "";
                string msg = (values.ContainsKey("msg")) ? values["msg"] : "";

                done(status, email, name, token, msg);

            }

        }

        public IEnumerator share_file(string file_id, List<string> emails, string token, Action<string, string> done)
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

            // Build the instance.
            UnityWebRequest request = UnityWebRequest.Get(url);

            // Call the actual request.
            yield return request.SendWebRequest();
            
            // Get the contents.
            if (request.error == null)
            {

                string response = request.downloadHandler.text;

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

                done(status, msg);

            }else {
                Debug.Log("error");
            }

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
