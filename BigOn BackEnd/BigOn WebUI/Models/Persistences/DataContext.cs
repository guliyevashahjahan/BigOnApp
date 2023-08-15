using Microsoft.EntityFrameworkCore;

namespace BigOn_WebUI.Models.Persistences
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            :base(options)
        {
            
        }
    }
}
