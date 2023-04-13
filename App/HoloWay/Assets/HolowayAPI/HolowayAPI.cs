using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

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

    class HolowayAPI
    {

        static public (string, string, string, string) check_credential(string priv_key, string token, string api_key)
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
            Task<string> task = Task.Run(() => api_request(url));

            // Wait until it has loaded.
            task.Wait();

            // Get the response from the task.
            string response = task.Result;

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

        static public (string, string, string) create_login(string priv_key, string api_key)
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
            string url = $"https://azure.borjagq.com/check_credentials/?{get_vars}";

            // Call the request.
            Task<string> task = Task.Run(() => api_request(url));

            // Wait until it has loaded.
            task.Wait();

            // Get the response from the task.
            string response = task.Result;

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

        static public (string, List<HoloFile>, string) list_files(string priv_key, string api_key, string dir_id, string token)
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
            string url = $"https://azure.borjagq.com/check_credentials/?{get_vars}";

            // Call the request.
            Task<string> task = Task.Run(() => api_request(url));

            // Wait until it has loaded.
            task.Wait();

            // Get the response from the task.
            string response = task.Result;

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

        static public (string, List<HoloFile>, string) list_files(string priv_key, string api_key, string token)
        {

            return list_files(priv_key, api_key, "", token);

        }

        static public (string, string, string, string) retrieve_login(string priv_key, string api_key, string code)
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
            string url = $"https://azure.borjagq.com/check_credentials/?{get_vars}";

            // Call the request.
            Task<string> task = Task.Run(() => api_request(url));

            // Wait until it has loaded.
            task.Wait();

            // Get the response from the task.
            string response = task.Result;

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

        static async private Task<string> api_request(string url)
        {

            // Build the instance.
            UnityWebRequest request = UnityWebRequest.Get(url);

            await Task.Yield();

            // Call the actual request.
            request.SendWebRequest();
            
            // Get the contents.
            string response = "";
            if (request.error != null)
            {
                Debug.Log("Error While Sending: " + request.error);
            }
            else
            {
                response = request.downloadHandler.text;
            }

            return response;

        }

        private static int get_integer_size(BinaryReader binr)
        {

            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
                {
                    highbyte = binr.ReadByte();
                    lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                }
                else
                {
                    count = bt;
                }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }

            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;

        }

        private static void import_from_pem(RSA rsa, string priv_key)
        {

            byte[] MODULUS, E, D, P, Q, DP, DQ, IQ ;

            // Read the file.
            byte[] privkey_content = Encoding.UTF8.GetBytes(File.ReadAllText(priv_key));
            
            MemoryStream  mem = new MemoryStream(privkey_content);
            BinaryReader binr = new BinaryReader(mem);

            byte bt = 0;
            ushort twobytes = 0;
            int elems = 0;

            try {

                twobytes = binr.ReadUInt16();

                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte();        //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16();       //advance 2 bytes
                else
                    return;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return;
                    bt = binr.ReadByte();
                if (bt !=0x00)
                    return;

                //------  all private key components are Integer sequences ----
                elems = get_integer_size(binr);
                MODULUS = binr.ReadBytes(elems);

                elems = get_integer_size(binr);
                E = binr.ReadBytes(elems) ;

                elems = get_integer_size(binr);
                D = binr.ReadBytes(elems) ;

                elems = get_integer_size(binr);
                P = binr.ReadBytes(elems) ;

                elems = get_integer_size(binr);
                Q = binr.ReadBytes(elems) ;

                elems = get_integer_size(binr);
                DP = binr.ReadBytes(elems) ;

                elems = get_integer_size(binr);
                DQ = binr.ReadBytes(elems) ;

                elems = get_integer_size(binr);
                IQ = binr.ReadBytes(elems) ;

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSAParameters RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                
                rsa.ImportParameters(RSAparams);

            }
            
            catch (Exception) {
                return;
            }
            
            finally {
                binr.Close();
            }

        }

        static public (string, string) share_file(string priv_key, string api_key, string file_id, List<string> emails, string token)
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
            string url = $"https://azure.borjagq.com/check_credentials/?{get_vars}";

            // Call the request.
            Task<string> task = Task.Run(() => api_request(url));

            // Wait until it has loaded.
            task.Wait();

            // Get the response from the task.
            string response = task.Result;

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

        static private string sign_message(string priv_key, string message)
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
                import_from_pem(rsa, priv_key);
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