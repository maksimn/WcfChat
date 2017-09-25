namespace WcfChat.Services.Repositories.Model {
    class User {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
    }
}
