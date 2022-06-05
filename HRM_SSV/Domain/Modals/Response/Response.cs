using Domain.Modals.User;

namespace Domain.Modals.Response
{
    public class Response
    {
        public UserToGet User { get; set; } = new();
        public bool IsSuccess { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
