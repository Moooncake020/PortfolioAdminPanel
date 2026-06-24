namespace PortfolioAdminPanel.Models
{
    public class ProjectItem
    {
        // Veritabanındaki her kaydın benzersiz bir kimliği (Primary Key) olmalıdır.
        public int Id { get; set; }

        // Projenin başlığı (Örn: FPS Arena Survival veya Selenium CI/CD Test)
        public string Title { get; set; } = string.Empty;

        // Projenin detaylı açıklaması
        public string Description { get; set; } = string.Empty;

        // Kullanılan teknolojiler (Örn: Unreal Engine 5, C#, Docker)
        public string Technologies { get; set; } = string.Empty;

        // Projenin tamamlanıp tamamlanmadığını tutan mantıksal değer
        public bool IsCompleted { get; set; }

        // Eklenme tarihi
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}