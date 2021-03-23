using GuestApp.DAL;
using GuestApp.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GuestApp.Services
{
    public class LoginService
    {
        private static string _apiKey = "AIzaSyB5c5uJoax_O5NWaTVhmq9Hw3ck3W0QWrY";

        public static async Task<bool> RegistrationSuccessfull(FireBaseUser user)
        {
            if (BothPasswordsMatch(user))
            {
                const string urlEndPoint = "signUp";
                if (await IsValidRequest(user, urlEndPoint))
                {
                    var userRepository = new UserRepository();
                    userRepository.AddUser(user);
                    return true;
                }
            }
            return false;
        }

        public static async Task<bool> LoginSuccessfull(FireBaseUser user)
        {
            const string urlEndPoint = "signInWithPassword";
            return await IsValidRequest(user, urlEndPoint);
        }

        public static async Task<bool> ChangePassword(FireBaseUser user)
        {
            const string urlEndPoint = "update";
            return await IsValidRequest(user, urlEndPoint, user.NewPassword);
        }

        private static bool BothPasswordsMatch(FireBaseUser user)
        {
            if (user.Password.Trim() != user.Password2.Trim())
            {
                MessageBox.Show("Your Passwords Don't Match!", "Password Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
                return true;
        }

        private static async Task<bool> IsValidRequest(FireBaseUser user, string endPoint, string newPassword = null)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = SetContent(user, newPassword);
                HttpResponseMessage response = await GetResponse(client, content, endPoint);

                if (response == null)
                    return false;

                if (response.IsSuccessStatusCode)
                {
                    var fireBaseUser = await GetUserIds(response);
                    user.Id = fireBaseUser.Id;
                    user.IdToken = fireBaseUser.IdToken;
                    return true;
                }
                else
                    DisplayError(response);
              return false;
            }
        }
        private static StringContent SetContent(FireBaseUser user, string newPassword = null)
        {
            var body = new
            {
                email = user.Email,
                idToken = user.IdToken,
                password = (newPassword == null) ? user.Password : newPassword,
                returnSecureToken = (newPassword == null) ? true : false
            };

            string bodyJson = JsonConvert.SerializeObject(body);
            var data = new StringContent(bodyJson, Encoding.UTF8, "application/json");
            return data;
        }

        private static async Task<HttpResponseMessage> GetResponse(HttpClient client, StringContent content, string endPoint)
        {
            HttpResponseMessage response = null;
            try
            {
                var url = $"https://identitytoolkit.googleapis.com/v1/accounts:{endPoint}?key={_apiKey}";
                response = await client.PostAsync(url, content);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                MessageBox.Show("Please check your Internet connection!", "Failed Request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                throw;
            }
            return response;
        }

        private static async Task<FireBaseUser> GetUserIds(HttpResponseMessage response)
        {
            var user = new FireBaseUser();
            string resultJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<FirebaseResult>(resultJson);
            user.Id = result.localId;
            user.IdToken = result.idToken;
            return user;
        }

        private static async void DisplayError(HttpResponseMessage response)
        {
            string errorJson = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<FirebaseError>(errorJson);
            MessageBox.Show(error.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
