using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using static CMMMobileMaui.API.Common.BaseBLL;

namespace CMMMobileMaui.API
{
    public class APIManage : Interfaces.IAPIManage
    {
        #region FIELDS

        public event EventHandler OnError;
        public event EventHandler OnSuccess;
        private readonly AsyncRetryPolicy asyncRetryPolicy;
        private readonly IHttpClientFactory httpClientFactory;

        #endregion

        #region PROPERTY Host

        public string Host
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Login

        public string Login
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Pass

        public string Pass
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsAnonymous

        public bool IsAnonymous
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsSSL

        public bool IsSSL
        {
            get;
            set;
        }

        #endregion

        #region Cstr

        public APIManage(IHttpClientFactory httpClientFactory):this()
        {
            this.httpClientFactory = httpClientFactory;
        }

        public APIManage()
        {
            asyncRetryPolicy = Policy.Handle<Exception>().RetryAsync(5, onRetry: (ex, retry, context) =>
                 Console.WriteLine(ex.Message)
            );
        }

        #endregion

        #region METHOD GetHttpType

        private string GetHttpType() =>
            IsSSL ? "https"
            : "http";

        #endregion

        #region METHOD GetBaseHttpClient

        private HttpClient GetBaseHttpClient(AuthenticationType authentication)
        {
            HttpClient client = null;

            if(httpClientFactory == null)
            {
                if (IsSSL)
                {
                    var httpClientHandler = new HttpClientHandler();

                    httpClientHandler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => 
                    {
                        return true; 
                    };

                    client = new HttpClient(httpClientHandler);
                }
                else
                {
                    client = new HttpClient();
                }

                client.BaseAddress = GetHostUri();
                client.Timeout = new TimeSpan(0, 0, 10);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
            else
            {
                client = httpClientFactory.CreateClient("CMM");
            }

            if (!IsAnonymous)
            {
                if (authentication == AuthenticationType.Basic)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", GetAuthorizationHeaderText());
                }
                else if(authentication == AuthenticationType.Token 
                    && MainObjects.Instance.CurrentUser != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", MainObjects.Instance.CurrentUser.Token);
                }
            }

            return client;
        }

        #endregion     

        #region METHOD GetAuthorizationHeaderText

        public string GetAuthorizationHeaderText() =>
            Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Login, Pass)));

        #endregion

        #region METHOD GetSocetUri

        internal Uri GetSocketUri(string path) =>
            new Uri($"ws://{Host}/{path}");

        #endregion

        #region METHOD GetHttpClientResponseText

        private async Task<string> GetHttpClientResponseText(Func<HttpContent, Task<HttpResponseMessage>> responseMethod, string obj)
        {
            if (string.IsNullOrEmpty(obj))
            {
                obj = string.Empty;
            }

            Console.WriteLine(obj);

            string resp = string.Empty;
            string expMessage = string.Empty;

            using (HttpContent httpContent = new StringContent(obj, Encoding.UTF8, "application/json"))
            {
                CancellationTokenSource token = new CancellationTokenSource();
                CancellationToken cancellationToken = token.Token;

                Task responseTask = responseMethod(httpContent).ContinueWith(async t =>
                {
                    if (t == null)
                    {
                        throw new OperationCanceledException("Connection timeout [No Response]");
                    }

                    try
                    {
                        var result = t.Result;
                        cancellationToken.ThrowIfCancellationRequested();
                        resp = await result.Content.ReadAsStringAsync();

                        if (!result.IsSuccessStatusCode)
                        {
                            if (string.IsNullOrEmpty(resp))
                            {
                                expMessage = result.ReasonPhrase ?? "UNKNOW ERROR";
                            }
                        }
                    }
                    catch (OperationCanceledException) { }
                    catch (AggregateException ex) { expMessage = ex.Message; }
                    catch (Exception ex) { expMessage = ex.Message; }
                }, cancellationToken);

                Task cancelTask = Task.Run(async () =>
                {
                    await Task.Delay(10000);
                });

                await Task.WhenAny(responseTask, cancelTask);

                if (!responseTask.IsCompleted)
                {
                    token.Cancel();
                    throw new OperationCanceledException("Connection timeout [10 s]");
                }

                if (!string.IsNullOrEmpty(expMessage))
                {
                    throw new Exception(expMessage);
                }
            }

            return resp;
        }

        #endregion

        #region METHOD GetTaskItem

        public async Task<InvokeReturn<T>> GetTask<T>(string path, string obj = null, AuthenticationType authentication = AuthenticationType.Token)
        {
            HttpClient client = GetBaseHttpClient(authentication);

            if (client != null)
            {
                return await RunClientMethod<T>(path, obj, (content) => client.GetAsync(path));
            }

            return default;
        }

        #endregion

        private async Task<InvokeReturn<T>> RunClientMethod<T>(string path, string obj, Func<HttpContent, Task<HttpResponseMessage>> method)
        {
            var result = await asyncRetryPolicy.ExecuteAndCaptureAsync(async () =>
            {
                string responseText = string.Empty;

                try
                {
                    Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} # [{path}][JSON]:{obj}");
                    responseText = await GetHttpClientResponseText(method, obj);

                    if (!string.IsNullOrEmpty(responseText))
                    {
                        var response = JsonConvert.DeserializeObject<InvokeReturn<T>>(responseText);

                        if (!response.IsValid)
                        {
                            OnError?.Invoke($"{string.Join("#", response.Errors)}", null);
                        }

                        return response;
                    }
                }
                catch (OperationCanceledException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw e;
                }

                return Activator.CreateInstance<InvokeReturn<T>>();
            });

            if (result.FinalException != null)
            {
                OnError?.Invoke($"{result.FinalException.Message}", null);

                return Activator.CreateInstance<InvokeReturn<T>>();
            }

            return result.Result;

        }


        #region METHOD PostTask

        public async Task<InvokeReturn<T>> PostTask<T>(string path, string obj, AuthenticationType authentication)
        {
            HttpClient client = GetBaseHttpClient(authentication);

            if (client != null)
            {
                return await RunClientMethod<T>(path, obj, (content) => client.PostAsync(path, content));

            }

            return default;

        }

        #endregion

        #region METHOD DeleteTask

        public async Task<InvokeReturn<T>> DeleteTask<T>(string path, string obj, AuthenticationType authentication)
        {
            HttpClient client = GetBaseHttpClient(authentication);

            if (client != null)
            {
                return await RunClientMethod<T>(path, obj, (content) => client.DeleteAsync(path));
            }

            return default;
        }

        #endregion

        #region METHOD PutTask

        public async Task<InvokeReturn<T>> PutTask<T>(string path, string obj, AuthenticationType authentication)
        {
            HttpClient client = GetBaseHttpClient(authentication);

            if (client != null)
            {
                return await RunClientMethod<T>(path, obj, (content) => client.PutAsync(path, content));
            }

            return default;
        }

        #endregion

        #region METHOD IsDataSet

        public bool IsDataSet()
        {
            return (!string.IsNullOrEmpty(Login)
                && !string.IsNullOrEmpty(Pass)
                && !string.IsNullOrEmpty(Host) && !IsAnonymous)
                || (!string.IsNullOrEmpty(Host) && IsAnonymous);
        }

        public Uri GetHostUri() =>
            new Uri($"{GetHttpType()}://{Host}");

        #endregion
    }


}
