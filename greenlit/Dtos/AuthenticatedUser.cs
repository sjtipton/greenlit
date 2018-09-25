namespace greenlit.Dtos
{
    public class AuthenticatedUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Token { get; set; }
    }
}
