using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RepositorySQL
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
