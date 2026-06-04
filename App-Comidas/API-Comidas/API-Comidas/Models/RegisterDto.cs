namespace API_Comidas.Models
{
    public class RegisterDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Phone { get; set; }

        public string RoleName { get; set; } // "Restaurante" or "Usuario"

        // Optional fields when registering as a restaurant
        public string TradeName { get; set; }

        public int CategoryId { get; set; }

        public string OpeningTime { get; set; }

        public string ClosingTime { get; set; }
    }
}