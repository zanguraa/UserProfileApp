namespace UserProfile.Domain.Entities
{
    public class UserProfileEntity // <<<<< შეცვალე სახელი სინგულარად
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public List<string> FavoriteCountries { get; set; }
        public List<string> VisitedCountries { get; set; }
        public string FavoriteFootballTeam { get; set; }
        public List<string> Hobbies { get; set; }
        public string Bio { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
