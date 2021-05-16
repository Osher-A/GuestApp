namespace GuestApp.DTO
{
    public class FirebaseResult
    {
        public string Kind { get; set; }
        public string IdToken { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public string ExpiresIn { get; set; }
        public string LocalId { get; set; }
    }
    public class ErrorDetails
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public string Domain { get; set; }
        public string Reason { get; set; }
    }

    public class FirebaseError
    {
        public ErrorDetails Error { get; set; }
    }

    //public class FireBaseChangePasswordResult
    //{
    //    public string LocalId { get; set; }
    //    public string Email { get; set; }
    //    public string DispalayName { get; set; }
    //    public string PhotoUrl { get; set; }
    //    public string PasswordHash { get; set; }
    //    public string IdToken { get; set; }
    //    public string RefreshToken { get; set; }
    //    public string ExpiresIn { get; set; }
    //    public List<UsersInfo> ProviderUserInfo { get; set; }
    //}

    //public class UsersInfo
    //{
    //    public string ProviderId { get; set; }
    //    public string FederatedId { get; set; }
    //}
}
